using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DapperExtensions;
using FeaturesManagementDashboard.Application.DTO.Features;
using FeaturesManagementDashboard.Domain.Entities.Features;
using FeaturesManagementDashboard.Domain.Repositories;
using FeaturesManagementDashboard.Infrastructure.Mappers;
#if NET6_0 || NET7_0
using Microsoft.Data.SqlClient;
#endif
#if NET5_0
using System.Data.SqlClient;
#endif
using Umbraco.Cms.Core.Configuration.Models;

namespace FeaturesManagementDashboard.Infrastructure.Repositories
{
    internal class UmbracoFeatureRepository : IUmbracoFeatureRepository
    {
        private readonly string _umbracoConnectionString;
        private readonly IFeatureItemMapper _featureItemMapper;
        private readonly IFeatureItemsMapper _featureItemsMapper;
        private readonly IFeatureItemDtoMapper _featureDtoMapper;

#if NET6_0 || NET7_0
        public UmbracoFeatureRepository(ConnectionStrings connectionStrings,
            IFeatureItemMapper featureItemMapper,
            IFeatureItemsMapper featureItemsMapper,
            IFeatureItemDtoMapper featureDtoMapper)
        {
            _umbracoConnectionString = connectionStrings!.ConnectionString!;
            _featureItemMapper = featureItemMapper;
            _featureItemsMapper = featureItemsMapper;
            _featureDtoMapper = featureDtoMapper;
        }
#endif
#if NET5_0
        public UmbracoFeatureRepository(ConnectionStrings connectionStrings,
            IFeatureItemMapper featureItemMapper,
            IFeatureItemsMapper featureItemsMapper,
            IFeatureItemDtoMapper featureDtoMapper)
        {
            _umbracoConnectionString = connectionStrings.UmbracoConnectionString.ConnectionString;
            _featureItemMapper = featureItemMapper;
            _featureItemsMapper = featureItemsMapper;
            _featureDtoMapper = featureDtoMapper;
        }
#endif
        public async ValueTask<bool> ExistsAsync(FeatureId featureId)
        {
            using var connection = new SqlConnection(_umbracoConnectionString);

            var idPrediction = Predicates.Field<FeatureDto>(field => field.Id, DapperExtensions.Predicate.Operator.Eq, featureId.Id);

            var feature = (await connection.GetListAsync<FeatureDto>(idPrediction))
                ?.FirstOrDefault();

            return feature is not null;
        }

        public async ValueTask<IEnumerable<Feature>> GetAllAsync()
        {
            try
            {
                using var connection = new SqlConnection(_umbracoConnectionString);

                var features = await connection.GetListAsync<FeatureDto>();

                return features is not null
                    ? _featureItemsMapper.Map(features)
                    : Enumerable.Empty<Feature>();
            }
            catch (Exception)
            {
            }

            return Enumerable.Empty<Feature>();
        }

        public async ValueTask<Feature> GetAsync(FeatureId featureId)
        {
            try
            {
                using var connection = new SqlConnection(_umbracoConnectionString);

                var idPrediction = Predicates.Field<FeatureDto>(field => field.Id, DapperExtensions.Predicate.Operator.Eq, featureId.Id);

                var feature = (await connection.GetListAsync<FeatureDto>(idPrediction))
                    ?.FirstOrDefault();

                return feature is not null
                    ? _featureItemMapper.Map(feature)
                    : null;
            }
            catch (Exception)
            {
            }

            return null;
        }

        public async ValueTask SaveAsync(Feature feature)
        {
            try
            {
                var featureExists = await ExistsAsync(feature.Id);

                using var connection = new SqlConnection(_umbracoConnectionString);

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
            }
        }
    }
}