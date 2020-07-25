using FakeTypes.For.DITests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Msfx.DI.Scanners;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msfx.DI.Scanners.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class DependencyScannerTests
    {
        [TestMethod]
        public void DependencyScanner_GetDependencyScanner_NamespaceRecursive_Test()
        {
            //arrange 
            Type fakeType = typeof(Boot);
            DependencyScanner dependencyScanner;

            //act
            dependencyScanner = DependencyScanner.GetDependencyScanner(DependencyScanTarget.CurrentNamespaceRecursive, fakeType.Assembly, fakeType.Namespace);

            //assert
            Assert.IsNotNull(dependencyScanner);
            Assert.IsInstanceOfType(dependencyScanner, typeof(NamespaceRecursiveScanner));
        }

        [TestMethod]
        public void DependencyScanner_GetDependencyScanner_Assembly_Test()
        {
            //arrange 
            Type fakeType = typeof(Boot);
            DependencyScanner dependencyScanner;

            //act
            dependencyScanner = DependencyScanner.GetDependencyScanner(DependencyScanTarget.Assembly, fakeType.Assembly, fakeType.Namespace);

            //assert
            Assert.IsNotNull(dependencyScanner);
            Assert.IsInstanceOfType(dependencyScanner, typeof(AssemblyScanner));
        }

        [TestMethod]
        public void DependencyScanner_GetDependencyScanner_None_Test()
        {
            //arrange 
            Type fakeType = typeof(Boot);
            DependencyScanner dependencyScanner;

            //act
            dependencyScanner = DependencyScanner.GetDependencyScanner(DependencyScanTarget.None, fakeType.Assembly, fakeType.Namespace);

            //assert
            Assert.IsNull(dependencyScanner);
        }

        [TestMethod]
        public void DependencyScanner_GetDependencyScanner_Namespace_Test()
        {
            //arrange 
            Type fakeType = typeof(Boot);
            DependencyScanner dependencyScanner;

            //act
            dependencyScanner = DependencyScanner.GetDependencyScanner(DependencyScanTarget.CurrentNamespace, fakeType.Assembly, fakeType.Namespace);

            //assert
            Assert.IsNotNull(dependencyScanner);
            Assert.IsInstanceOfType(dependencyScanner, typeof(NamespaceScanner));
        }
    }
}