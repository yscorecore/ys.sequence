using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YS.Docker;

namespace YS.Sequence.Impl.EFCore.SqlServer
{
   [TestClass]
   public  class TestEnvironment
    {
        private static IHost host;
        private static string containerId;
        static DockerContainerSettings SqlServerDockerSetting = new DockerContainerSettings
        {
            ImageName = "mcr.microsoft.com/mssql/server:2019-latest",
            Ports = new Dictionary<int, int>
            {
                [1433] = 1433
            },
            Envs = new Dictionary<string, string>
            {
                ["ACCEPT_EULA"] = "Y",
                ["SA_PASSWORD"] = "yourStrong(!)Password"
            }
        };

        [AssemblyInitialize()]
        public static void Setup(TestContext assemblyTestContext)
        {
            host = Knife.Hosting.Host.CreateHost();
            var dockerContainerService = host.Services.GetRequiredService<IDockerContainerService>();
            containerId = dockerContainerService.RunAsync(SqlServerDockerSetting).Result;
            Console.WriteLine($"Start docker container {containerId} success.");
            // delay 90s ,wait for sql server ready
            Task.Delay(90000).Wait();
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
    }
}
