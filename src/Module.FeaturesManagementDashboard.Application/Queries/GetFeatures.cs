using System.Collections.Generic;
using Module.FeaturesManagementDashboard.Application.DTO.Features;

namespace Module.FeaturesManagementDashboard.Application.Queries
{
    public record GetFeatures : IQuery<GetFeatures, IEnumerable<FeatureDto>>;
}