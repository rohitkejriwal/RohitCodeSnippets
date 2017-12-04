namespace IOCInfrastructure
{
    public interface ITransactionTracker
    {
        string GetTransactionID(string sessionId);

        string SetTransactionID(string component, string sessionId);
    }
}