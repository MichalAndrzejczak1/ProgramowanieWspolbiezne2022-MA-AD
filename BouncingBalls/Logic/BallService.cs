using BouncingBalls.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace BouncingBalls.Logic
{
    internal class BallService
    {
        public void Add(MovingObjectDataLayerAbstractAPI data, double x, double y, double speedX, double speedY, double radius)
        {
            data.Add(new Ball(x, y, speedX, speedY, radius));
        }

        public void Add(MovingObjectDataLayerAbstractAPI data, MovingObject movingObject)
        {
            data.Add(movingObject);
        }

        public void WallBounce(MovingObject ball, int width, int height)
        {
            double size = ((Ball)ball).Radius * 2;
            double right = width - size;
            double down = height - size;

            if(ball.X < 0)
            {
                ball.X = -ball.X;
                ball.SpeedX = -ball.SpeedX;
            }
            else if(ball.X > right)
            {
                ball.X = right - (ball.X - right);
                ball.SpeedX = -ball.SpeedX;
            }

            if (ball.Y < 0)
            {
                ball.Y = -ball.Y;
                ball.SpeedY = -ball.SpeedY;
            }
            else if (ball.Y > down)
            {
                ball.Y = down - (ball.Y - down);
                ball.SpeedY = -ball.SpeedY;
            }
        }
    }
}
