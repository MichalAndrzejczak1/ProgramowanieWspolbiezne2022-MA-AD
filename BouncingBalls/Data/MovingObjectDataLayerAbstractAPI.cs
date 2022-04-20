using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace BouncingBalls.Data
{
    /// <summary>
    /// Abstrakcyjne API dla danych.
    /// </summary>
    public abstract class MovingObjectDataLayerAbstractAPI
    {
        /// <summary>
        /// Dodaje poruszający się obiekt dodanych.
        /// </summary>
        /// <param name="movingObject"></param>
        public abstract void Add(MovingObject movingObject);
        /// <summary>
        /// Zwraca poruszający się obiekt o konkretnym numerze.
        /// </summary>
        /// <param name="i">Numer poruszającego się obiektu.</param>
        /// <returns>Poruszający się obiekt.</returns>
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
