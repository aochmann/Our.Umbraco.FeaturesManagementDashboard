﻿using System.Threading;
using System.Threading.Tasks;
using FeaturesManagementDashboard.Domain.Entities.Features;
using FeaturesManagementDashboard.Domain.Exceptions;
using FeaturesManagementDashboard.Domain.Repositories;

namespace FeaturesManagementDashboard.Application.Commands.Handlers
{
    internal class UpdateFeatureHandler : ICommandHandler<UpdateFeature>
    {
        private readonly IUmbracoFeatureRepository _featureRepository;

        public UpdateFeatureHandler(IUmbracoFeatureRepository featureRepository)
            => _featureRepository = featureRepository;

        public async ValueTask HandleAsync(UpdateFeature command)
        {
            var feature = await _featureRepository.GetAsync(command.FeatureId.ToFeatureId());

            if (feature == null)
            {
                throw new FeatureNotFoundException(command.FeatureId);
            }

            feature.UpdateStatus(command.Status); // example of domain method

            await _featureRepository.SaveAsync(feature);
        }
    }
}