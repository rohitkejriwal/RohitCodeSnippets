using System;

namespace IOCInfrastructure
{
    public class TransactionTracker : ITransactionTracker
    {
        public string GetTransactionID(string sessionId)
        {
            throw new NotImplementedException();
        }

        public string SetTransactionID(string component, string sessionId)
        {
            throw new NotImplementedException();
        }
    }
}