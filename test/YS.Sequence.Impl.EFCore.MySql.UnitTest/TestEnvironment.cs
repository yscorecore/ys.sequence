using YS.Knife.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using YS.Knife.Test;

namespace YS.Sequence.Impl.EFCore.MySql.UnitTest
{
    [TestClass]
    public class TestEnvironment
    {
        [AssemblyInitialize()]
        public static void Setup(TestContext assemblyTestContext)
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
            // delay 90s ,wait for mysql container ready
            Task.Delay(90000).Wait();
        }
        private static void SetConnectionString(uint port, string password)
        {
            var mySqlConnectionStringBuilder = new MySqlConnectionStringBuilder();
            mySqlConnectionStringBuilder.Server = "127.0.0.1";
            mySqlConnectionStringBuilder.Port = port;
            mySqlConnectionStringBuilder.Database = "SequenceContext";
            mySqlConnectionStringBuilder.UserID = "root";
            mySqlConnectionStringBuilder.Password = password;
            Environment.SetEnvironmentVariable("ConnectionStrings__@DbType", "mysql");
            Environment.SetEnvironmentVariable("ConnectionStrings__SequenceContext", mySqlConnectionStringBuilder.ConnectionString);
        }




    }
}
