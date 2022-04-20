using System;
using System.Collections.Generic;
using System.Text;

namespace BouncingBalls.Data
{
    /// <summary>
    /// Bazowa klasa dla wszystkich poruszających się obiektów.
    /// </summary>
    public abstract class MovingObject
    {
        /// <summary>
        /// Położenie w poziomie.
        /// </summary>
        public double X { get; set; } 
        /// <summary>
        /// Położenie w pionie.
        /// </summary>
        public double Y { get; set; }
        /// <summary>
        /// Prędkość w poziomie, wartość co jaką obiekt przesunie się co milisekundę.
        /// </summary>
        public double SpeedX { get; set; }
        /// <summary>
        /// Prędkość w pionie, wartość co jaką obiekt przesunie się co milisekundę.
        /// </summary>
        public double SpeedY { get; set; }

        /// <summary>
        /// Porusza obiektem po określonym czasie milisekund.
        /// </summary>
        /// <param name="miliseconds">Ile milisekund minęło od ostatniej aktualizacji.</param>
        public abstract void Move(double miliseconds);
    }
}
