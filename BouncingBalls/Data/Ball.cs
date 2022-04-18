using System;
using System.Drawing;

namespace BouncingBalls.Data
{
    internal class Ball : MovingObject
    {
        public float Radius { get; set; }

        public Ball(float x, float y, float speedX, float speedY, float radius)
        {
            X = x;
            Y = y;
            SpeedX = speedX;
            SpeedY = speedY;
            Radius = radius;
        }

        public override void Move(float timeDelta)
        {
            X += SpeedX * timeDelta;
            Y += SpeedY * timeDelta;
        }
    }
}
