using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class AddRegionRequestDto
    {
        //here we add only properties that we want to receive from the client
        //Id will be created internally by the app, so we do not need it here

        //add data annotations

        [Required] //coming from System.ComponentModel.DataAnnotations
        [MinLength(3, ErrorMessage ="'Code' has to be minimum of 3 characters")]
        [MaxLength(3, ErrorMessage = "'Code' has to be maximum of 3 characters")]
        public string Code { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "'Name' has to be maximum of 100 characters")]
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; } //nullable property
    }
}
