using BouncingBalls.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace BouncingBalls.Logic
{
    /// <summary>
    /// Usługa wykonująca czynności na rzecz poruszających się kul.
    /// </summary>
    internal class BallService
    {
        /// <summary>
        /// Odbija kule od ściany, jeśli kula dotarła do ściany.
        /// </summary>
        /// <param name="ball">Poruszająca się kula.</param>
        /// <param name="width">Szerokość pola, po którym porusza się kula.</param>
        /// <param name="height">Wysokość pola, po którym porusza się kula.</param>
        public void WallBounce(MovingObject ball, int width, int height)
        {
            // Średnica kuli.
            double diameter = DataAbstractAPI.GetBallRadius(ball) * 2;
            // Prawa ściana nie licząc średnicy kuli.
            double right = width - diameter;
            // Prawa Dolna nie licząc średnicy kuli.
            double down = height - diameter;

            // Prawo.
            if (ball.X < 0)
            {
                ball.X = -ball.X;
                ball.SpeedX = -ball.SpeedX;
            }
            // Lewo.
            else if (ball.X > right)
            {
                ball.X = right - (ball.X - right);
                ball.SpeedX = -ball.SpeedX;
            }

            // Góra.
            if (ball.Y < 0)
            {
                ball.Y = -ball.Y;
                ball.SpeedY = -ball.SpeedY;
            }
            // Dół.
            else if (ball.Y > down)
            {
                ball.Y = down - (ball.Y - down);
                ball.SpeedY = -ball.SpeedY;
            }
        }
    }
}
