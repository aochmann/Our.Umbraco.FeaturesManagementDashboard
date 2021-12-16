using System.Collections.Generic;
using System.Linq;
using Lamar;
using Module.FeaturesManagementDashboard.Application.DI;
using Shared.Exceptions;

namespace Module.FeaturesManagementDashboard.Infrastructure
{
    internal class CompositionRoot : ICompositionRoot
    {
        private readonly IContainer _container;

        public CompositionRoot(IContainer container)
            => _container = container;

        public THandler Resolve<THandler>()
        {
            var handler = _container.TryGetInstance<THandler>();

            return handler is not null
                ? handler
                : throw new DependencyNotFoundException(typeof(THandler));
        }

        public IEnumerable<THandler> ResolveMany<THandler>()
        {
            var handlers = _container.GetAllInstances<THandler>();

            return handlers is not null && handlers.Any()
                ? handlers
                : throw new DependencyNotFoundException(typeof(THandler));
        }
    }
}