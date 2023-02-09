using Castle.DynamicProxy;
using MultiServiceDemo.AOP;
using MultiServiceDemo.DI;
using MultiServiceDemo.Filters;
using MultiServiceDemo.Help;
using System.Reflection;

namespace MultiServiceDemo.Extention
{
    public static class Extention_Service
    {
        private static readonly ProxyGenerator _generator = new ProxyGenerator();
        private static string _serviceConfig;

        /// <summary>
        /// 自动注入拥有ITransientDependency,IScopeDependency或ISingletonDependency的类
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <returns></returns>
        public static IServiceCollection AddFxServices(this IServiceCollection services)
        {

            _serviceConfig = Appsettings.GetValue("serviceConfig");
            Dictionary<Type, ServiceLifetime> lifeTimeMap = new Dictionary<Type, ServiceLifetime>
            {
                { typeof(ITransientDependency), ServiceLifetime.Transient},
                { typeof(IScopedDependency),ServiceLifetime.Scoped},
                { typeof(ISingletonDependency),ServiceLifetime.Singleton}
            };

            GlobalAssemblies.AllTypes.ForEach(aType =>
            {
                lifeTimeMap.ToList().ForEach(aMap =>
                {
                    var theDependency = aMap.Key;
                    if (theDependency.IsAssignableFrom(aType) && theDependency != aType && !aType.IsAbstract && aType.IsClass)
                    {
                        //获取服务特性值
                        var serviceAttribute = aType.GetCustomAttribute<ServiceFilterAttribute>();
                        if (serviceAttribute != null && !string.IsNullOrEmpty(serviceAttribute._serviceName) && serviceAttribute._serviceName == _serviceConfig)
                        {
                            //注入实现
                            services.Add(new ServiceDescriptor(aType, aType, aMap.Value));
                            var interfaces = GlobalAssemblies.AllTypes.Where(x => x.IsAssignableFrom(aType) && x.IsInterface && x != theDependency).ToList();
                            //有接口则注入接口
                            if (interfaces.Count > 0)
                            {
                                interfaces.ForEach(aInterface =>
                                {
                                    //注入AOP
                                    services.Add(new ServiceDescriptor(aInterface, serviceProvider =>
                                    {
                                        CastleInterceptor castleInterceptor = new CastleInterceptor(serviceProvider);
                                        return _generator.CreateInterfaceProxyWithTarget(aInterface, serviceProvider.GetService(aType), castleInterceptor);
                                    }, aMap.Value));
                                });
                            }
                            //无接口则注入自己
                            else
                            {
                                services.Add(new ServiceDescriptor(aType, aType, aMap.Value));
                            }
                        }
                    }
                });
            });
            return services;
        }

        /// <summary>
        /// 给IEnumerable拓展ForEach方法
        /// </summary>
        /// <typeparam name="T">模型类</typeparam>
        /// <param name="iEnumberable">数据源</param>
        /// <param name="func">方法</param>
        public static void ForEach<T>(this IEnumerable<T> iEnumberable, Action<T> func)
        {
            foreach (var item in iEnumberable)
            {
                func(item);
            }
        }
    }
}
