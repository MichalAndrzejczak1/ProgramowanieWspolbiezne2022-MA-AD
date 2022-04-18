using System;
using System.Collections.Generic;
using System.Text;

namespace BouncingBalls.Data
{
    internal abstract class MovingObject
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float SpeedX { get; set; }
        public float SpeedY { get; set; }

        public abstract void Move(float timeDelta);
    }
}
