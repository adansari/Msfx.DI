using FakeTypes.For.ActivatorInstanceFactoryTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Msfx.DI.Factories.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class ActivatorInstanceFactoryTests
    {
        [TestMethod]
        public void ActivatorInstanceFactory_CreateInstance_Public_Ctor_Test()
        {
            //arrange
            Type type = typeof(Public_Ctor);
            object[] args = null;
            InstanceFactory instanceFactory = new ActivatorInstanceFactory();

            //act
            Object obj = instanceFactory.CreateInstance(type, args);

            //Assert
            Assert.IsInstanceOfType(obj, type);

        }

        [TestMethod]
        public void ActivatorInstanceFactory_CreateInstance_Public_Ctor_With_Params_Test()
        {
            //arrange
            Type type = typeof(Public_Ctor_With_Params);
            object[] args = new object[] {"string",1};
            InstanceFactory instanceFactory = new ActivatorInstanceFactory();

            //act
            Object obj = instanceFactory.CreateInstance(type, args);

            //Assert
            Assert.IsInstanceOfType(obj, type);

        }

        [TestMethod]
        public void ActivatorInstanceFactory_CreateInstance_Protected_Crot_Test()
        {
            //arrange
            Type type = typeof(Protected_Crot);
            object[] args = null;
            InstanceFactory instanceFactory = new ActivatorInstanceFactory();

            //act
            Object obj = instanceFactory.CreateInstance(type, args);

            //Assert
            Assert.IsInstanceOfType(obj, type);

        }

        [TestMethod]
        public void ActivatorInstanceFactory_CreateInstance_Protected_Crot_With_Params_Test()
        {
            //arrange
            Type type = typeof(Protected_Crot_With_Params);
            object[] args = new object[] { "string", 1 };
            InstanceFactory instanceFactory = new ActivatorInstanceFactory();
            
            //act
            Object obj = instanceFactory.CreateInstance(type, args);

            //Assert
            Assert.IsInstanceOfType(obj, type);

        }

        [TestMethod]
        public void ActivatorInstanceFactory_CreateInstance_Private_Ctor_Test()
        {
            //arrange
            Type type = typeof(Private_Ctor);
            object[] args = null;
            InstanceFactory instanceFactory = new ActivatorInstanceFactory();

            //act
            Object obj = instanceFactory.CreateInstance(type, args);

            //Assert
            Assert.IsInstanceOfType(obj, type);

        }

        [TestMethod]
        public void ActivatorInstanceFactory_CreateInstance_Private_Ctor_With_Params_Test()
        {
            //arrange
            Type type = typeof(Private_Ctor_With_Params);
            object[] args = new object[] { "string", 1 };
            InstanceFactory instanceFactory = new ActivatorInstanceFactory();

            //act
            Object obj = instanceFactory.CreateInstance(type, args);

            //Assert
            Assert.IsInstanceOfType(obj, type);

        }
    }
}
