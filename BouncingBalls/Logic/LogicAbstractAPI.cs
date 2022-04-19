using System;
using System.Windows;
using BouncingBalls.Data;

namespace BouncingBalls.Logic
{
    public abstract class LogicAbstractAPI
    {
        public abstract void Add(MovingObject movingObject);
        public abstract void Remove(MovingObject movingObject);
        public abstract void Step(float timeDelta);
        public abstract MovingObject Create();


        public static LogicAbstractAPI CreateLayer(int width, int height, MovingObjectDataLayerAbstractAPI data = default(MovingObjectDataLayerAbstractAPI))
        {
            return new BallLogic(width, height, data ?? MovingObjectDataLayerAbstractAPI.Create());
        }

        private class BallLogic : LogicAbstractAPI
        {
            public BallLogic(int width, int height, MovingObjectDataLayerAbstractAPI dataLayerAPI)
            {
                dataLayer = dataLayerAPI;
                service = new Logic.BallService();
                boardHeight = height;
                boardWidth = width;
            }

            public override void Add(MovingObject movingObject)
            {
                service.Add(dataLayer, movingObject);
            }

            public override void Step(float timeDelta)
            {
                for(int i=0; i<dataLayer.Count(); i++)
                {
                    dataLayer.Get(i).Move(timeDelta);

                    // TODO: odbijanie się od ścian.
                }
            }

            public override void Remove(MovingObject movingObject)
            {
                dataLayer.Remove(movingObject);
            }

            public override MovingObject Create()
            {
                Random r = new Random();
                int ray = r.Next(10, 25);
                double x = r.NextDouble() * (boardWidth - ray * 2);
                double y = r.NextDouble() * (boardHeight - ray * 2);
                double speedX = r.NextDouble();
                double speedY = r.NextDouble();
                int directionX = r.Next(0, 1) == 1 ? 1 : -1;
                int directionY = r.Next(0, 1) == 1 ? 1 : -1;

                return new Ball(x, y, speedX * directionX, speedY * directionY, ray);
            }

            private readonly MovingObjectDataLayerAbstractAPI dataLayer;
            private readonly BallService service = default(Logic.BallService);
            private int boardWidth;
            private int boardHeight;
        }
    }
}
