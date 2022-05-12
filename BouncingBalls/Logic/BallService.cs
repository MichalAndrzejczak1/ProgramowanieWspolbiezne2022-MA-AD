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
        public void WallBounce(MovingBall ball, int width, int height)
        {
            // Średnica kuli.
            double diameter = DataAbstractApi.GetBallRadius(ball) * 2;
            // Prawa ściana nie licząc średnicy kuli.
            double right = width - diameter;
            // Prawa Dolna nie licząc średnicy kuli.
            double down = height - diameter;

            // Prawo.
            if (ball.X < 0)
            {
                //Back(ball, right, down);
                ball.X = -ball.X;
                ball.SpeedX = -ball.SpeedX;
            }
            // Lewo.
            else if (ball.X > right)
            {
                //Back(ball, right, down);
                ball.X = right - (ball.X - right);
                ball.SpeedX = -ball.SpeedX;
            }

            // Góra.
            if (ball.Y < 0)
            {
                //Back(ball, right, down);
                ball.Y = -ball.Y;
                ball.SpeedY = -ball.SpeedY;
            }
            // Dół.
            else if (ball.Y > down)
            {
                //Back(ball, right, down);
                ball.Y = down - (ball.Y - down);
                ball.SpeedY = -ball.SpeedY;
            }
        }


        public void BallBounce(List<MovingBall> ballsList, int i)
        {
            var mainBall = (MovingBall.Ball)ballsList[i];
            for (var j = 0; j < ballsList.Count; j++)
            {
                if (j == i)
                    continue;

                var ball = (MovingBall.Ball)ballsList[j];
                if (Collision(mainBall, ball))
                {
                    Back(mainBall, ball);

                    var m1 = mainBall.Radius;
                    var m2 = ball.Radius;
                    var u1X = mainBall.SpeedX;
                    var u2X = ball.SpeedX;
                    var u1Y = ball.SpeedY;
                    var u2Y = ball.SpeedY;

                    if (Math.Abs(m1 - m2) < 0.1)
                    {
                        (mainBall.SpeedX, ball.SpeedX) = (ball.SpeedX, mainBall.SpeedX);
                        (mainBall.SpeedY, ball.SpeedY) = (ball.SpeedY, mainBall.SpeedY);
                    }
                    else
                    {
                        var v1X = (m1 - m2) * u1X / (m1 + m2) + (2 * m2) * u2X / (m1 + m2);
                        var v1Y = (m1 - m2) * u1Y / (m1 + m2) + (2 * m2) * u2Y / (m1 + m2);

                        var v2X = 2 * m1 * u1X / (m1 + m2) + (m2 - m1) * u2X / (m1 + m2);
                        var v2Y = 2 * m1 * u1Y / (m1 + m2) + (m2 - m1) * u2Y / (m1 + m2);

                        mainBall.SpeedX = v1X;
                        mainBall.SpeedY = v1Y;

                        ball.SpeedX = v2X;
                        ball.SpeedY = v2Y;
                    }

                    return;
                }
            }
        }

        public bool Collision(MovingBall.Ball a, MovingBall.Ball b)
        {
            if (a == null || b == null)
                return false;

            return Distance(a, b) <= (a.Radius + b.Radius);
        }

        private double Distance(MovingBall.Ball a, MovingBall.Ball b)
        {
            var x1 = a.X + a.Radius;
            var y1 = a.Y + a.Radius;
            var x2 = b.X + b.Radius;
            var y2 = b.Y + b.Radius;

            return Math.Sqrt((Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2)));
        }

        private void Back(MovingBall.Ball a, MovingBall.Ball b)
        {
            while (Collision(a, b))
            {
                a.Move(-1);
            }
        }
    }
}
