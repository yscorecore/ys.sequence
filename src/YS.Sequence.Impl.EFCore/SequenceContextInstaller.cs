using Microsoft.EntityFrameworkCore;
using System;

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
