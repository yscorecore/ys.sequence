using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YS.Sequence.Impl.EFCore
{
    
    public class SequenceInfo
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(128)]
        public string Name { get; set; }
        public long StartValue { get; set; } = 1L;
        public int Step { get; set; } = 1;
        public long? EndValue { get; set; } 
        public long? CurrentValue { get; set; }
    }
}
