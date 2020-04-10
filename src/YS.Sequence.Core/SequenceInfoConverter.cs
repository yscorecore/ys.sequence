using System;
using System.ComponentModel;
using System.Globalization;

namespace YS.Sequence
{
    public class SequenceInfoConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return base.CanConvertFrom(context, sourceType) || sourceType == typeof(string);
        }
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return base.CanConvertTo(context, destinationType) || destinationType == typeof(string);
        }
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                if (object.Equals(value, string.Empty))
                {
                    return SequenceInfo.Default;
                }
                var items = (value as string).Split(',');
                if (items.Length == 0)
                {
                    return SequenceInfo.Default;
                }
                else if (items.Length == 1)
                {
                    return new SequenceInfo()
                    {
                        StartValue = long.Parse(items[0].Trim())
                    };
                }
                else if (items.Length == 2)
                {
                    return new SequenceInfo()
                    {
                        StartValue = long.Parse(items[0].Trim()),
                        Step = int.Parse(items[1].Trim())
                    };
                }
                else if (items.Length == 3)
                {
                    return new SequenceInfo()
                    {
                        StartValue = long.Parse(items[0].Trim()),
                        Step = int.Parse(items[1].Trim()),
                        EndValue = long.Parse(items[2].Trim())
                    };
                }
                else
                {
                    throw new ArgumentException($"Can not convert \"{value}\" to SequenceInfo.");
                }
            }
            return base.ConvertFrom(context, culture, value);
        }
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                var sequenceInfo = value as SequenceInfo;
                return sequenceInfo.EndValue.HasValue ? $"{sequenceInfo.StartValue},{sequenceInfo.Step},{sequenceInfo.EndValue}" : $"{sequenceInfo.StartValue},{sequenceInfo.Step}";
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
