using System.Collections.Generic;

namespace Msfx.DI
{
    public interface IDependencyMap
    {
        bool IsAbstractOrInterface { get; }
        IDependencyHolder PrimaryDependencyHolder { get; set; }
        List<IDependencyHolder> SecondaryDependencyHolder { get; }
        string SourceDependencyId { get; }
        IDependencyHolder GetSecondaryDependencyHolder(string depedencyId);
    }
}