using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace BouncingBalls.Data
{
    public abstract class MovingObjectDataLayerAbstractAPI
    {
        public abstract void Add(MovingObject movingObject);

        public abstract MovingObject Get(int i);

        public abstract bool Remove(MovingObject movingObject);

        public abstract int Count();

        public static MovingObjectDataLayerAbstractAPI Create()
        {
            return new Board(); 
        }

        public static MovingObject CreateBall(double x, double y, double speedX, double speedY, double radius)
        {
            return new Ball(x, y, speedX, speedY, radius);
        }

        #region Layer implementation
        internal class Board : MovingObjectDataLayerAbstractAPI
        {
            internal Board()
            {
                balls = new List<MovingObject>();
            }

            public override void Add(MovingObject movingObject)
            {
                balls.Add(movingObject);
            }

            public override int Count()
            {
                return balls.Count;
            }

            public override MovingObject Get(int i)
            {
                return balls[i];
            }

            public override bool Remove(MovingObject movingObject)
            {
                return balls.Remove(movingObject);
            }

            private readonly List<MovingObject> balls;
        }

        #endregion Layer implementation
    }
}
