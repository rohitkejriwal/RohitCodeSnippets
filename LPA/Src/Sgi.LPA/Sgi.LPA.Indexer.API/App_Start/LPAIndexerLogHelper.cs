using IOCInfrastructure;
using Logger.Core;
using Logger.Domain;
using Sgi.LPA.Common.Utilities;
using System;

namespace Sgi.LPA.Indexer.API.App_Start
{
    public class LPAIndexerLogHelper : LogWriteHelper
    {
        private IServiceResolver _serviceResolver;
        private ILogger _logger;

        public LPAIndexerLogHelper(IServiceResolver serviceResolver)
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
                    txnLogData.SessionId = requestHeader.SessionId;
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
                txnData.UpdateTransactionStep();
                var formattedMessage = string.Format("Step:{0}. {1}. Process time:{2}ms", step, message, stopwatch.ElapsedMilliseconds);
                string logger = string.Empty;
                if (parent != null)
                {
                    logger = parent.GetType().Name;
                }
                else
                {
                }
                var logdata = new ExecutionStepLoggerData
                {
                    Message = formattedMessage,
                    TransactionId = transactionObject.TransactionId,
                    SessisonId = transactionObject.SessionId,
                    LoggerName = logger,
                    Step = step
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