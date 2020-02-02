using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using YS.Sequence.Core.UnitTest;

namespace YS.Sequence.Impl.EFCore.MySql.UnitTest
{
    [TestClass]
    public class MySqlSequenceServiceTest : SequenceServiceTestBase
    {
        protected override void OnSetup()
        {
            base.OnSetup();
            this.Get<SequenceContext>().Database.Migrate();
        }

    }
}