using System.Diagnostics.CodeAnalysis;

namespace Module.FeaturesManagementDashboard.Infrastructure.Exceptions
{
    public abstract class InfrastructureException : Exception
    {
        public virtual string Code { get; } = null!;

        protected InfrastructureException([DisallowNull] string message) : base(message)
        {
        }
    }
}