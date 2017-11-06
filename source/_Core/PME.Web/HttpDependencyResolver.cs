using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using System.Web.Http.Dependencies;

namespace PME.Web
{
    public class HttpDependencyResolver : DisposableObject, IDependencyResolver
    {

        private readonly ILifetimeScope _scope;

        private class PerRequestContainer : DisposableObject, IDependencyScope
        {

            private readonly ILifetimeScope _perRequestScope;

            public PerRequestContainer(ILifetimeScope perRequestScope)
            {
                _perRequestScope = perRequestScope;
            }

            public virtual IEnumerable<object> GetServices(Type serviceType)
            {
                if (!_perRequestScope.IsRegistered(serviceType))
                {
                    return Enumerable.Empty<object>();
                }

                Type type = typeof(IEnumerable<>).MakeGenericType(new[] { serviceType });

                return _perRequestScope.Resolve(type) as IEnumerable<object>;
            }

            public virtual object GetService(Type serviceType)
            {
                return _perRequestScope.ResolveOptional(serviceType);
            }

            public override void Dispose(bool disposing)
            {
                if (disposing)
                {
                    _perRequestScope.Dispose();
                }

                base.Dispose(disposing);
            }
        }

        public HttpDependencyResolver(ILifetimeScope scope)
        {
            _scope = scope;
        }

        public IDependencyScope BeginScope()
        {
            return new PerRequestContainer(_scope.BeginLifetimeScope());
        }

        public virtual IEnumerable<object> GetServices(Type serviceType)
        {
            if (!_scope.IsRegistered(serviceType))
            {
                return Enumerable.Empty<object>();
            }

            var type = typeof(IEnumerable<>).MakeGenericType(new[] { serviceType });

            return _scope.Resolve(type) as IEnumerable<object>;
        }

        public virtual object GetService(Type serviceType)
        {
            return _scope.ResolveOptional(serviceType);
        }

    }
}
