namespace FeaturesManagementDashboard.Infrastructure.Queries.Handlers;

internal class GetFeatureHandler : IQueryHandler<GetFeature, FeatureDto>
{
    private readonly IUmbracoFeatureRepository _featureRepository;
    private readonly IFeatureItemDtoMapper _featureDtoMapper;

    public GetFeatureHandler(IUmbracoFeatureRepository featureRepository, IFeatureItemDtoMapper featureDtoMapper)
    {
        _featureRepository = featureRepository;
        _featureDtoMapper = featureDtoMapper;
    }

    public async ValueTask<FeatureDto> HandleAsync(GetFeature query)
    {
        var feature = await _featureRepository.GetAsync(query.FeatureId.ToFeatureId());

        return feature is not null
            ? _featureDtoMapper.Map(feature)
            : null;
    }
}