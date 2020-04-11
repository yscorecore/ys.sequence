using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using YS.Knife.Hosting;

namespace YS.Sequence.Impl.EFCore.SqlServer.UnitTest
{
    [TestClass]
    public class SqlServerSequenceContextMigrationTest : KnifeHost
    {
        [TestMethod]
        public void ShouldSuccessWhenMigrationDownByStep()
        {
            this.GetService<SequenceContext>().MigrateDownByStep();
        }
        [TestMethod]
        public void ShouldSuccessWhenMigrationUpByStep()
        {
            this.GetService<SequenceContext>().MigrateUpByStep();
        }

    }
}
