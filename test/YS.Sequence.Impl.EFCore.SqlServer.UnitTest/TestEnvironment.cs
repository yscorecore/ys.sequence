using Knife.Test;
using Microsoft.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YS.Knife.Test;

namespace YS.Sequence.Impl.EFCore.SqlServer
{
    [TestClass]
    public class TestEnvironment
    {
        [AssemblyInitialize()]
        public static void Setup(TestContext assemblyTestContext)
        {
            var availablePort = Utility.GetAvailableTcpPort(1433);
            var password = Utility.NewPassword();
            StartContainer(availablePort, password);
            SetConnectionString(availablePort, password);
        }

        [AssemblyCleanup()]
        public static void TearDown()
        {
            DockerCompose.Down();
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
            DockerCompose.Up(new Dictionary<string, object>
            {
                ["SQLSERVER_PORT"] = port,
                ["SA_PASSWORD"] = password
            });
            // delay 90s ,wait for sql server container ready
            Task.Delay(90000).Wait();
        }


    }
}
