using Msfx.DI.Containers;
using System.Collections.Generic;

namespace Msfx.DI
{
    public interface IDependencyMap
    {
        IDIContainer Container { get; }
        bool IsAbstractOrInterface { get; }
        IDependencyHolder PrimaryDependencyHolder { get; set; }
        List<IDependencyHolder> SecondaryDependencyHolder { get; }
        string SourceDependencyId { get; }
        IDependencyHolder GetSecondaryDependencyHolder(string depedencyId);
    }
}