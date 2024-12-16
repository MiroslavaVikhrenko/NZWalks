using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class RegisterRequestDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; } //username should be an email
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
