namespace NZWalks.API.Models.DTO
{
    public class WalkDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; } //nullable property

        //properties that set relations between this model and the others:
        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }

        //include navigation properties' info
        public RegionDto Region { get; set; }
    }
}
