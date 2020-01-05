using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace YS.Sequence.Impl.EFCore
{

    public abstract class SequenceContext : DbContext
    {
        public SequenceContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }
        public virtual DbSet<SequenceInfo> Sequences { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<SequenceInfo>().HasIndex(p => p.Name).IsUnique(true);
            modelBuilder.Entity<SequenceInfo>().Property(p => p.Name).IsUnicode(false);
        }
    }
}
