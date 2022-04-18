using System;
using BouncingBalls.Data;

namespace BouncingBalls.Logic
{
    public abstract class LogicAbstractAPI
    {
        public static LogicAbstractAPI CreateLayer(DataLayerAbstractAPI data = default(DataLayerAbstractAPI))
        {
            return new BallLogic(data == null ? DataLayerAbstractAPI.Create() : data);
        }

        private class BallLogic : LogicAbstractAPI
        {
            public BallLogic(DataLayerAbstractAPI dataLayerAPI)
            {
                MyDataLayer = dataLayerAPI;
                MyDataLayer.Connect();

                add = new Logic.BallService();
            }

            private readonly DataLayerAbstractAPI MyDataLayer;
            private BallService add = default(Logic.BallService);
        }
    }
}
