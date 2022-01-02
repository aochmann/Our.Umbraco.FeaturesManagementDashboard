namespace FeaturesManagementDashboard.Application.DTO.Features
{
    public record FeatureDto
    {
        public string Id { get; init; } = null!;
        public string Name { get; init; } = null!;
        public bool Status { get; init; }
    }
}