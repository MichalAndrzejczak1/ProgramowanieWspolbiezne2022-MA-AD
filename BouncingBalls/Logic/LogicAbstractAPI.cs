using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using BouncingBalls.Data;

namespace BouncingBalls.Logic
{
    public abstract class LogicAbstractAPI
    {
        public abstract event EventHandler CordinatesChanged;
        public abstract void Add(MovingObject movingObject);
        public abstract void Remove(MovingObject movingObject);
        public abstract MovingObject Get(int i);
        public abstract void Update(float miliseconds);
        public abstract MovingObject Create();
        public abstract void Start();
        public abstract void Stop();
        public abstract int Count();
        public abstract void SetInterval(int miliseconds);


        public static LogicAbstractAPI CreateLayer(int width, int height, MovingObjectDataLayerAbstractAPI data = default(MovingObjectDataLayerAbstractAPI))
        {
            return new BallLogic(width, height, data ?? MovingObjectDataLayerAbstractAPI.Create());
        }

        private class BallLogic : LogicAbstractAPI
        {
            public override event EventHandler CordinatesChanged { add=> timer.Tick+=value; remove => timer.Tick-=value; }

            public BallLogic(int width, int height, MovingObjectDataLayerAbstractAPI dataLayerAPI)
            {
                dataLayer = dataLayerAPI;
                service = new Logic.BallService();
                boardHeight = height;
                boardWidth = width;
                r = new Random();
                timer = new DispatcherTimer();
                SetInterval(30);
                timer.Tick += (sender, args) => Update(timer.Interval.Milliseconds);
            }

            public override void Add(MovingObject movingObject)
            {
                service.Add(dataLayer, movingObject);
            }

            public override void Update(float miliseconds)
            {
                for(int i=0; i<dataLayer.Count(); i++)
                {
                    dataLayer.Get(i).Move(miliseconds);

                    service.WallBounce(dataLayer.Get(i), boardWidth, boardHeight);
                }
            }

            public override void Remove(MovingObject movingObject)
            {
                dataLayer.Remove(movingObject);
            }

            public override MovingObject Create()
            {
                int ray = r.Next(10, 25);
                double x = r.NextDouble() * (boardWidth - ray * 2.0);
                double y = r.NextDouble() * (boardHeight - ray * 2.0);
                double speedX = (r.NextDouble() - 0.5) / 10.0;
                double speedY = (r.NextDouble() - 0.5) / 10.0;

                return new Ball(x, y, speedX, speedY, ray);
            }

            public override MovingObject Get(int i)
            {
                return dataLayer.Get(i);
            }

            public override void Start()
            {
                timer.Start();
            }

            public override void Stop()
            {
                timer.Stop();
            }

            public override int Count()
            {
                return dataLayer.Count();
            }

            public override void SetInterval(int miliseconds)
            {
                timer.Interval = TimeSpan.FromMilliseconds(miliseconds);
            }


            private readonly MovingObjectDataLayerAbstractAPI dataLayer;
            private readonly BallService service = default(Logic.BallService);
            private int boardWidth;
            private int boardHeight;
            private Random r;
            DispatcherTimer timer;
        }
    }
}
