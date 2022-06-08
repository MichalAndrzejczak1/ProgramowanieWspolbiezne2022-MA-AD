using BouncingBalls.Data;
using System;
using System.Collections.Generic;

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
        public bool WallBounce(MovingBall ball, int width, int height)
        {
            // Średnica kuli.
            double diameter = DataAbstractApi.GetBallRadius(ball) * 2;
            // Prawa ściana nie licząc średnicy kuli.
            double right = width - diameter;
            // Prawa Dolna nie licząc średnicy kuli.
            double down = height - diameter;
            // Czy kula odbiła się od ściany.
            bool bounced = false;
            // Prawo.
            if (ball.X < 0)
            {
                ball.X = -ball.X;
                ball.SpeedX = -ball.SpeedX;
                bounced = true;
            }
            // Lewo.
            else if (ball.X > right)
            {
                ball.X = right - (ball.X - right);
                ball.SpeedX = -ball.SpeedX;
                bounced = true;
            }

            // Góra.
            if (ball.Y < 0)
            {
                ball.Y = -ball.Y;
                ball.SpeedY = -ball.SpeedY;
                bounced = true;
            }
            // Dół.
            else if (ball.Y > down)
            {
                ball.Y = down - (ball.Y - down);
                ball.SpeedY = -ball.SpeedY;
                bounced = true;
            }
            return bounced;
        }


        public int BallBounce(List<MovingBall> ballsList, int i)
        {
            MovingBall mainBall = ballsList[i];
            for (int j = 0; j < ballsList.Count; j++)
            {
                if (j == i)
                    continue;

                MovingBall ball = ballsList[j];
                if (Collision(mainBall, ball))
                {
                    double m1 = mainBall.Radius;
                    double m2 = ball.Radius;
                    double u1X = mainBall.SpeedX;
                    double u2X = ball.SpeedX;
                    double u1Y = ball.SpeedY;
                    double u2Y = ball.SpeedY;

                    if (Math.Abs(m1 - m2) < 0.1)
                    {
                        (mainBall.SpeedX, ball.SpeedX) = (ball.SpeedX, mainBall.SpeedX);
                        (mainBall.SpeedY, ball.SpeedY) = (ball.SpeedY, mainBall.SpeedY);
                    }
                    else
                    {
                        double v1X = (m1 - m2) * u1X / (m1 + m2) + (2 * m2) * u2X / (m1 + m2);
                        double v1Y = (m1 - m2) * u1Y / (m1 + m2) + (2 * m2) * u2Y / (m1 + m2);

                        double v2X = 2 * m1 * u1X / (m1 + m2) + (m2 - m1) * u2X / (m1 + m2);
                        double v2Y = 2 * m1 * u1Y / (m1 + m2) + (m2 - m1) * u2Y / (m1 + m2);

                        mainBall.SpeedX = v1X;
                        mainBall.SpeedY = v1Y;

                        ball.SpeedX = v2X;
                        ball.SpeedY = v2Y;
                    }

                    return ball.Id;
                }
            }
            return -1;
        }

        public bool Collision(MovingBall a, MovingBall b)
        {
            if (a == null || b == null)
                return false;

            return Distance(a, b) <= (a.Radius + b.Radius);
        }

        private double Distance(MovingBall a, MovingBall b)
        {
            double x1 = a.X + a.Radius;
            double y1 = a.Y + a.Radius;
            double x2 = b.X + b.Radius;
            double y2 = b.Y + b.Radius;

            return Math.Sqrt((Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2)));
        }

    }
}
