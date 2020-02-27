using Knife.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace YS.Sequence.Impl.EFCore.MySql.UnitTest
{
    [TestClass]
    public class MySqlSequenceContextMigrationTest :KnifeHost
    {
        [TestMethod]
        public void ShouldSuccessWhenMigrationDownByStep()
        {
            this.Get<SequenceContext>().MigrateDownByStep();
        }
        [TestMethod]
        public void ShouldSuccessWhenMigrationUpByStep()
        {
            this.Get<SequenceContext>().MigrateUpByStep();
        }
    }
}
