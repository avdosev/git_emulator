using System;
using NUnit.Framework;
using GitTask;

namespace GitTask
{
    [TestFixture]
    public class GitTests
    {
        private const int DefaultFilesCount = 5;
        private Git sut;

        [SetUp]
        public void SetUp()
        {
            sut = new Git(DefaultFilesCount);
        }

        [Test]
        public void YouTried()
        {
            try {
                sut.Checkout(0, 1);
            }
            catch (ArgumentException ) {
                Assert.True(true, "OK");
            }
            catch (Exception e) {
                Assert.Fail(e.Message);
            }
            Assert.True(true, "Никто не написал этот тест...");
        }
   }
}