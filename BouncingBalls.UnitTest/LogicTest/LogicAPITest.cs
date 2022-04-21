using BouncingBalls.Data;
using BouncingBalls.Logic;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace BouncingBalls.UnitTest
{
    public class LogicAPITest
    {
        private Mock<DataAbstractAPI> dataAPI;
        private Mock<ATimer> timer;
        private LogicAbstractAPI logic;
        List<MovingObject> balls;

        public LogicAPITest()
        {
            dataAPI = new Mock<DataAbstractAPI>();
            timer = new Mock<ATimer>();
            logic = LogicAbstractAPI.CreateLayer(100, 100, dataAPI.Object, timer.Object);
            balls = new List<MovingObject>();
        }


       [SetUp]
        public void Setup()
        {
            balls = new List<MovingObject>();
            logic = LogicAbstractAPI.CreateLayer(100, 100, dataAPI.Object, timer.Object);
        }

        [Test]
        public void MockAPICountTest()
        {
            dataAPI.Setup(x => x.Count()).Returns(balls.Count);
            Assert.AreEqual(0, logic.Count());
            balls.Add(DataAbstractAPI.CreateBall(10, 20, 2, 3, 1));
            Assert.AreEqual(1, balls.Count);
            dataAPI.Setup(x => x.Count()).Returns(balls.Count);
            Assert.AreEqual(1, logic.Count());
        }

        [Test]
        public void MockAPIGetXTest()
        {
            MovingObject o = DataAbstractAPI.CreateBall(10, 20, 2, 3, 1);
            balls.Add(o);
            int i = 0;
            dataAPI.Setup(x => x.Get(i)).Returns(balls[i]);

            Assert.AreEqual(10, logic.GetX(i));
        }

        [Test]
        public void MockAPIGetYTest()
        {
            MovingObject o = DataAbstractAPI.CreateBall(10, 20, 2, 3, 1);
            balls.Add(o);
            int i = 0;
            dataAPI.Setup(x => x.Get(i)).Returns(balls[i]);

            Assert.AreEqual(20, logic.GetY(i));
        }

        [Test]
        public void MockAPIRemoveTest()
        {
            dataAPI.Setup(x => x.Count()).Returns(balls.Count);
            Assert.AreEqual(0, logic.Count());

            MovingObject o = DataAbstractAPI.CreateBall(10, 20, 2, 3, 1);
            balls.Add(o);
            Assert.AreEqual(1, balls.Count);
            dataAPI.Setup(x => x.Count()).Returns(balls.Count);
            Assert.AreEqual(1, logic.Count());
            dataAPI.Setup(x => x.Remove(o)).Returns(balls.Remove(o));
            logic.Remove(o);
            dataAPI.Setup(x => x.Count()).Returns(balls.Count);
            Assert.AreEqual(0, logic.Count());
        }

        [Test]
        public void MockAPIAddTest()
        {
            dataAPI.Setup(x => x.Count()).Returns(balls.Count);
            Assert.AreEqual(0, logic.Count());

            MovingObject o = DataAbstractAPI.CreateBall(10, 20, 2, 3, 1);
            dataAPI.Setup(x => x.Add(o)).Returns(() => { balls.Add(o); return balls.Count-1; } );

            Assert.AreEqual(0, logic.Add());
        }

        [Test]
        public void MockAPIGetTest()
        {
            MovingObject o = DataAbstractAPI.CreateBall(10, 20, 2, 3, 1);
            balls.Add(o);
            dataAPI.Setup(x => x.Get(0)).Returns(balls[0]);

            Assert.AreEqual(o, logic.Get(0));
        }

        [Test]
        public void MockAPIGetTest()
        {
            MovingObject o = DataAbstractAPI.CreateBall(10, 20, 2, 3, 1);
            balls.Add(o);
            dataAPI.Setup(x => x.Get(0)).Returns(balls[0]);

            Assert.AreEqual(o, logic.Get(0));
        }
    }
}