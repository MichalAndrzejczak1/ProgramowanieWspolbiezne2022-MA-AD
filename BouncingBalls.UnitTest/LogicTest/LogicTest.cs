using BouncingBalls.Data;
using BouncingBalls.Logic;
using Moq;
using NUnit.Framework;

namespace BouncingBalls.UnitTest
{
    public class LogicTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void APITest()
        {
            Assert.Pass();
        }

        [Test]
        public void MockTest()
        {
            var dataAPI = new Mock<DataAbstractAPI>();
            var logic = LogicAbstractAPI.CreateLayer(100, 100, dataAPI.Object);
        }
    }
}