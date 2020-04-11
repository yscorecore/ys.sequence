using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using YS.Sequence.Core.UnitTest;

namespace YS.Sequence.Impl.EFCore.SqlServer
{
    [TestClass]
    public class SqlServerSequenceServiceTest : SequenceServiceTestBase
    {
        public SqlServerSequenceServiceTest()
        {
            this.GetService<SequenceContext>().Database.Migrate();
        }

    }
}
