using BouncingBalls.Data;
using BouncingBalls.Logic;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace BouncingBalls.UnitTest
{
    public class APITest
    {
        private Mock<DataAbstractAPI> dataAPI;
        private Mock<ATimer> timer;
        private LogicAbstractAPI logic;

        public APITest()
        {
            dataAPI = new Mock<DataAbstractAPI>();
            timer = new Mock<ATimer>();
            logic = LogicAbstractAPI.CreateLayer(100, 100, dataAPI.Object, timer.Object);
        }


       [SetUp]
        public void Setup()
        {
            dataAPI = new Mock<DataAbstractAPI>();
            timer = new Mock<ATimer>();
            logic = LogicAbstractAPI.CreateLayer(100, 100, dataAPI.Object, timer.Object);
        }

        [Test]
        public void MockAPITest()
        {
            List<MovingObject> balls = new List<MovingObject>();
            //MovingObject ball = DataAbstractAPI.Create();


            dataAPI.Setup(x => x.Count()).Returns(balls.Count);
            
            Assert.AreEqual(0, dataAPI.Object.Count());

            //dataAPI.Setup(x => x.Add(ball)));
        }
    }
}