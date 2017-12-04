using Logger.Core;
using Logger.Domain;
using System;
using System.Collections.Generic;

namespace IOCInfrastructure
{
    public abstract class LogWriteHelper : ILogHelper
    {
        private IServiceResolver _serviceResolver;
        private ILogger _logger;

        public LogWriteHelper(IServiceResolver serviceResolver)
        {
            _serviceResolver = serviceResolver;
            _logger = _serviceResolver.GetInstance<ILogger>();
        }

        protected virtual TransactionLoggerData GetLogTnxData()
        {
            TransactionLoggerData txnLogData = new TransactionLoggerData()
            {
                SessionId = "NONE",
                TransactionId = "TXN0",
                ClientInfo = "NONE",
                Component = "NONE"
            };
            return txnLogData;
        }

        public virtual void LogError(Exception ex, string className, string methodName, Dictionary<string, object> inputParams, object parent = null)
        {
            try
            {
                var logTxnData = GetLogTnxData();
                var logdata = new ErrorLoggerData
                {
                    Message = ex.Message,
                    ErrorMessage = ex.Message,
                    ErrorSource = ex.Source,
                    MethodName = methodName,
                    Params = inputParams,
                    StackTrace = ex.StackTrace,
                };
                if (parent != null)
                {
                    logdata.LoggerName = parent.GetType().Name;
                }
                if (ex.InnerException != null)
                {
                    logdata.InnerException = Convert.ToString(ex.InnerException);
                }

                if (logTxnData != null)
                {
                    logdata.TransactionId = logTxnData.TransactionId;
                    logdata.SessisonId = logTxnData.SessionId;
                    logdata.ClientInfo = logTxnData.ClientInfo;
                }
                _logger.Error(logdata);
            }
            catch
            {
                // need to handle this;
                throw;
            }
        }

        public virtual void LogEnter(string methodName, Dictionary<string, object> inputParams, string message, object parent = null)
        {
            try
            {
                var transactionObject = GetLogTnxData();
                var logdata = new EnterLoggerData
                {
                    Message = message,
                    InputParams = inputParams,
                    LoggerName = parent != null ? parent.GetType().Name : "",
                    TransactionId = transactionObject.TransactionId,
                    SessisonId = transactionObject.SessionId,
                    MethodName = methodName
                };
                if (parent != null)
                {
                    logdata.LoggerName = parent.GetType().Name;
                }
                _logger.Enter(logdata);
            }
            catch
            {
                // need to handle this;
                throw;
            }
        }

        public virtual void LogExit(string methodName, Dictionary<string, object> outputParams, object parent = null)
        {
            try
            {
                var transactionObject = GetLogTnxData();
                var logdata = new ExitLoggerData
                {
                    OutputParams = outputParams,
                    TransactionId = transactionObject.TransactionId,
                    SessisonId = transactionObject.SessionId,
                    MethodName = methodName
                };
                if (parent != null)
                {
                    logdata.LoggerName = parent.GetType().Name;
                }
                _logger.Exit(logdata);
            }
            catch
            {
                // need to handle this;
                throw;
            }
        }

        public virtual void LogDebug(string methodName, string message, Dictionary<string, object> debugParams, object parent = null)
        {
            try
            {
                var transactionObject = GetLogTnxData();
                var logdata = new DebugLoggerData
                {
                    Message = message,
                    Params = debugParams,
                    TransactionId = transactionObject.TransactionId,
                    SessisonId = transactionObject.SessionId,
                    MethodName = methodName
                };
                if (parent != null)
                {
                    logdata.LoggerName = parent.GetType().Name;
                }
                _logger.Debug(logdata);
            }
            catch
            {
                // need to handle this;
                throw;
            }
        }

        public virtual void LogInfo(string methodName, string message, Dictionary<string, object> debugParams, object parent = null)
        {
            try
            {
                var transactionObject = GetLogTnxData();
                var logdata = new InfoLoggerData
                {
                    Message = message,
                    Params = debugParams,
                    TransactionId = transactionObject.TransactionId,
                    SessisonId = transactionObject.SessionId,
                    MethodName = methodName
                };
                if (parent != null)
                {
                    logdata.LoggerName = parent.GetType().Name;
                }
                _logger.Info(logdata);
            }
            catch
            {
                // need to handle this;
                throw;
            }
        }

        public virtual void LogStep(string message, object parent = null, System.Diagnostics.Stopwatch stopwatch = null)
        {
            throw new NotImplementedException();
        }
    }
}