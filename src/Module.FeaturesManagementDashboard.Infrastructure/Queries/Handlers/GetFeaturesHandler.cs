using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Module.FeaturesManagementDashboard.Application.DTO.Features;
using Module.FeaturesManagementDashboard.Application.Queries;
using Module.FeaturesManagementDashboard.Domain.Repositories;
using Module.FeaturesManagementDashboard.Infrastructure.Mappers;

namespace Module.FeaturesManagementDashboard.Infrastructure.Queries.Handlers
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