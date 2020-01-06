using Knife.Hosting.MSTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace YS.Sequence.Core.UnitTest
{
    public abstract class SequenceServiceTestBase : TestBase<ISequenceService>
    {
        [TestMethod]
        public async Task CreateSequenceSuccess()
        {
            var id = RandomUtility.RandomCode(10);
            Assert.IsFalse(await this.TestObject.ExistsAsync(id));
            await this.TestObject.CreateSequence(id, new SequenceInfo
            {
                StartValue = 1,
                EndValue = 1000,
                Step = 1
            });
            Assert.IsTrue(await this.TestObject.ExistsAsync(id));
        }

        [TestMethod]
        public async Task RemoveExistsSequenceShouldReturnTrue()
        {
            var id = RandomUtility.RandomCode(10);
            await this.TestObject.CreateSequence(id, new SequenceInfo());
            var removeResult = await this.TestObject.RemoveAsync(id);
            Assert.IsTrue(removeResult);
        }
        [TestMethod]
        public async Task RemoveNoExistsSequenceShouldReturnFalse()
        {
            var id = RandomUtility.RandomCode(10);
            var removeResult = await this.TestObject.RemoveAsync(id);
            Assert.IsFalse(removeResult);
        }
        [TestMethod]
        public async Task ResetExistsSequenceAndRegetValueShouldReturnDefaultValue()
        {
            var id = RandomUtility.RandomCode(10);
            await this.TestObject.CreateSequence(id, null);
            await this.TestObject.GetValueAsync(id); //return 1
            await this.TestObject.GetValueAsync(id); //return 2
            await this.TestObject.GetValueAsync(id); //return 3;
            await this.TestObject.ResetAsync(id);
            // after reset and get value, it should be the default value
            var value = await this.TestObject.GetValueAsync(id);
            Assert.AreEqual(1L, value);
        }



    }
}
