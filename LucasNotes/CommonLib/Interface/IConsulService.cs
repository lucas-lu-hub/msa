using Consul;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Interface
{
    public interface IConsulService
    {
        Task<List<AgentService>> GetServicesAsync(string serviceName);

        Task<string> GetUrlFromServiceNameAsync(string serviceName);
    }
}
