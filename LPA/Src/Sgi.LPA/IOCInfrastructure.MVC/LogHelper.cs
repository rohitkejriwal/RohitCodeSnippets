using Logger.Core;
using Logger.Domain;
using System;

namespace IOCInfrastructure.MVC
{
    public class LogHelper : LogWriteHelper
    {
        private IServiceResolver _serviceResolver;
        private ILogger _logger;

        public LogHelper(IServiceResolver serviceResolver)
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
                var txnLogIOCData = txnData.Get<TransactionLoggerData>(Consts.TransactionDataKey);
                if (txnLogIOCData != null)
                {
                    txnLogData = txnLogIOCData;
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
    }
}