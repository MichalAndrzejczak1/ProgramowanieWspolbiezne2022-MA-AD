using BouncingBalls.Data;
using NUnit.Framework;

namespace BouncingBalls.UnitTest
{
    public class BallDataTest
    {
        private Ball ball;
        private double x = 100, y = 300;
        private double speedX = -1.0, speedY = 2.0;
        private double radius = 3.0;
        private double x2 = 300, y2 = 400;
        private double speedX2 = -5.0, speedY2 = 6.0;
        private double radius2 = 7.0;
        private double timeDelta = 10;

        public BallDataTest()
        {
            ball = (Ball)MovingObjectDataLayerAbstractAPI.CreateBall(x, y, speedX, speedY, radius);
        }

        [SetUp]
        public void Setup()
        {
            ball = (Ball)MovingObjectDataLayerAbstractAPI.CreateBall(x, y, speedX, speedY, radius);
        }

        [Test]
        public void GetPropertiesTest()
        {
            Assert.AreEqual(x, ball.X);
            Assert.AreEqual(y, ball.Y);
            Assert.AreEqual(speedX, ball.SpeedX);
            Assert.AreEqual(speedY, ball.SpeedY);
            Assert.AreEqual(radius, ball.Radius);
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
            ball.Radius = radius2;
            Assert.AreEqual(radius2, ball.Radius);
        }

        [Test]
        public void MoveTest()
        {
            ball.Move(timeDelta);
            Assert.AreEqual(x + speedX * timeDelta, ball.X);
            Assert.AreEqual(y + speedY * timeDelta, ball.Y);
        }
    }
}