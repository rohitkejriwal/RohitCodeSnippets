using System.Web.Http.Dependencies;

namespace IOCInfrastructure.MVC
{
    public interface IMVCDependencyResolver
    {
        IDependencyResolver GetWebApiDependencyResolver();

        System.Web.Mvc.IDependencyResolver GetMVCDependencyResolver();
    }
}