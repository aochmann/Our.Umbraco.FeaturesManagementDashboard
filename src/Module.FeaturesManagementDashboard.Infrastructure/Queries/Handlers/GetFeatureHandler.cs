using System.Threading;
using System.Threading.Tasks;
using FeaturesManagementDashboard.Application.DTO.Features;
using FeaturesManagementDashboard.Application.Queries;
using FeaturesManagementDashboard.Domain.Entities.Features;
using FeaturesManagementDashboard.Domain.Repositories;
using FeaturesManagementDashboard.Infrastructure.Mappers;

namespace FeaturesManagementDashboard.Infrastructure.Queries.Handlers
{
    internal class GetFeatureHandler : IQueryHandler<GetFeature, FeatureDto>
    {
        private readonly IUmbracoFeatureRepository _featureRepository;
        private readonly IFeatureItemDtoMapper _featureDtoMapper;

        public GetFeatureHandler(IUmbracoFeatureRepository featureRepository, IFeatureItemDtoMapper featureDtoMapper)
        {
            _featureRepository = featureRepository;
            _featureDtoMapper = featureDtoMapper;
        }

        public async Task<FeatureDto> Handle(GetFeature query, CancellationToken cancellationToken)
        {
            var feature = await _featureRepository.GetAsync(query.FeatureId.ToFeatureId());

            return feature is not null
                ? _featureDtoMapper.Map(feature)
                : null;
        }
    }
}