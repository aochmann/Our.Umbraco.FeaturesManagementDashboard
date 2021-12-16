using System.Threading;
using System.Threading.Tasks;
using Module.FeaturesManagementDashboard.Domain.Entities.Features;
using Module.FeaturesManagementDashboard.Domain.Exceptions;
using Module.FeaturesManagementDashboard.Domain.Repositories;

namespace Module.FeaturesManagementDashboard.Application.Commands.Handlers
{
    internal class UpdateFeatureHandler : ICommandHandler<UpdateFeature>
    {
        private readonly IUmbracoFeatureRepository _featureRepository;

        public UpdateFeatureHandler(IUmbracoFeatureRepository featureRepository)
            => _featureRepository = featureRepository;

        public async Task Handle(UpdateFeature command, CancellationToken cancellationToken)
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