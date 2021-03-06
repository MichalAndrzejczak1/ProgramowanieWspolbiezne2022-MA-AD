using System;
using System.Collections.Generic;
using static BouncingBalls.Data.MovingBall;

namespace BouncingBalls.Data
{
    /// <summary>
    /// Abstrakcyjne API dla danych.
    /// </summary>
    public abstract class DataAbstractApi
    {
        /// <summary>
        /// Zwraca logera dla danych.
        /// </summary>
        public abstract LoggerAbstractApi LoggerAbstractApi { get; }
        /// <summary>
        /// Szerokość obszaru, po którym poruszają się kule.
        /// </summary>
        public abstract int BoardWidth {get;}
        /// <summary>
        /// Wysokość obszaru, po którym poruszają się kule.
        /// </summary>
        public abstract int BoardHeight {get;}
        /// <summary>
        /// Dodaje poruszający się obiekt dodanych.
        /// </summary>
        /// <param name="ball"></param>
        /// <returns>Numer utworzonego obiektu na liście.</returns>
        public abstract int Add(MovingBall ball);
        /// <summary>
        /// Zwraca poruszający się obiekt o konkretnym numerze.
        /// </summary>
        /// <param name="i">Numer poruszającego się obiektu.</param>
        /// <returns>Poruszający się obiekt.</returns>
        public abstract MovingBall Get(int i);
        /// <summary>
        /// Usuwa wskazany poruszający się obiekt.
        /// </summary>
        /// <param name="ball">Poruszający się obiekt do usunięcia.</param>
        /// <returns>True, jeśli udało się go usunąć.</returns>
        public abstract bool Remove(MovingBall ball);
        /// <summary>
        /// Zwraca wszystkie obiekty.
        /// </summary>
        /// <returns>Wszystkie obiekty.</returns>
        public abstract List<MovingBall> GetAll();
        /// <summary>
        /// Zwraca ilość przechowywanych poruszających się obiektów.
        /// </summary>
        /// <returns>Ilość przechowywanych poruszających się obiektów.</returns>
        public abstract int Count();
        /// <summary>
        /// Tworzy implementację abstrakcyjnego API w postaci tablicy kul.
        /// </summary>
        /// <returns>Implementacja API w postaci tablicy poruszających się kul.</returns>
        public static DataAbstractApi Create(LoggerAbstractApi logger = default(LoggerAbstractApi))
        {
            return new Board(logger ?? LoggerAbstractApi.Create());
        }

        /// <summary>
        /// Tworzy nową poruszającą się kulę.
        /// </summary>
        /// <param name="id">Numer kuli z listy będący jej unikalnym id.</param>
        /// <param name="x">Położenie w poziomie.</param>
        /// <param name="y">Położenie w pionie.</param>
        /// <param name="speedX">Prędkość w poziomie, wartość co jaką obiekt przesunie się co milisekundę.</param>
        /// <param name="speedY">Prędkość w pionie, wartość co jaką obiekt przesunie się co milisekundę.</param>
        /// <param name="radius">Promień kuli.</param>
        /// <param name="loggerAbstractApi">Logger zapisujący zmiany położnia.</param>
        /// <returns>Nowa poruszająca się kula.</returns>
        public static MovingBall CreateBall(int id, double x, double y, double speedX, double speedY, double radius, LoggerAbstractApi loggerAbstractApi = default(LoggerAbstractApi))
        {
            return new Ball(id, x, y, speedX, speedY, radius, loggerAbstractApi ?? LoggerAbstractApi.Create());
        }
        /// <summary>
        /// Zwraca promień kuli.
        /// </summary>
        /// <param name="ball">Kula.</param>
        /// <returns>Promień kuli.</returns>
        public static double GetBallRadius(MovingBall ball)
        {
            return ball.Radius;
        }
        #region Layer implementation
        /// <summary>
        /// Implementacja API w postaci tablicy poruszających się kul.
        /// </summary>
        internal class Board : DataAbstractApi
        {
            public override int BoardWidth => boardWidth;
            public override int BoardHeight => boardHeight;
            public override LoggerAbstractApi LoggerAbstractApi { get => loggerApi; }

            /// <summary>
            /// Konstruktor, tworzy listę do przechowywania kul.
            /// </summary>
            internal Board(LoggerAbstractApi loggerAbstractApi = default(LoggerAbstractApi))
            {
                balls = new List<MovingBall>();
                loggerApi = loggerAbstractApi ?? LoggerAbstractApi.Create();
                boardWidth = 770;
                boardHeight = 500;
            }

            /// <summary>
            /// Szerokość obszaru, po którym poruszają się kule.
            /// </summary>
            public override int Add(MovingBall ball)
            {
                balls.Add(ball);
                return balls.Count - 1;
            }

            public override List<MovingBall> GetAll()
            {
                return balls;
            }

            public override int Count()
            {
                return balls.Count;
            }

            public override MovingBall Get(int i)
            {
                return balls[i];
            }

            public override bool Remove(MovingBall ball)
            {
                return balls.Remove(ball);
            }

            #region Private stuff
            private readonly List<MovingBall> balls;
            private int boardWidth;
            private int boardHeight;
            private readonly LoggerAbstractApi loggerApi = null;
            #endregion Private stuff
        }

        #endregion Layer implementation
    }
}
