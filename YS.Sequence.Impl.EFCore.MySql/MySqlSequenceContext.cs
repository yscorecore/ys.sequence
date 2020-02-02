using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace YS.Sequence.Impl.EFCore.MySql
{
    [MySqlDbContextClass("SequenceContext", InjectType = typeof(SequenceContext))]
    public class MySqlSequenceContext: SequenceContext
    {
        public MySqlSequenceContext(DbContextOptions<MySqlSequenceContext> dbContextOptions) : base(dbContextOptions)
        {
        }
    }
}
