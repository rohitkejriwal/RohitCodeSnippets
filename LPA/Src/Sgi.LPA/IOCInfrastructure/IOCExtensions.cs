using Logger.Core;

namespace IOCInfrastructure
{
    public static class IOCExtensions
    {
        public static void SetServiceTransactionData(this IServiceTransactionData serviceRequestData, string SessionTxnId, object parent)
        {
            TransactionLoggerData txnDataSet = new TransactionLoggerData();
            txnDataSet.SessionId = SessionTxnId;
            txnDataSet.TransactionId = string.Format("{0}-{1}", Utility.GetTimeStamp(), Utility.GetRandomString(5));
            txnDataSet.Component = parent.GetType().FullName;
            serviceRequestData.Set(IOCInfrastructure.Consts.TransactionDataKey, txnDataSet);
        }
    }
}