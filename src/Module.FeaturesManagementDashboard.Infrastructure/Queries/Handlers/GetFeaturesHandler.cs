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

        public async ValueTask<IEnumerable<FeatureDto>> HandleAsync(GetFeatures _)
        {
            var features = await _featureRepository.GetAllAsync();

            return features is not null
                ? _featureItemsDtoMapper.Map(features)
                : Enumerable.Empty<FeatureDto>();
        }
    }
}