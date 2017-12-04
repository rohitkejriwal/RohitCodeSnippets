using IOCInfrastructure;
using Logger.Core;
using Logger.Domain;
using SGI.LPA.Crawler.Utilities;
using System;

namespace SGI.LPA.Crawler.App_Start
{
    public class CrawlerLogHelper : LogWriteHelper
    {
        private IServiceResolver _serviceResolver;
        private ILogger _logger;

        public CrawlerLogHelper(IServiceResolver serviceResolver)
            : base(serviceResolver)
        {
            _serviceResolver = serviceResolver;
            _logger = _serviceResolver.GetInstance<ILogger>();
        }

        protected override TransactionLoggerData GetLogTnxData()
        {
            TransactionLoggerData txnLogData = new TransactionLoggerData();
            try
            {
                var txnData = _serviceResolver.GetInstance<IServiceTransactionData>();
                var requestHeader = txnData.GetRequestHeader();
                if (requestHeader != null)
                {
                    txnLogData.TransactionId = requestHeader.TransactionId;
                    txnLogData.ClientInfo = requestHeader.Client;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(new ErrorLoggerData()
                {
                    ErrorMessage = ex.Message,
                    LoggerName = "LogHelper",
                    MethodName = "GetLogTnxData",
                    Message = ex.Message,
                    InnerException = ex.InnerException.ToString(),
                });
            }
            return txnLogData;
        }

        public override void LogStep(string message, object parent = null, System.Diagnostics.Stopwatch stopwatch = null)
        {
            try
            {

                var transactionObject = GetLogTnxData();
                var txnData = _serviceResolver.GetInstance<IServiceTransactionData>();
                if (stopwatch == null)
                {
                    stopwatch = txnData.GetStopwatch();
                }

                int step = txnData.GetTransactionStep();
                var lastStepDuration = txnData.TransactionLastStepDuration(step);
                var currentStepDuration = (stopwatch.ElapsedMilliseconds - lastStepDuration);
                txnData.UpdateTransactionStepDuration(step, currentStepDuration);
                var clientStateId = txnData.TransactionGetClientStateID();
                var clientState = clientStateId != string.Empty ? clientStateId + "." : string.Empty;
                var formattedMessage = string.Format("Step:{0}{1}. {2}", clientState, txnData.GetApplicationTransactionStep(), message, stopwatch.ElapsedMilliseconds);
                string logger = string.Empty;

                // update the txn step to next .
                txnData.UpdateTransactionStep();
                if (parent != null)
                {
                    logger = parent.GetType().Name;
                }
                var logdata = new ExecutionStepLoggerData
                {
                    Message = formattedMessage,
                    TransactionId = transactionObject.TransactionId,
                    LoggerName = logger,
                    Step = step,
                    TotalExecuationDuration = stopwatch.ElapsedMilliseconds,
                    StepDuration = currentStepDuration
                };
                if (parent != null)
                {
                    logdata.LoggerName = parent.GetType().Name;
                }
                _logger.Timespan(logdata);
            }
            catch
            {
                // need to handle this;
                throw;
            }
        }
    }
}