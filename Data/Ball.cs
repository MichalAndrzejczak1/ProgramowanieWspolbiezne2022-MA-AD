using System;
using System.Drawing;

namespace Data
{
    public class Ball
    {

        private PointF _coordinates;
        private PointF _speed;
        private float _radius;

        public Ball(PointF coordinates, PointF speed, float radius)
        {
            _coordinates = coordinates;
            _speed = speed;
            _radius = radius;
        }

        public PointF Coordinates { get => _coordinates; set => _coordinates = value; }
        public PointF Speed { get => _speed; set => _speed = value; }
        public float Radius { get => _radius; set => _radius = value; }
    }
}
