using CommonLib.Interface;
using Consul;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Imp
{
    [Service(Lifetime = ServiceLifetime.Scoped)]
    public class ConsulService : IConsulService
    {
        private IConsulClient _client;

        public ConsulService(IConsulClient consulClient)
        {
            _client = consulClient;
        }

        public async Task<List<AgentService>> GetServicesAsync(string serviceName)
        {
            var result = await _client.Health.Service(serviceName, "", true);
            return result.Response.Select(x => x.Service).ToList();
        }

        public async Task<string> GetUrlFromServiceNameAsync(string serviceName)
        {
            var result = await _client.Health.Service(serviceName, "", true);
            var services = result.Response.Select(x => x.Service).ToList();

            if (services.Count == 0)
            {
                return string.Empty;
            }
            var service = services[new Random().Next(0, services.Count - 1)];
            var url = $"http://{service.Address}:{service.Port}";
            return url;
        }
    }
}
