using BouncingBalls.Logic;
using System;
using System.Collections.Generic;
using System.Text;

namespace BouncingBalls.Data
{
    public class ModelLayer
    {
        public ModelLayer(LogicAbstractAPI bussinesLayer = null)
        {
            Data = bussinesLayer ?? LogicAbstractAPI.CreateLayer();
        }

        private readonly LogicAbstractAPI Data = default(LogicAbstractAPI);
    }
}
