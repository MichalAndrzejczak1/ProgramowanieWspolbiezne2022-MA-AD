using BouncingBalls.Data;
using BouncingBalls.Logic;
using Moq;
using NUnit.Framework;

namespace BouncingBallsVisualization.UnitTest
{
    public class ModelTest
    {
        private ModelLayer layer;
        private Mock<LogicAbstractApi> logicApi;

        public ModelTest()
        {
            logicApi = new Mock<LogicAbstractApi>();
            layer = new ModelLayer(100, 100, logicApi.Object);
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void StartTest()
        {
            bool state = false;
            logicApi.Setup(x => x.Start()).Callback(() => state = true);
            layer.Start();

            Assert.IsTrue(state);
            Assert.Pass();
        }
    }
}