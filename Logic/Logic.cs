using System;
using Data;

namespace Logic
{
    public class BoardLogic
    {
        private Board _board;

        public void AddBall(Ball ball)
        {
            _board.Balls.Add(ball);
        }
    }
}
