using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class Board
    {
        private List<Ball> _balls = new List<Ball>();

        public Board(List<Ball> balls)
        {
            Balls = balls;
        }

        public List<Ball> Balls { get => _balls; set => _balls = value; }
    }
}
