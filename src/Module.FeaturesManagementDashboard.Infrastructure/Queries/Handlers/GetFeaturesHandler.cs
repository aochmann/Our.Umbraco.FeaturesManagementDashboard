using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FeaturesManagementDashboard.Application.DTO.Features;
using FeaturesManagementDashboard.Application.Queries;
using FeaturesManagementDashboard.Domain.Repositories;
using FeaturesManagementDashboard.Infrastructure.Mappers;

namespace FeaturesManagementDashboard.Infrastructure.Queries.Handlers
{
    internal class GetFeaturesHandler : IQueryHandler<GetFeatures, IEnumerable<FeatureDto>>
    {
        private readonly IUmbracoFeatureRepository _featureRepository;
        private readonly IFeatureItemsDtoMapper _featureItemsDtoMapper;

        public GetFeaturesHandler(IUmbracoFeatureRepository featureRepository,
            IFeatureItemsDtoMapper featureItemsDtoMapper)
        {
            _featureRepository = featureRepository;
            _featureItemsDtoMapper = featureItemsDtoMapper;
        }

        public async Task<IEnumerable<FeatureDto>> Handle(GetFeatures _, CancellationToken cancellationToken)
        {
            var features = await _featureRepository.GetAllAsync();

            return features is not null
                ? _featureItemsDtoMapper.Map(features)
                : Enumerable.Empty<FeatureDto>();
        }
    }
}