using System.Collections.Generic;
using System.Reflection;
using System.Web.Http.Filters;

namespace IOCInfrastructure.MVC
{
    public class IOCLogFilterProvider : IFilterProvider
    {
        private IServiceResolver _serviceResolver;
        private readonly ActionDescriptorFilterProvider _defaultProvider = new ActionDescriptorFilterProvider();

        public IOCLogFilterProvider(IServiceResolver serviceResolver)
        {
            _serviceResolver = serviceResolver;
        }

        public IEnumerable<FilterInfo> GetFilters(System.Web.Http.HttpConfiguration configuration, System.Web.Http.Controllers.HttpActionDescriptor actionDescriptor)
        {
            var attributes = _defaultProvider.GetFilters(configuration, actionDescriptor);

            foreach (var filterAttrs in attributes)
            {
                foreach (var property in filterAttrs.Instance.GetType().GetProperties())
                {
                    var propAttr = (property as PropertyInfo).GetCustomAttribute(typeof(IOCDependencyAttribute));

                    if (propAttr != null)
                    {
                        var data = _serviceResolver.GetInstance((property.PropertyType).Assembly.GetType((property.PropertyType).FullName));
                        property.SetValue(filterAttrs.Instance, data, null);
                    }
                }
            }
            return attributes;
        }
    }
}