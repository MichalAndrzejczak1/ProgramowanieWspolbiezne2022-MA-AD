using System;
using System.Drawing;

namespace BouncingBalls.Data
{
    /// <summary>
    /// Poruszająca się kula.
    /// </summary>
    public class Ball : MovingObject
    {
        /// <summary>
        /// Promień kuli.
        /// </summary>
        public double Radius { get; set; }
        /// <summary>
        /// Tworzy kulę.
        /// </summary>
        /// <param name="x">Położenie w poziomie.</param>
        /// <param name="y">Położenie w pionie.</param>
        /// <param name="speedX">Prędkość w poziomie, wartość co jaką obiekt przesunie się co milisekundę.</param>
        /// <param name="speedY">Prędkość w pionie, wartość co jaką obiekt przesunie się co milisekundę.</param>
        /// <param name="radius">Promień kuli.</param>
        public Ball(double x, double y, double speedX, double speedY, double radius)
        {
            X = x;
            Y = y;
            SpeedX = speedX;
            SpeedY = speedY;
            Radius = radius;
        }
        /// <summary>
        /// Porusza kulą po określonym czasie milisekund.
        /// </summary>
        /// <param name="miliseconds">Ile milisekund minęło od ostatniej aktualizacji.</param>
        public override void Move(double milliseconds)
        {
            X += SpeedX * milliseconds;
            Y += SpeedY * milliseconds;
        }
    }
}
