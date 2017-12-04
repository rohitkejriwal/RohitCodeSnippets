namespace IOCInfrastructure.MVC
{
    public interface IContainerProviderAccessor
    {
        IServiceResolver ServiceResolver { get; }
    }
}