using Microsoft.EntityFrameworkCore;
using System;

namespace YS.Sequence.Impl.EFCore
{
    [ServiceClass(Lifetime = Microsoft.Extensions.DependencyInjection.ServiceLifetime.Scoped)]
    public class SequenceContextInstaller : DbContextMigrationInstaller<SequenceContext>
    {
        public SequenceContextInstaller(SequenceContext sequenceContext) : base(sequenceContext)
        {
        }

    }
}
