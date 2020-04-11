using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace YS.Sequence
{
    [TypeConverter(typeof(SequenceInfoConverter))]
    public class SequenceInfo
    {
        public readonly static SequenceInfo Default = new SequenceInfo();
        public long StartValue { get; set; } = 1L;
        public int Step { get; set; } = 1;
        public long? EndValue { get; set; }
    }


}
