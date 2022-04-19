using BouncingBalls.Data;
using System;
using System.Collections.Generic;
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
    }
}
