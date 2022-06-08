using BouncingBalls.Data;
using NUnit.Framework;

namespace BouncingBalls.UnitTest
{
    public class MovingObjectDataTest
    {
        private MovingBall ball;
        private readonly double x = 100, y = 300;
        private readonly double speedX = -1.0, speedY = 2.0;
        private readonly double radius = 3.0;
        private readonly double x2 = 300;
        private readonly double y2 = 400;
        private readonly double speedX2 = -5.0, speedY2 = 6.0;
        private readonly double timeDelta = 10;

        public MovingObjectDataTest()
        {
            ball = DataAbstractApi.CreateBall(1, x, y, speedX, speedY, radius);
        }

        [SetUp]
        public void Setup()
        {
            ball = DataAbstractApi.CreateBall(1, x, y, speedX, speedY, radius);
        }

        [Test]
        public void GetPropertiesTest()
        {
            Assert.AreEqual(x, ball.X);
            Assert.AreEqual(y, ball.Y);
            Assert.AreEqual(speedX, ball.SpeedX);
            Assert.AreEqual(speedY, ball.SpeedY);
        }

        [Test]
        public void SetPropertiesTest()
        {
            ball.X = x2;
            Assert.AreEqual(x2, ball.X);
            ball.Y = y2;
            Assert.AreEqual(y2, ball.Y);
            ball.SpeedX = speedX2;
            Assert.AreEqual(speedX2, ball.SpeedX);
            ball.SpeedY = speedY2;
            Assert.AreEqual(speedY2, ball.SpeedY);

        }
    }
}