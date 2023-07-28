using Consul;

namespace LucasNotes.Identity
{
    public static class IServiceCollectionExtension
    {
        public static IApplicationBuilder UseConul(this IApplicationBuilder app, IConfiguration configuration)
        {
            var client = new ConsulClient(options =>
            {
                options.Address = new Uri(configuration["Consul:Address"]); // Consul客户端地址
            });

            var registration = new AgentServiceRegistration
            {
                ID = Guid.NewGuid().ToString(), // 唯一Id
                Name = configuration["Consul:Name"], // 服务名
                Address = configuration["Consul:Ip"], // 服务绑定IP
                Port = Convert.ToInt32(configuration["Consul:Port"]), // 服务绑定端口
                Check = new AgentServiceCheck
                {
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5), // 服务启动多久后注册
                    Interval = TimeSpan.FromSeconds(10), // 健康检查时间间隔
                    HTTP = $"http://{configuration["Consul:Ip"]}:{configuration["Consul:Port"]}{configuration["Consul:HealthCheck"]}", // 健康检查地址
                    Timeout = TimeSpan.FromSeconds(5) // 超时时间
                }
            };

            // 注册服务
            client.Agent.ServiceRegister(registration).Wait();

            return app;
        }
    }
}
