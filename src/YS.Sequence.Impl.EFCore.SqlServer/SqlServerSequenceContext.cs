using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace YS.Sequence.Impl.EFCore.SqlServer
{
    [SqlServerDbContextClass("SequenceContext")]
    public class SqlServerSequenceContext : SequenceContext
    {
        public SqlServerSequenceContext(DbContextOptions<SqlServerSequenceContext> dbContextOptions) : base(dbContextOptions)
        {
        }

    }
}
