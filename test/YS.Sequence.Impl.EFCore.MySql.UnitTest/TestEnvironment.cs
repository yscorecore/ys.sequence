using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using YS.Knife.Test;

namespace YS.Sequence.Impl.EFCore.MySql.UnitTest
{
    [TestClass]
    public class TestEnvironment
    {
        [AssemblyInitialize()]
        public static void Setup(TestContext _)
        {
            var availablePort = Utility.GetAvailableTcpPort(3306);
            var password = Utility.NewPassword(32);
            StartContainer(availablePort, password);
            SetConnectionString(availablePort, password);
        }

        [AssemblyCleanup()]
        public static void TearDown()
        {
            DockerCompose.Down();
        }


        private static void StartContainer(uint port, string password)
        {
            DockerCompose.Up(new Dictionary<string, object>
            {
                ["MYSQL_PORT"] = port,
                ["MYSQL_ROOT_PASSWORD"] = password
            });
        }
        private static void SetConnectionString(uint port, string password)
        {
            var mySqlConnectionStringBuilder = new MySqlConnectionStringBuilder
            {
                Server = "127.0.0.1",
                Port = port,
                Database = "SequenceContext",
                UserID = "root",
                Password = password
            };
            Environment.SetEnvironmentVariable("ConnectionStrings__@DbType", "mysql");
            Environment.SetEnvironmentVariable("ConnectionStrings__SequenceContext", mySqlConnectionStringBuilder.ConnectionString);
        }




    }
}
