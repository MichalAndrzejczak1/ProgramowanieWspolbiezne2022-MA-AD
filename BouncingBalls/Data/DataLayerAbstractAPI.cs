using System;
using System.Collections.Generic;
using System.Text;

namespace BouncingBalls.Data
{
    public abstract class DataLayerAbstractAPI
    {
        private List<MovingObject> movingObjects;

        internal List<MovingObject> MovingObjects { get => movingObjects; set => movingObjects = value; }

        public abstract void Connect();

        public static DataLayerAbstractAPI Create()
        {
            return new Board(); 
        }

        #region Layer implementation
        private class Board : DataLayerAbstractAPI
        {
            public override void Connect() => MovingObjects = new List<MovingObject>();
        }

        #endregion Layer implementation
    }
}
