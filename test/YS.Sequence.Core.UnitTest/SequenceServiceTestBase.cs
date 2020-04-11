using YS.Knife.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace YS.Sequence.Core.UnitTest
{
    public abstract class SequenceServiceTestBase : KnifeHost
    {
        public SequenceServiceTestBase()
        {
            this.TestObject = GetService<ISequenceService>();
        }
        private ISequenceService TestObject;
        #region Create
        [TestCategory("Create")]
        [TestMethod]
        public async Task ShouldCreateSequenceWhenCreateSequenceAndGivenValidArguments()
        {
            var name = RandomUtility.RandomCode(10);
            Assert.IsFalse(await this.TestObject.ExistsAsync(name));
            await this.TestObject.CreateSequence(name, SequenceInfo.Default);
            Assert.IsTrue(await this.TestObject.ExistsAsync(name));
        }
        //[TestCategory("Create")]
        //[TestMethod]
        //public async Task ShouldThrowExceptionWhenCreateSequenceAndNameHasExists()
        //{
        //    var name = RandomUtility.RandomCode(10);
        //    await this.TestObject.CreateSequence(name, SequenceInfo.Default);
        //    var ex = await Assert.ThrowsExceptionAsync<Exception>(() => this.TestObject.CreateSequence(name, SequenceInfo.Default));
        //}
        [TestCategory("Create")]
        [TestMethod]
        public async Task ShouldUseDefaultSequenceWhenCreateSequenceAndGivenSequenceInfoIsNull()
        {
            var name = RandomUtility.RandomCode(10);
            await this.TestObject.CreateSequence(name, null);
            var sequence = await this.TestObject.GetSequence(name);
            Assert.AreEqual(SequenceInfo.Default.StartValue, sequence.StartValue);
            Assert.AreEqual(SequenceInfo.Default.Step, sequence.Step);
            Assert.AreEqual(SequenceInfo.Default.EndValue, sequence.EndValue);
        }
        //[TestCategory("Create")]
        //[TestMethod]
        //public async Task ShouldThrowArgumentNullArgumentExceptionWhenCreateSequenceAndGivenNameIsNull()
        //{
        //    var name = RandomUtility.RandomCode(10);
        //    await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => this.TestObject.CreateSequence(null, new SequenceInfo()));
        //}
        #endregion

        #region Remove

        [TestCategory("Remove")]
        [TestMethod]
        public async Task ShouldReturnTrueWhenRemoveExistsSequence()
        {
            var name = RandomUtility.RandomCode(10);
            await this.TestObject.CreateSequence(name, SequenceInfo.Default);
            var removeResult = await this.TestObject.Remove(name);
            Assert.IsTrue(removeResult);
            Assert.IsFalse(await this.TestObject.ExistsAsync(name));
        }
        [TestCategory("Remove")]
        [TestMethod]
        public async Task ShouldReturnFalseWhenRemoveNoExistsSequence()
        {
            var name = RandomUtility.RandomCode(10);
            var removeResult = await this.TestObject.Remove(name);
            Assert.IsFalse(removeResult);
        }
        #endregion

        #region GetSequence

        [TestCategory("GetSequence")]
        [TestMethod]
        public async Task ShouldReturnNullWhenGetNoExistsSequence()
        {
            var name = RandomUtility.RandomCode(10);
            var sequenceInfo = await this.TestObject.GetSequence(name);
            Assert.IsNull(sequenceInfo);
        }
        [TestCategory("GetSequence")]
        [TestMethod]
        public async Task ShouldReturnSequenceInfoWhenGetExistsSequence()
        {
            var name = RandomUtility.RandomCode(10);
            await this.TestObject.CreateSequence(name, new SequenceInfo() { StartValue = 100, Step = 5, EndValue = 200 });
            var sequenceInfo = await this.TestObject.GetSequence(name);
            Assert.IsNotNull(sequenceInfo);
            Assert.AreEqual(100, sequenceInfo.StartValue);
            Assert.AreEqual(5, sequenceInfo.Step);
            Assert.AreEqual(200, sequenceInfo.EndValue);

        }
        #endregion

        #region Reset
        [TestCategory("Reset")]
        [TestMethod]

        public async Task ShouldReturnStartValueWhenResetExistsSequenceAndGetValue()
        {
            var name = RandomUtility.RandomCode(10);
            await this.TestObject.CreateSequence(name, SequenceInfo.Default);
            await this.TestObject.GetValue(name); //return 1
            await this.TestObject.GetValue(name); //return 2
            var res = await this.TestObject.Reset(name);
            Assert.IsTrue(res);
            // after reset and get value, it should be the default value
            var value = await this.TestObject.GetValue(name);
            Assert.AreEqual(SequenceInfo.Default.StartValue, value);
        }

        [TestCategory("Reset")]
        [TestMethod]

        public async Task ShouldReturnFalseWhenResetNoExistsSequence()
        {
            var name = RandomUtility.RandomCode(10);
            var res = await this.TestObject.Reset(name);
            Assert.IsFalse(res);
        }
        #endregion

        #region GetValue
        [TestCategory("GetValue")]
        [TestMethod]

        public async Task ShouldReturnStartValueWhenGetNewSequenceValue()
        {
            var name = RandomUtility.RandomCode(10);
            await this.TestObject.CreateSequence(name, new SequenceInfo() { StartValue = 100 });
            var value = await this.TestObject.GetValue(name);
            Assert.AreEqual(100, value);
        }
        [TestCategory("GetValue")]
        [TestMethod]
        public async Task ShouldAwaysReturnStartValueWhenGetSequenceValueAndStepIsZero()
        {
            var name = RandomUtility.RandomCode(10);
            await this.TestObject.CreateSequence(name, new SequenceInfo() { StartValue = 999, Step = 0 });
            Assert.AreEqual(999, await this.TestObject.GetValue(name));
            Assert.AreEqual(999, await this.TestObject.GetValue(name));
            Assert.AreEqual(999, await this.TestObject.GetValue(name));
        }

        [TestCategory("GetValue")]
        [TestMethod]
        public async Task ShouldIncreaseValueWhenGetSequenceValueAndStepGreatThanZero()
        {
            var name = RandomUtility.RandomCode(10);
            await this.TestObject.CreateSequence(name, new SequenceInfo() { StartValue = 100, Step = 5 });
            Assert.AreEqual(100, await this.TestObject.GetValue(name));
            Assert.AreEqual(105, await this.TestObject.GetValue(name));
            Assert.AreEqual(110, await this.TestObject.GetValue(name));
            Assert.AreEqual(115, await this.TestObject.GetValue(name));
        }

        [TestCategory("GetValue")]
        [TestMethod]
        public async Task ShouldReuseStartValueWhenGetSequenceValueAndStepGreatThanZeroAndCurrentValueGreatThanEndValue()
        {
            var name = RandomUtility.RandomCode(10);
            await this.TestObject.CreateSequence(name, new SequenceInfo() { StartValue = 100, Step = 5, EndValue = 110 });
            Assert.AreEqual(100, await this.TestObject.GetValue(name));
            Assert.AreEqual(105, await this.TestObject.GetValue(name));
            Assert.AreEqual(110, await this.TestObject.GetValue(name));
            Assert.AreEqual(100, await this.TestObject.GetValue(name));
            Assert.AreEqual(105, await this.TestObject.GetValue(name));
        }

        [TestCategory("GetValue")]
        [TestMethod]
        public async Task ShouldDecreaseValueWhenGetSequenceValueAndStepLessThanZero()
        {
            var name = RandomUtility.RandomCode(10);
            await this.TestObject.CreateSequence(name, new SequenceInfo() { StartValue = 100, Step = -5 });
            Assert.AreEqual(100, await this.TestObject.GetValue(name));
            Assert.AreEqual(95, await this.TestObject.GetValue(name));
            Assert.AreEqual(90, await this.TestObject.GetValue(name));
            Assert.AreEqual(85, await this.TestObject.GetValue(name));
        }

        [TestCategory("GetValue")]
        [TestMethod]
        public async Task ShouldReuseStartValueWhenGetSequenceValueAndStepLessThanZeroAndCurrentValueLessThanEndValue()
        {
            var name = RandomUtility.RandomCode(10);
            await this.TestObject.CreateSequence(name, new SequenceInfo() { StartValue = 100, Step = -5, EndValue = 90 });
            Assert.AreEqual(100, await this.TestObject.GetValue(name));
            Assert.AreEqual(95, await this.TestObject.GetValue(name));
            Assert.AreEqual(90, await this.TestObject.GetValue(name));
            Assert.AreEqual(100, await this.TestObject.GetValue(name));
            Assert.AreEqual(95, await this.TestObject.GetValue(name));
        }
        #endregion

        #region GetValueOrCreate

        [TestCategory("GetValueOrCreate")]
        [TestMethod]
        public async Task ShouldReturnStartValueWhenGetValueOrCreateNewSequence()
        {
            var name = RandomUtility.RandomCode(10);
            var value = await this.TestObject.GetOrCreateValue(name, new SequenceInfo() { StartValue = 100, Step = 2, EndValue = 200 });
            Assert.AreEqual(100, value);
            var sequenceInfo = await this.TestObject.GetSequence(name);
            Assert.AreEqual(100, sequenceInfo.StartValue);
            Assert.AreEqual(2, sequenceInfo.Step);
            Assert.AreEqual(200, sequenceInfo.EndValue);
        }

        [TestCategory("GetValueOrCreate")]
        [TestMethod]
        public async Task ShouldReturnNextValueWhenGetValueOrCreateExistsSequence()
        {
            var name = RandomUtility.RandomCode(10);
            await this.TestObject.CreateSequence(name, SequenceInfo.Default);
            var value = await this.TestObject.GetOrCreateValue(name, new SequenceInfo() { StartValue = 100, Step = 2, EndValue = 200 });
            Assert.AreEqual(1, value);
            var sequenceInfo = await this.TestObject.GetSequence(name);
            Assert.AreEqual(1, sequenceInfo.StartValue);
            Assert.AreEqual(1, sequenceInfo.Step);
            Assert.AreEqual(null, sequenceInfo.EndValue);
        }




        [TestCategory("GetValueOrCreate")]
        [TestMethod]
        public async Task ShouldAwaysReturnStartValueWhenGetOrCreateSequenceValueAndStepIsZero()
        {
            var name = RandomUtility.RandomCode(10);
            await this.TestObject.CreateSequence(name, new SequenceInfo() { StartValue = 999, Step = 0 });
            Assert.AreEqual(999, await this.TestObject.GetOrCreateValue(name, SequenceInfo.Default));
            Assert.AreEqual(999, await this.TestObject.GetOrCreateValue(name, SequenceInfo.Default));
            Assert.AreEqual(999, await this.TestObject.GetOrCreateValue(name, SequenceInfo.Default));
        }

        [TestCategory("GetValueOrCreate")]
        [TestMethod]
        public async Task ShouldIncreaseValueWhenGetOrCreateSequenceValueAndStepGreatThanZero()
        {
            var name = RandomUtility.RandomCode(10);
            await this.TestObject.CreateSequence(name, new SequenceInfo() { StartValue = 100, Step = 5 });
            Assert.AreEqual(100, await this.TestObject.GetOrCreateValue(name, SequenceInfo.Default));
            Assert.AreEqual(105, await this.TestObject.GetOrCreateValue(name, SequenceInfo.Default));
            Assert.AreEqual(110, await this.TestObject.GetOrCreateValue(name, SequenceInfo.Default));
            Assert.AreEqual(115, await this.TestObject.GetOrCreateValue(name, SequenceInfo.Default));
        }

        [TestCategory("GetValueOrCreate")]
        [TestMethod]
        public async Task ShouldReuseStartValueWhenGetOrCreateSequenceValueAndStepGreatThanZeroAndCurrentValueGreatThanEndValue()
        {
            var name = RandomUtility.RandomCode(10);
            await this.TestObject.CreateSequence(name, new SequenceInfo() { StartValue = 100, Step = 5, EndValue = 110 });
            Assert.AreEqual(100, await this.TestObject.GetOrCreateValue(name, SequenceInfo.Default));
            Assert.AreEqual(105, await this.TestObject.GetOrCreateValue(name, SequenceInfo.Default));
            Assert.AreEqual(110, await this.TestObject.GetOrCreateValue(name, SequenceInfo.Default));
            Assert.AreEqual(100, await this.TestObject.GetOrCreateValue(name, SequenceInfo.Default));
            Assert.AreEqual(105, await this.TestObject.GetOrCreateValue(name, SequenceInfo.Default));
        }

        [TestCategory("GetValueOrCreate")]
        [TestMethod]
        public async Task ShouldDecreaseValueWhenGetOrCreateSequenceValueAndStepLessThanZero()
        {
            var name = RandomUtility.RandomCode(10);
            await this.TestObject.CreateSequence(name, new SequenceInfo() { StartValue = 100, Step = -5 });
            Assert.AreEqual(100, await this.TestObject.GetValue(name));
            Assert.AreEqual(95, await this.TestObject.GetValue(name));
            Assert.AreEqual(90, await this.TestObject.GetValue(name));
            Assert.AreEqual(85, await this.TestObject.GetValue(name));
        }

        [TestCategory("GetValueOrCreate")]
        [TestMethod]
        public async Task ShouldReuseStartValueWhenGetOrCreateSequenceValueAndStepLessThanZeroAndCurrentValueLessThanEndValue()
        {
            var name = RandomUtility.RandomCode(10);
            await this.TestObject.CreateSequence(name, new SequenceInfo() { StartValue = 100, Step = -5, EndValue = 90 });
            Assert.AreEqual(100, await this.TestObject.GetOrCreateValue(name, SequenceInfo.Default));
            Assert.AreEqual(95, await this.TestObject.GetOrCreateValue(name, SequenceInfo.Default));
            Assert.AreEqual(90, await this.TestObject.GetOrCreateValue(name, SequenceInfo.Default));
            Assert.AreEqual(100, await this.TestObject.GetOrCreateValue(name, SequenceInfo.Default));
            Assert.AreEqual(95, await this.TestObject.GetOrCreateValue(name, SequenceInfo.Default));
        }
        #endregion

    }
}
