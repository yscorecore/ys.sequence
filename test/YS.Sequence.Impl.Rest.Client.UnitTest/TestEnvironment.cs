using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using YS.Knife.Test;

namespace YS.Sequence.Impl.EFCore.SqlServer
{
    [TestClass]
    public class TestEnvironment
    {

        [AssemblyInitialize()]
        public static void Setup(TestContext _)
        {
            var availablePort = Utility.GetAvailableTcpPort(8080);
            SetServiceBaseAddress(availablePort);
            StartContainer(availablePort);
        }

        [AssemblyCleanup()]
        public static void TearDown()
        {
            DockerCompose.Down();
        }


        private static void SetServiceBaseAddress(uint port)
        {
            Environment.SetEnvironmentVariable("ApiServices__Services__SequenceService__BaseAddress", $"http://127.0.0.1:{port}");
        }
        private static void StartContainer(uint port)
        {
            DockerCompose.Up(new Dictionary<string, object>
            {
                ["API_PORT"] = port,
            });
            // delay 90s ,wait for container ready
            Task.Delay(90000).Wait();
        }


    }
}
