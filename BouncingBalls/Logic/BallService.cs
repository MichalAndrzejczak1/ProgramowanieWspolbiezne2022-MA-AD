using BouncingBalls.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace BouncingBalls.Logic
{
    internal class BallService
    {
        public void Add(DataLayerAbstractAPI data, float x, float y, float speedX, float speedY, float radius)
        {
            data.MovingObjects.Add(new Ball(x, y, speedX, speedY, radius));
        }
    }
}
