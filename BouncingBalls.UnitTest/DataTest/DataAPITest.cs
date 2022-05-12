using BouncingBalls.Data;
using NUnit.Framework;
using Moq;

namespace BouncingBalls.UnitTest
{
    public class DataApiTest
    {
        private DataAbstractApi api;
        private MovingBall ball, ball2, ball3, ball4;

        public DataApiTest()
        {
            api = DataAbstractApi.Create();
            ball = DataAbstractApi.CreateBall(1, 1, 2, 3, 4, 5);
            ball2 = DataAbstractApi.CreateBall(2, 1, 2, 3, 4, 5);
            ball3 = DataAbstractApi.CreateBall(3, 1, 2, 3, 4, 5);
            ball4 = DataAbstractApi.CreateBall(4, 1, 2, 3, 4, 5);
        }

        [SetUp]
        public void Setup()
        {
            api = DataAbstractApi.Create();
            api.Add(ball);
            api.Add(ball2);
            api.Add(ball3);
            api.Add(ball4);
        }

        [Test]
        public void ApiAddTest()
        {
            Assert.AreEqual(4, api.Count());
            api.Add(ball);
            Assert.AreEqual(5, api.Count());

            Assert.AreEqual(5, api.Count());


            Assert.AreEqual(5, api.Count());


            Assert.AreEqual(5, api.Count());
            api.Add(ball2);
            api.Add(ball3);
            api.Add(ball4);
            Assert.AreEqual(8, api.Count());

            Assert.AreEqual(8, api.Count());

            Assert.AreEqual(8, api.Count());

            Assert.AreEqual(8, api.Count());
        }

        [Test]
        public void ApiGetTest()
        {
            Assert.AreEqual(ball, api.Get(0));
            Assert.AreEqual(ball2, api.Get(1));
            Assert.AreEqual(ball3, api.Get(2));
            Assert.AreEqual(ball4, api.Get(3));

            Assert.AreEqual(ball, api.Get(0));
            Assert.AreEqual(ball2, api.Get(1));
            Assert.AreEqual(ball3, api.Get(2));
            Assert.AreEqual(ball4, api.Get(3));

            Assert.AreEqual(ball, api.Get(0));
            Assert.AreEqual(ball2, api.Get(1));
            Assert.AreEqual(ball3, api.Get(2));
            Assert.AreEqual(ball4, api.Get(3));
            Assert.AreEqual(ball, api.Get(0));
            Assert.AreEqual(ball2, api.Get(1));
            Assert.AreEqual(ball3, api.Get(2));
            Assert.AreEqual(ball4, api.Get(3));
            Assert.AreEqual(ball, api.Get(0));
            Assert.AreEqual(ball2, api.Get(1));
            Assert.AreEqual(ball3, api.Get(2));
            Assert.AreEqual(ball4, api.Get(3));
            Assert.AreEqual(ball, api.Get(0));
            Assert.AreEqual(ball2, api.Get(1));
            Assert.AreEqual(ball3, api.Get(2));
            Assert.AreEqual(ball4, api.Get(3));
            Assert.AreEqual(ball, api.Get(0));
            Assert.AreEqual(ball2, api.Get(1));
            Assert.AreEqual(ball3, api.Get(2));
            Assert.AreEqual(ball4, api.Get(3));
            Assert.AreEqual(ball, api.Get(0));
            Assert.AreEqual(ball2, api.Get(1));
            Assert.AreEqual(ball3, api.Get(2));
            Assert.AreEqual(ball4, api.Get(3));
            Assert.AreEqual(ball, api.Get(0));
            Assert.AreEqual(ball2, api.Get(1));
            Assert.AreEqual(ball3, api.Get(2));
            Assert.AreEqual(ball4, api.Get(3));
            Assert.AreEqual(ball, api.Get(0));
            Assert.AreEqual(ball2, api.Get(1));
            Assert.AreEqual(ball3, api.Get(2));
            Assert.AreEqual(ball4, api.Get(3));
            Assert.AreEqual(ball, api.Get(0));
            Assert.AreEqual(ball2, api.Get(1));
            Assert.AreEqual(ball3, api.Get(2));
            Assert.AreEqual(ball4, api.Get(3));
            Assert.AreEqual(ball, api.Get(0));
            Assert.AreEqual(ball2, api.Get(1));
            Assert.AreEqual(ball3, api.Get(2));
            Assert.AreEqual(ball4, api.Get(3));
            Assert.AreEqual(ball, api.Get(0));
            Assert.AreEqual(ball2, api.Get(1));
            Assert.AreEqual(ball3, api.Get(2));
            Assert.AreEqual(ball4, api.Get(3));
            Assert.AreEqual(ball, api.Get(0));
            Assert.AreEqual(ball2, api.Get(1));
            Assert.AreEqual(ball3, api.Get(2));
            Assert.AreEqual(ball4, api.Get(3));
            Assert.AreEqual(ball, api.Get(0));
            Assert.AreEqual(ball2, api.Get(1));
            Assert.AreEqual(ball3, api.Get(2));
            Assert.AreEqual(ball4, api.Get(3));
            Assert.AreEqual(ball, api.Get(0));
            Assert.AreEqual(ball2, api.Get(1));
            Assert.AreEqual(ball3, api.Get(2));
            Assert.AreEqual(ball4, api.Get(3));
            Assert.AreEqual(ball, api.Get(0));
            Assert.AreEqual(ball2, api.Get(1));
            Assert.AreEqual(ball3, api.Get(2));
            Assert.AreEqual(ball4, api.Get(3));
        }

        [Test]
        public void ApiDeleteTest()
        {
            Assert.AreEqual(4, api.Count());
            api.Remove(ball);
            Assert.AreEqual(3, api.Count());
            api.Remove(ball2);
            api.Remove(ball3);
            api.Remove(ball4);
            Assert.AreEqual(0, api.Count());
        }
    }
}