using System;

namespace FeaturesManagementDashboard.Infrastructure.Exceptions
{
    public abstract class InfrastructureException : Exception
    {
        public virtual string Code { get; } = null!;

        protected InfrastructureException(string message) : base(message)
        {
        }
    }
}