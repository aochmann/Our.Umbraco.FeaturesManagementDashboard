using System;
using System.Diagnostics.CodeAnalysis;

namespace FeaturesManagementDashboard.Infrastructure.Exceptions
{
    public abstract class InfrastructureException : Exception
    {
        public virtual string Code { get; } = null!;

        protected InfrastructureException([DisallowNull] string message) : base(message)
        {
        }
    }
}