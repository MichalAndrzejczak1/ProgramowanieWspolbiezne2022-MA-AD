using System;
using System.Drawing;

namespace BouncingBalls.Data
{
    public class Ball : MovingObject
    {
        public double Radius { get; set; }

        public Ball(double x, double y, double speedX, double speedY, double radius)
        {
            X = x;
            Y = y;
            SpeedX = speedX;
            SpeedY = speedY;
            Radius = radius;
        }

        public override void Move(double timeDelta)
        {
            X += SpeedX * timeDelta;
            Y += SpeedY * timeDelta;
        }
    }
}
