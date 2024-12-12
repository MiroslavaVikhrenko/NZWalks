namespace NZWalks.API.Models.DTO
{
    public class RegionDto
    {
        //propeties that we want to expose back to the client - in this case we want to expose all properties as in our domain model
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; } //nullable property
    }
}
