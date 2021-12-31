using System.Collections.Generic;
using FeaturesManagementDashboard.Application.DTO.Features;

namespace FeaturesManagementDashboard.Application.Queries
{
    public record GetFeatures : IQuery<GetFeatures, IEnumerable<FeatureDto>>;
}