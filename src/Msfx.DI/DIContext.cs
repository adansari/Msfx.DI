using Msfx.DI.Containers;
using Msfx.DI.Scanners;
using System;
using System.Reflection;

namespace Msfx.DI
{
    public abstract class DIContext
    {
        protected string _currentNamespace;
        protected Assembly _callingAssembly;

        protected DependencyScanTarget _scanTarget = DependencyScanTarget.CurrentNamespace;
        protected IDIContainer _container;

        private Type _callingType;

        protected DIContext()
        {
            this._container = new ConcurrentDictDIContainer();
        }

        protected DIContext(Type callingType) : this()
        {
            this._callingType = callingType;

            this._currentNamespace = callingType.Namespace;

            this._callingAssembly = callingType.Assembly;
        }

        protected DIContext(Type callingType, DependencyScanTarget scanTarget) : this(callingType)
        {
            this._scanTarget = scanTarget;
        }

        public virtual string CurrentNamespace { get { return this._currentNamespace; } }

        public virtual Assembly CallingAssembly { get { return this._callingAssembly; } }

        public virtual IDIContainer Container { get { return this._container; } }

        public abstract void Scan();

        public virtual T Inject<T>(params object[] args)
        {
            string dependencyId = typeof(T).GetDependencyId();

            if (this.Container.ContainsDependency(dependencyId))
            {
                //Todo: For Interface/Abs Class dont have any Preferred imple class, PrimaryDependencyHolder is null - Need to handle 
                return (T)this.Container.GetDependencyMap(dependencyId).PrimaryDependencyHolder.GetInstance(args);
            }

            return default(T);
        }

        public virtual TSource Inject<TSource, TTarget>(params object[] args)
        {
            string sourceDependencyId = typeof(TSource).GetDependencyId();
            string targetDependencyId = typeof(TTarget).GetDependencyId();

            if (sourceDependencyId == targetDependencyId)
                return Inject<TSource>(args);

            if (this.Container.ContainsDependency(sourceDependencyId))
            {
                //Todo: For Interface/Abs Class dont have any imple class marked with Injectable, PrimaryDependencyHolder is null - Need to handle 
                IDependencyHolder primaryDependencyHolder = this.Container.GetDependencyMap(sourceDependencyId).PrimaryDependencyHolder;

                if (primaryDependencyHolder != null && primaryDependencyHolder.DependencyId == targetDependencyId)
                {
                    return Inject<TSource>(args);
                }
                else
                {
                    IDependencyHolder secondDependencyHolder = this.Container.GetDependencyMap(sourceDependencyId).GetSecondaryDependencyHolder(targetDependencyId);

                    if (secondDependencyHolder != null)
                    {
                        return (TSource)secondDependencyHolder.GetInstance(args);
                    }
                }
            }
            return default(TSource);
        }
    }
}
