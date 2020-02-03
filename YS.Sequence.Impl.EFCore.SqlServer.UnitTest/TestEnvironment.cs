using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using YS.Docker;

namespace YS.Sequence.Impl.EFCore.SqlServer
{
    [TestClass]
    public class TestEnvironment
    {
        private static IHost host;
        private static string containerId;

        [AssemblyInitialize()]
        public static void Setup(TestContext assemblyTestContext)
        {
            var availablePort = GetAvailableTcpPort(1433);
            var password = RandomUtility.RandomCode(32);
            SetConnectionString(availablePort, password);
            StartContainer(availablePort, password);
        }

        [AssemblyCleanup()]
        public static void TearDown()
        {
            if (!string.IsNullOrEmpty(containerId))
            {
                var dockerContainerService = host.Services.GetRequiredService<IDockerContainerService>();
                dockerContainerService.StopAsync(containerId).Wait();
                Console.WriteLine($"Stop docker container {containerId} success.");
                containerId = null;
                host.Dispose();
                host = null;
            }
        }


        private static void SetConnectionString(uint port, string password)
        {
            SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder();
            sqlConnectionStringBuilder.DataSource = $"127.0.0.1,{port}";
            sqlConnectionStringBuilder.InitialCatalog = "SequenceContext";
            sqlConnectionStringBuilder.UserID = "sa";
            sqlConnectionStringBuilder.Password = password;
            Environment.SetEnvironmentVariable("ConnectionStrings__SequenceContext", sqlConnectionStringBuilder.ConnectionString);
        }
        private static void StartContainer(uint port, string password)
        {
            var dockerSetting = new DockerContainerSettings
            {
                ImageName = "mcr.microsoft.com/mssql/server:2019-latest",
                Ports = new Dictionary<int, int>
                {
                    [1433] = Convert.ToInt32(port)
                },
                Envs = new Dictionary<string, string>
                {
                    ["ACCEPT_EULA"] = "Y",
                    ["SA_PASSWORD"] = password
                }
            };

            host = Knife.Hosting.Host.CreateHost();
            var dockerContainerService = host.Services.GetRequiredService<IDockerContainerService>();
            containerId = dockerContainerService.RunAsync(dockerSetting).Result;
            Console.WriteLine($"Start docker container {containerId} success.");
            // delay 90s ,wait for mysql server ready
            Task.Delay(90000).Wait();
        }

        private static uint GetAvailableTcpPort(uint start = 1024, uint stop = IPEndPoint.MaxPort)
        {
            IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] endPoints = ipGlobalProperties.GetActiveTcpListeners();
            for (uint i = start; i <= stop; i++)
            {
                if (!endPoints.Any(p => p.Port == i))
                {
                    return i;
                }
            }
            throw new ApplicationException("Not able to find a free TCP port.");
        }
    }
}
