using System;
using System.Collections.Generic;
using System.Text;

namespace YS.Sequence
{
    public class SequenceInfo
    {
        public long StartValue { get; set; } = 1L;
        public int Step { get; set; } = 1;
        public long? EndValue { get; set; }
    }
}
