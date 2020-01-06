using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using YS.Sequence.Core.UnitTest;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using YS.Docker;

namespace YS.Sequence.Impl.EFCore.SqlServer
{
    [TestClass]
    public class SqlServerSequenceServiceTest: SequenceServiceTestBase
    {
        static DockerContainerSettings SqlServerDockerSetting = new DockerContainerSettings
        {
            ImageName = "mcr.microsoft.com/mssql/server:2017-CU8-ubuntu",
            Ports = new Dictionary<int, int>
            {
                [1433] = 1433
            },
            Envs=new Dictionary<string, string>
            { 
                ["ACCEPT_EULA"] ="Y",
                ["SA_PASSWORD"] = "yourStrong(!)Password"
            }
        };
        private string containerId = null;
        private IDockerContainerService dockerContainerService;

        protected override void OnSetup()
        {
            base.OnSetup();

            this.dockerContainerService = this.Get<IDockerContainerService>();
            this.containerId = this.dockerContainerService.RunAsync(SqlServerDockerSetting).Result;

            var sequenceContext = this.Get<SequenceContext>();

            sequenceContext.Database.Migrate();

        }

        protected override void OnTearDown()
        {
            if (!string.IsNullOrEmpty(containerId))
            {
                this.dockerContainerService.StopAsync(containerId).Wait();
            }
            base.OnTearDown();

        }

    }
}
