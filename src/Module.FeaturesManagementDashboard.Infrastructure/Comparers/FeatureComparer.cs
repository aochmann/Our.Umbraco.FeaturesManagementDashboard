using System.Diagnostics.CodeAnalysis;
using Module.FeaturesManagementDashboard.Domain.Entities.Features;

namespace Module.FeaturesManagementDashboard.Infrastructure.Comparers
{
#pragma warning disable CS8767 // Nullability of reference types in type of parameter doesn't match implicitly implemented member (possibly because of nullability attributes).

    internal class FeatureComparer : IEqualityComparer<Feature>
    {
        public bool Equals([DisallowNull] Feature first, [DisallowNull] Feature second)

            => first.Id.Equals(second.Id);

        public int GetHashCode([DisallowNull] Feature obj)
            => obj.Id.GetHashCode();
    }

#pragma warning restore CS8767 // Nullability of reference types in type of parameter doesn't match implicitly implemented member (possibly because of nullability attributes).
}