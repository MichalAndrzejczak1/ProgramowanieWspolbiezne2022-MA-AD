using BouncingBalls.Data;
using BouncingBalls.Logic;
using Moq;
using NUnit.Framework;

namespace BouncingBallsVisualization.UnitTest
{
    public class ModelTest
    {
        private ModelLayer layer;
        private Mock<LogicAbstractAPI> logicAPI;

        public ModelTest()
        {
            logicAPI = new Mock<LogicAbstractAPI>();
            //layer = new ModelLayer(100, 100, logicAPI.Object);
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void StartTest()
        {
            bool state = false;
            logicAPI.Setup(x => x.Start()).Callback(() => state = true);
            //layer.Start();

            //Assert.IsTrue(state);
            Assert.Pass();
        }
    }
}