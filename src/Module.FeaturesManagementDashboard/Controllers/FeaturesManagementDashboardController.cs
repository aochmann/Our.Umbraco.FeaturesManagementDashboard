using System.Collections.Generic;
using System.Threading.Tasks;
using FeaturesManagementDashboard.Application.Commands;
using FeaturesManagementDashboard.Application.DI;
using FeaturesManagementDashboard.Application.DTO.Features;
using FeaturesManagementDashboard.Application.Queries;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Web.BackOffice.Controllers;

namespace FeaturesManagementDashboard.Controllers
{
    [Route("umbraco/backoffice/api/[controller]")]
    public class FeaturesManagementDashboardController : UmbracoAuthorizedApiController
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;

        public FeaturesManagementDashboardController(ICompositionRoot compositionRoot)
        {
            _queryDispatcher = compositionRoot.Resolve<IQueryDispatcher>();
            _commandDispatcher = compositionRoot.Resolve<ICommandDispatcher>();
        }

        public ActionResult<TResult> OkOrNotFound<TResult>(TResult result)
            => result is not null
                ? Ok(result)
                : NotFound();

        [HttpGet("features")]
        public async ValueTask<ActionResult<IEnumerable<FeatureDto>>> GetFeatures()
        {
            var features = await _queryDispatcher.QueryAsync(new GetFeatures());

            return OkOrNotFound(features);
        }

        [HttpGet("{featureId:alpha}")]
        public async ValueTask<ActionResult<FeatureDto>> GetFeature([FromRoute] GetFeature query)
        {
            var featureDefinition = await _queryDispatcher.QueryAsync(query);

            return OkOrNotFound(featureDefinition);
        }

        [HttpPut("features/{featureId:alpha}/status")]
        public async ValueTask<IActionResult> UpdateFeature(string featureId, [FromBody] UpdateFeature command)
        {
            command = new UpdateFeature(featureId, command.Status);

            await _commandDispatcher.DispatchAsync(command);

            return CreatedAtAction(nameof(GetFeature), command.FeatureId);
        }
    }
}