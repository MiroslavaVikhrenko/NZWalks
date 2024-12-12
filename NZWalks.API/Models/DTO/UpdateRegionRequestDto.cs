namespace NZWalks.API.Models.DTO
{
    public class UpdateRegionRequestDto
    {
        //this class will have only those properties that we want the client to update
        //we do not want to allow the client to update an Id
        public string Code { get; set; }
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
