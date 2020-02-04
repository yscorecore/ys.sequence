using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace YS.Sequence.Core.UnitTest
{
    [TestClass]
    public class SequenceInfoConverterTest
    {
        TypeConverter converter = TypeDescriptor.GetConverter(typeof(SequenceInfo));

        [TestMethod]
        public void ShouldBeSequenceInfoConverter()
        {
            Assert.AreEqual(typeof(SequenceInfoConverter), converter.GetType());
        }

        [TestMethod]
        public void CanConvertToString()
        {
            Assert.IsTrue(converter.CanConvertTo(typeof(string)));
        }

        [DataTestMethod]
        [DataRow(1, 1, null, "1,1")]
        [DataRow(1, -1, null, "1,-1")]
        [DataRow(-1, -1, null, "-1,-1")]
        [DataRow(1, 1, 1L, "1,1,1")]
        public void ShouldSuccessWhenConvertToString(long startValue, int step, long? endValue, string expected)
        {
            var sequence = new SequenceInfo()
            {
                StartValue = startValue,
                Step = step,
                EndValue = endValue
            };
            var actual = converter.ConvertToString(sequence);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CanConvertFromString()
        {
            Assert.IsTrue(converter.CanConvertFrom(typeof(string)));
        }

        [DataTestMethod]
        [DataRow(1, 1, null, "")]
        [DataRow(2, 1, null, "2")]
        [DataRow(3, 2, null, "3,2")]
        [DataRow(4, 5, 100L, "4,5,100")]
        [DataRow(1, -1, null, "1,-1")]
        [DataRow(-1, -1, null, "-1,-1")]
        public void ShouldSuccessWhenConvertFromString(long startValue, int step, long? endValue, string text)
        {
            var sequence = (SequenceInfo)converter.ConvertFromString(text);
            Assert.AreEqual(startValue, sequence.StartValue);
            Assert.AreEqual(step, sequence.Step);
            Assert.AreEqual(endValue, sequence.EndValue);
        }
    }
}
