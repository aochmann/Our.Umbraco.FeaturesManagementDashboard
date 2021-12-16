using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Module.FeaturesManagementDashboard.Domain.Entities.Features;

namespace Module.FeaturesManagementDashboard.Infrastructure.Comparers
{
    internal class FeatureComparer : IEqualityComparer<Feature>
    {
        public bool Equals(Feature first, Feature second)
            => first.Id.Equals(second.Id);

        public int GetHashCode([DisallowNull] Feature obj)
            => obj.Id.GetHashCode();
    }
}