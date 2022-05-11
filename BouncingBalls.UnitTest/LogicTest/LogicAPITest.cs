using BouncingBalls.Data;
using BouncingBalls.Logic;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace BouncingBalls.UnitTest
{
    public class LogicApiTest
    {
        private Mock<DataAbstractApi> dataApi;
        private Mock<ATimer> timer;
        private LogicAbstractApi logic;
        List<MovingObject> balls;

        public LogicApiTest()
        {
            dataApi = new Mock<DataAbstractApi>();
            timer = new Mock<ATimer>();
            logic = LogicAbstractApi.CreateLayer(100, 100, dataApi.Object, timer.Object);
            balls = new List<MovingObject>();
        }


       [SetUp]
        public void Setup()
        {
            balls = new List<MovingObject>();
            logic = LogicAbstractApi.CreateLayer(100, 100, dataApi.Object, timer.Object);
        }

        [Test]
        public void MockApiCountTest()
        {
            dataApi.Setup(x => x.Count()).Returns(balls.Count);
            Assert.AreEqual(0, logic.Count());
            balls.Add(DataAbstractApi.CreateBall(10, 20, 2, 3, 1));
            Assert.AreEqual(1, balls.Count);
            dataApi.Setup(x => x.Count()).Returns(balls.Count);
            Assert.AreEqual(1, logic.Count());
        }

        [Test]
        public void MockApiGetXTest()
        {
            MovingObject o = DataAbstractApi.CreateBall(10, 20, 2, 3, 1);
            balls.Add(o);
            int i = 0;
            dataApi.Setup(x => x.Get(i)).Returns(balls[i]);

            Assert.AreEqual(10, logic.GetX(i));
        }

        [Test]
        public void MockApiGetYTest()
        {
            MovingObject o = DataAbstractApi.CreateBall(10, 20, 2, 3, 1);
            balls.Add(o);
            int i = 0;
            dataApi.Setup(x => x.Get(i)).Returns(balls[i]);

            Assert.AreEqual(20, logic.GetY(i));
        }

        [Test]
        public void MockApiRemoveTest()
        {
            dataApi.Setup(x => x.Count()).Returns(balls.Count);
            Assert.AreEqual(0, logic.Count());

            MovingObject o = DataAbstractApi.CreateBall(10, 20, 2, 3, 1);
            balls.Add(o);
            Assert.AreEqual(1, balls.Count);
            dataApi.Setup(x => x.Count()).Returns(balls.Count);
            Assert.AreEqual(1, logic.Count());
            dataApi.Setup(x => x.Remove(o)).Returns(balls.Remove(o));
            logic.Remove(o);
            dataApi.Setup(x => x.Count()).Returns(balls.Count);
            Assert.AreEqual(0, logic.Count());
        }

        [Test]
        public void MockApiAddTest()
        {
            dataApi.Setup(x => x.Count()).Returns(balls.Count);
            Assert.AreEqual(0, logic.Count());

            MovingObject o = DataAbstractApi.CreateBall(10, 20, 2, 3, 1);
            dataApi.Setup(x => x.Add(o)).Returns(() => { balls.Add(o); return balls.Count-1; } );

            Assert.AreEqual(0, logic.Add());
        }

        [Test]
        public void MockApiGetTest()
        {
            MovingObject o = DataAbstractApi.CreateBall(10, 20, 2, 3, 1);
            balls.Add(o);
            dataApi.Setup(x => x.Get(0)).Returns(balls[0]);

            Assert.AreEqual(o, logic.Get(0));
        }

        [Test]
        public void MockApiStartTest()
        {
            bool state = false;
            timer.Setup(x => x.Start()).Callback(() => state = true);
            logic.Start();

            Assert.IsTrue(state);
        }

        [Test]
        public void MockApiStopTest()
        {
            bool state = true;
            timer.Setup(x => x.Stop()).Callback(() => state = false);
            logic.Stop();

            Assert.IsFalse(state);
        }

        [Test]
        public void MockApiSetIntervalTest()
        {
            System.TimeSpan ms = System.TimeSpan.FromMilliseconds(200);
            timer.SetupSet(x => x.Interval = ms);
            logic.SetInterval(200);

            Assert.AreEqual(200, ms.Milliseconds);
        }
    }
}