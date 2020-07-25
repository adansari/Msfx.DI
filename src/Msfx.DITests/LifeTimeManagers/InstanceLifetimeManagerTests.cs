using FakeTypes.For.NonDITests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Msfx.DI.LifeTimeManagers;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msfx.DI.LifeTimeManagers.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class InstanceLifetimeManagerTests
    {
        [TestMethod]
        public void InstanceLifetimeManager_GetInstanceLifetimeManager_Test()
        {
            //arrage
            InstanceLifetimeManager localInstanceLifetimeManager, staticInstanceLifetimeManager, invalidLifetimeManager;

            //act
            localInstanceLifetimeManager = InstanceLifetimeManager.GetInstanceLifetimeManager(typeof(foo),InstanceType.Local);
            staticInstanceLifetimeManager = InstanceLifetimeManager.GetInstanceLifetimeManager(typeof(foo), InstanceType.Static);
            invalidLifetimeManager = InstanceLifetimeManager.GetInstanceLifetimeManager(typeof(foo), InstanceType.Invalid);

            //assert
            Assert.IsInstanceOfType(localInstanceLifetimeManager, typeof(LocalInstanceLifetimeManager));
            Assert.IsInstanceOfType(staticInstanceLifetimeManager, typeof(StaticInstanceLifetimeManager));
            Assert.IsNull(invalidLifetimeManager);
        }
    }
}