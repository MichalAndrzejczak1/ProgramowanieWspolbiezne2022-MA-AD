using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BouncingBalls.UnitTest
{
    [TestClass]
    public class DataTest
    {
        [TestMethod]
        public void CreateTest()
        {
            /*TestLinq2SQLFixcture dataLayerTestingFixture = new TestLinq2SQLFixcture();
            BusinessLogicAbstractAPI model = BusinessLogicAbstractAPI.CreateLayer(dataLayerTestingFixture);
            Assert.AreEqual<int>(1, dataLayerTestingFixture.ConnectedCallCount);*/
        }
    }
}


/*using TPA.ApplicationArchitecture.Data;

namespace TPA.ApplicationArchitecture.BusinessLogic.Tests
{
    public class TestLinq2SQLFixcture : DataLayerAbstractAPI
    {
        internal int ConnectedCallCount = 0;

        #region ILinq2SQLAPI

        public override void Connect()
        {
            ConnectedCallCount++;
        }

        #endregion ILinq2SQLAPI
    }
}*/
