using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib
{
    public static class IServiceCollectionExtension
    {
        public static IApplicationBuilder UseConul(this IApplicationBuilder app, IConfiguration configuration)
        {
            var client = new ConsulClient(options =>
            {
                options.Address = new Uri(configuration["ConsulAddress"]); // Consul客户端地址
            });

            var registration = new AgentServiceRegistration
            {
                ID = Guid.NewGuid().ToString(), // 唯一Id
                Name = configuration["Consul:Name"], // 服务名
                Address = configuration["ip"], // 服务绑定IP
                Port = Convert.ToInt32(configuration["port"]), // 服务绑定端口
                Check = new AgentServiceCheck
                {
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5), // 服务启动多久后注册
                    Interval = TimeSpan.FromSeconds(10), // 健康检查时间间隔
                    HTTP = $"http://{configuration["ip"]}:{configuration["port"]}{configuration["Consul:HealthCheck"]}", // 健康检查地址
                    Timeout = TimeSpan.FromSeconds(5) // 超时时间
                }
            };

            // 注册服务
            client.Agent.ServiceRegister(registration).Wait();

            return app;
        }


        public static IApplicationBuilder UseGrpcConsul(this IApplicationBuilder app, IConfiguration configuration)
        {
            //    // 获取主机生命周期管理接口
            //    var lifetime = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();

            ConsulClient client = new ConsulClient(c =>
            {
                c.Address = new Uri(configuration["ConsulAddress"]);
                c.Datacenter = "dc1";
            });
            string ip = configuration["ip"]; //优先接收变量的值
            string port = configuration["port"]; //优先接收变量的值
            //string currentIp = configuration["Consul:currentIP"];
            //string currentPort = configuration["Consul:currentPort"];

            //ip = string.IsNullOrEmpty(ip) ? currentIp : ip; //当前程序的IP
            //port = string.IsNullOrEmpty(port) ? currentPort : port; //当前程序的端口
            string serviceId = $"service:{ip}:{port}";//服务ID，一个服务是唯一的
            //服务注册
            client.Agent.ServiceRegister(new AgentServiceRegistration()
            {
                ID = serviceId, //唯一的
                Name = configuration["Consul:Name"], //组名称-Group
                Address = ip, //ip地址
                Port = int.Parse(port), //端口
                Tags = new string[] { "api站点" },
                Check = new AgentServiceCheck()
                {
                    Interval = TimeSpan.FromSeconds(10),//多久检查一次心跳
                    GRPC = $"{ip}:{port}", //gRPC注册特有
                    GRPCUseTLS = false,//支持http
                    Timeout = TimeSpan.FromSeconds(5),//超时时间
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5) //服务停止多久后注销服务
                }

            }).Wait();
            ////应用程序终止时,注销服务
            //lifetime.ApplicationStopping.Register(() =>
            //{
            //    client.Agent.ServiceDeregister(serviceId).Wait();
            //});
            return app;
        }

        public static void BatchRegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            var allAssembly = GetAllAssembly();
            services.AddSingleton<IConsulClient>(new ConsulClient(x => {
                x.Address = new Uri(configuration["ConsulAddress"]);
            }));
            services.RegisterServiceByAttribute(ServiceLifetime.Singleton, allAssembly);
            services.RegisterServiceByAttribute(ServiceLifetime.Scoped, allAssembly);
            services.RegisterServiceByAttribute(ServiceLifetime.Transient, allAssembly);

        }

        /// <summary>
            /// 通过 ServiceAttribute 批量注册服务
            /// </summary>
            /// <param name="services"></param>
            /// <param name="serviceLifetime"></param>
        private static void RegisterServiceByAttribute(this IServiceCollection services, ServiceLifetime serviceLifetime, List<Assembly> allAssembly)
        {

            List<Type> types = allAssembly.SelectMany(t => t.GetTypes()).Where(t => t.GetCustomAttributes(typeof(ServiceAttribute), false).Length > 0 && t.GetCustomAttribute<ServiceAttribute>()?.Lifetime == serviceLifetime && t.IsClass && !t.IsAbstract).ToList();

            foreach (var type in types)
            {

                Type? typeInterface = type.GetInterfaces().FirstOrDefault();

                if (typeInterface == null)
                {
                    //服务非继承自接口的直接注入
                    switch (serviceLifetime)
                    {
                        case ServiceLifetime.Singleton: services.AddSingleton(type); break;
                        case ServiceLifetime.Scoped: services.AddScoped(type); break;
                        case ServiceLifetime.Transient: services.AddTransient(type); break;
                    }
                }
                else
                {
                    //服务继承自接口的和接口一起注入
                    switch (serviceLifetime)
                    {
                        case ServiceLifetime.Singleton: services.AddSingleton(typeInterface, type); break;
                        case ServiceLifetime.Scoped: services.AddScoped(typeInterface, type); break;
                        case ServiceLifetime.Transient: services.AddTransient(typeInterface, type); break;
                    }
                }

            }

        }


        /// <summary>
            /// 获取全部 Assembly
            /// </summary>
            /// <returns></returns>
        private static List<Assembly> GetAllAssembly()
        {

            var allAssemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();

            HashSet<string> loadedAssemblies = new();

            foreach (var item in allAssemblies)
            {
                loadedAssemblies.Add(item.FullName!);
            }

            Queue<Assembly> assembliesToCheck = new();
            assembliesToCheck.Enqueue(Assembly.GetEntryAssembly()!);

            while (assembliesToCheck.Any())
            {
                var assemblyToCheck = assembliesToCheck.Dequeue();
                foreach (var reference in assemblyToCheck!.GetReferencedAssemblies())
                {
                    if (!loadedAssemblies.Contains(reference.FullName))
                    {
                        var assembly = Assembly.Load(reference);

                        assembliesToCheck.Enqueue(assembly);

                        loadedAssemblies.Add(reference.FullName);

                        allAssemblies.Add(assembly);
                    }
                }
            }
            return allAssemblies;
        }
    }
}
