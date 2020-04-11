using Microsoft.EntityFrameworkCore;
using System;
using YS.Knife;

namespace YS.Sequence.Impl.EFCore
{
    [ServiceClass()]
    public class SequenceContextInstaller : DbContextMigrationInstaller<SequenceContext>
    {
        public SequenceContextInstaller(SequenceContext sequenceContext) : base(sequenceContext)
        {
        }

    }
}
