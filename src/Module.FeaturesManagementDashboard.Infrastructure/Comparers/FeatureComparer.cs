namespace FeaturesManagementDashboard.Infrastructure.Comparers;
#pragma warning disable CS8767 // Nullability of reference types in type of parameter doesn't match implicitly implemented member (possibly because of nullability attributes).

internal class FeatureComparer : IEqualityComparer<Feature>
{
    public bool Equals(Feature first, Feature second)

        => first.Id.Equals(second.Id);

    public int GetHashCode(Feature obj)
        => obj.Id.GetHashCode();
}

#pragma warning restore CS8767 // Nullability of reference types in type of parameter doesn't match implicitly implemented member (possibly because of nullability attributes).