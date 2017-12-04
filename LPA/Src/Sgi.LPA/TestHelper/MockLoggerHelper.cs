using IOCInfrastructure;
using Logger.Core;
using Logger.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestHelper
{
    public class MockLoggerHelper : LogWriteHelper
    {
        public MockLoggerHelper(IServiceResolver serviceResolver)
            : base(serviceResolver)
        {
            //_serviceResolver = serviceResolver;
            //_logger = _serviceResolver.GetInstance<ILogger>();
        }

        protected override TransactionLoggerData GetLogTnxData()
        {
            TransactionLoggerData txnLogData = new TransactionLoggerData();
            return txnLogData;
        }

        public override void LogStep(string message, object parent = null, System.Diagnostics.Stopwatch stopwatch = null)
        {
            //try
            //{
            //    var transactionObject = GetLogTnxData();
            //    var txnData = _serviceResolver.GetInstance<IServiceTransactionData>();
            //    if (stopwatch == null)
            //    {
            //        stopwatch = txnData.GetStopwatch();
            //    }

            //    int step = txnData.GetTransactionStep();
            //    txnData.UpdateTransactionStep();
            //    var formattedMessage = string.Format("Step:{0}. {1}. Process time:{2}ms", step, message, stopwatch.ElapsedMilliseconds);
            //    string logger = string.Empty;
            //    if (parent != null)
            //    {
            //        logger = parent.GetType().Name;
            //    }
            //    else
            //    {
            //    }
            //    var logdata = new ExecutionStepLoggerData
            //    {
            //        Message = formattedMessage,
            //        TransactionId = transactionObject.TransactionId,
            //        SessisonId = transactionObject.SessionId,
            //        LoggerName = logger,
            //        Step = step
            //    };
            //    if (parent != null)
            //    {
            //        logdata.LoggerName = parent.GetType().Name;
            //    }
            //    _logger.Timespan(logdata);
            //}
            //catch
            //{
            //    // need to handle this;
            //    throw;
            // }
        }
    }


}
