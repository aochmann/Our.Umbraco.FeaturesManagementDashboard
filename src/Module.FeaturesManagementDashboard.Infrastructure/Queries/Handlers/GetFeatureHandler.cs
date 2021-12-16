using System.Threading;
using System.Threading.Tasks;
using Module.FeaturesManagementDashboard.Application.DTO.Features;
using Module.FeaturesManagementDashboard.Application.Queries;
using Module.FeaturesManagementDashboard.Domain.Entities.Features;
using Module.FeaturesManagementDashboard.Domain.Repositories;
using Module.FeaturesManagementDashboard.Infrastructure.Mappers;

namespace Module.FeaturesManagementDashboard.Infrastructure.Queries.Handlers
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