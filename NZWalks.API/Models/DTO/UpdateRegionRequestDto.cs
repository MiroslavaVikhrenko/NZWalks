using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class UpdateRegionRequestDto
    {
        //this class will have only those properties that we want the client to update
        //we do not want to allow the client to update an Id

        [Required] //coming from System.ComponentModel.DataAnnotations
        [MinLength(3, ErrorMessage = "'Code' has to be minimum of 3 characters")]
        [MaxLength(3, ErrorMessage = "'Code' has to be maximum of 3 characters")]
        public string Code { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "'Name' has to be maximum of 100 characters")]
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; } //nullable property
    }
}
