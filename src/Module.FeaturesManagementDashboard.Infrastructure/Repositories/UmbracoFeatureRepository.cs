namespace FeaturesManagementDashboard.Infrastructure.Repositories
{
    internal class UmbracoFeatureRepository : IUmbracoFeatureRepository
    {
        private readonly string _umbracoConnectionString;
        private readonly string _providerName;
        private readonly IFeatureItemMapper _featureItemMapper;
        private readonly IFeatureItemsMapper _featureItemsMapper;
        private readonly IFeatureItemDtoMapper _featureDtoMapper;

        public UmbracoFeatureRepository(ConnectionStrings connectionStrings,
            IFeatureItemMapper featureItemMapper,
            IFeatureItemsMapper featureItemsMapper,
            IFeatureItemDtoMapper featureDtoMapper)
        {
            _umbracoConnectionString = connectionStrings!.ConnectionString!;
            _providerName = connectionStrings!.ProviderName!;
            _featureItemMapper = featureItemMapper;
            _featureItemsMapper = featureItemsMapper;
            _featureDtoMapper = featureDtoMapper;
        }

        public async ValueTask<bool> ExistsAsync(FeatureId featureId)
        {
            using var connection = GetDbConnection();

            var idPrediction = Predicates.Field<FeatureDto>(field => field.Id, DapperExtensions.Predicate.Operator.Eq, featureId.Id);

            var feature = (await connection.GetListAsync<FeatureDto>(idPrediction))
                ?.FirstOrDefault();

            return feature is not null;
        }

        public async ValueTask<IEnumerable<Feature>> GetAllAsync()
        {
            try
            {
                using var connection = GetDbConnection();
                var features = await connection.GetListAsync<FeatureDto>();
                return features is not null
                    ? _featureItemsMapper.Map(features)
                    : Enumerable.Empty<Feature>();
            }
            catch (Exception)
            {
                // ignored
            }

            return Enumerable.Empty<Feature>();
        }

        public async ValueTask<Feature> GetAsync(FeatureId featureId)
        {
            try
            {
                using var connection = GetDbConnection();

                var idPrediction = Predicates.Field<FeatureDto>(field => field.Id, DapperExtensions.Predicate.Operator.Eq, featureId.Id);

                var feature = (await connection.GetListAsync<FeatureDto>(idPrediction))
                    ?.FirstOrDefault();

                return feature is not null
                    ? _featureItemMapper.Map(feature)
                    : null;
            }
            catch (Exception)
            {
                // ignored
            }

            return null;
        }

        public async ValueTask SaveAsync(Feature feature)
        {
            try
            {
                var featureExists = await ExistsAsync(feature.Id);

                using var connection = GetDbConnection();

                var featureDto = _featureDtoMapper.Map(feature);

                if (featureExists)
                {
                    await connection.UpdateAsync(featureDto);

                    return;
                }

                await connection.InsertAsync(featureDto);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private IDbConnection GetDbConnection()
        {
            return _providerName?.ToLower() switch
            {
                "microsoft.data.sqlite" => new Microsoft.Data.Sqlite.SqliteConnection(_umbracoConnectionString),
                "microsoft.data.sqlclient" => new Microsoft.Data.SqlClient.SqlConnection(_umbracoConnectionString),
                _ => throw new ArgumentOutOfRangeException(_providerName, "Not implemented case for provider")
            };
        }
    }
}