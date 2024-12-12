namespace NZWalks.API.Models.DTO
{
    public class AddRegionRequestDto
    {
        //here we add only properties that we want to receive from the client
        //Id will be created internally by the app, so we do not need it here
        public string Code { get; set; }
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; } //nullable property
    }
}
