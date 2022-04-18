using BouncingBalls.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace BouncingBalls.Logic
{
    internal class BallService
    {
        public void Add(DataLayerAbstractAPI data, MovingObject movingObject)
        {
            data.MovingObjects.Add(movingObject);
        }

        public Ball CreateBall(float x, float y, float speedX, float speedY, float radius)
        {
            return new Ball(x, y, speedX, speedY, radius);
        }
    }
}
