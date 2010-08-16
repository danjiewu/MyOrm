using System;
using System.Collections.Generic;
using System.Text;

namespace Northwind.Business
{
    public static class ServiceProvider
    {
        private static Dictionary<Type, object> services = new Dictionary<Type, object>();
        public static T GetService<T>()
        {
            return (T)GetService(typeof(T));
        }

        public static object GetService(Type serviceType)
        {
            object service;
            services.TryGetValue(serviceType, out service);
            return service;
        }

        public static bool RegisterService(Type type, object service)
        {
            if (type.IsInstanceOfType(service)) return false;
            services[type] = service;
            return true;
        }

        public static bool UnRegisterService(Type type)
        {
            return services.Remove(type);
        }
    }
}
