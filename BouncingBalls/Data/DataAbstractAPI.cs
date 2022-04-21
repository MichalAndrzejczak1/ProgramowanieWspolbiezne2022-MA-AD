using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using static BouncingBalls.Data.MovingObject;

namespace BouncingBalls.Data
{
    /// <summary>
    /// Abstrakcyjne API dla danych.
    /// </summary>
    public abstract class DataAbstractAPI
    {
        /// <summary>
        /// Dodaje poruszający się obiekt dodanych.
        /// </summary>
        /// <param name="movingObject"></param>
        /// <returns>Numer utworzonego obiektu na liście.</returns>
        public abstract int Add(MovingObject movingObject);
        /// <summary>
        /// Zwraca poruszający się obiekt o konkretnym numerze.
        /// </summary>
        /// <param name="i">Numer poruszającego się obiektu.</param>
        /// <returns>Poruszający się obiekt.</returns>
        public abstract MovingObject Get(int i);
        /// <summary>
        /// Usuwa wskazany poruszający się obiekt.
        /// </summary>
        /// <param name="movingObject">Poruszający się obiekt do usunięcia.</param>
        /// <returns>True, jeśli udało się go usunąć.</returns>
        public abstract bool Remove(MovingObject movingObject);
        /// <summary>
        /// Zwraca ilość przechowywanych poruszających się obiektów.
        /// </summary>
        /// <returns>Ilość przechowywanych poruszających się obiektów.</returns>
        public abstract int Count();
        /// <summary>
        /// Tworzy implementację abstrakcyjnego API w postaci tablicy kul.
        /// </summary>
        /// <returns>Implementacja API w postaci tablicy poruszających się kul.</returns>
        public static DataAbstractAPI Create()
        {
            return new Board(); 
        }
        /// <summary>
        /// Tworzy nową poruszającą się kulę.
        /// </summary>
        /// <param name="x">Położenie w poziomie.</param>
        /// <param name="y">Położenie w pionie.</param>
        /// <param name="speedX">Prędkość w poziomie, wartość co jaką obiekt przesunie się co milisekundę.</param>
        /// <param name="speedY">Prędkość w pionie, wartość co jaką obiekt przesunie się co milisekundę.</param>
        /// <param name="radius">Promień kuli.</param>
        /// <returns>Nowa poruszająca się kula.</returns>
        public static MovingObject CreateBall(double x, double y, double speedX, double speedY, double radius)
        {
            return new Ball(x, y, speedX, speedY, radius);
        }
        /// <summary>
        /// Zwraca promień kuli.
        /// </summary>
        /// <param name="ball">Kula.</param>
        /// <returns>Promień kuli.</returns>
        public static double GetBallRadius(MovingObject ball)
        {
            return ((Ball)ball).Radius;
        }

        #region Layer implementation
        /// <summary>
        /// Implementacja API w postaci tablicy poruszających się kul.
        /// </summary>
        internal class Board : DataAbstractAPI
        {
            /// <summary>
            /// Konstruktor, tworzy listę do przechowywania kul.
            /// </summary>
            internal Board()
            {
                balls = new List<MovingObject>();
            }

            public override int Add(MovingObject movingObject)
            {
                balls.Add(movingObject);
                return balls.Count-1;
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

            #region Private stuff
            private readonly List<MovingObject> balls;
            #endregion Private stuff
        }

        #endregion Layer implementation
    }
}
