namespace NZWalks.API.Models.DTO
{
    public class AddWalkRequestDto
    {
        //we do not accept Id from the client, the app creates Id by itself

        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; } //nullable property

        //properties that set relations between this model and the others:
        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }
    }
}
