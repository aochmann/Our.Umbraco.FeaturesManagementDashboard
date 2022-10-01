using System;
using System.Collections.Generic;
using FeaturesManagementDashboard.Application.DI;
using Microsoft.Extensions.DependencyInjection;

namespace FeaturesManagementDashboard.Infrastructure
{
    internal class CompositionRoot : ICompositionRoot
    {
        private readonly IServiceProvider _container;

        public CompositionRoot(IServiceProvider container)
            => _container = container;

        public THandler Resolve<THandler>()
            => _container.GetRequiredService<THandler>();

        public IEnumerable<THandler> ResolveMany<THandler>()
            => _container.GetServices<THandler>();
    }
}