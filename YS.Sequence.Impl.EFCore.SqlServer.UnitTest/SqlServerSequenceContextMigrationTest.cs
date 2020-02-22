using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using YS.Knife.EntityFrameworkCore.MSTest;

namespace YS.Sequence.Impl.EFCore.SqlServer.UnitTest
{
    [TestClass]
    public class SqlServerSequenceContextMigrationTest:MigrationTestBase<SequenceContext>
    {

      
    }
}
