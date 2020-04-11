using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using YS.Knife.Hosting;

namespace YS.Sequence.Impl.EFCore.MySql.UnitTest
{
    [TestClass]
    public class MySqlSequenceContextMigrationTest : KnifeHost
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
