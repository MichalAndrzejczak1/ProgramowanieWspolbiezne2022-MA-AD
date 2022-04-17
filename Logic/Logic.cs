using System;
using Data;

namespace BusinessLogic
{
    public class Logic
    {
        private Board _board;

        public void AddBall(Ball ball)
        {
            _board.Balls.Add(ball);
        }
    }
}
