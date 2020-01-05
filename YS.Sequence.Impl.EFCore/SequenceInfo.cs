using System;

namespace YS.Sequence.Impl.EFCore
{

    public class SequenceInfo
    {
        public Guid Id { get; set; }
        public long StartValue { get; set; } = 1L;
        public int Step { get; set; } = 1;
        public long? EndValue { get; set; } 
    }
}
