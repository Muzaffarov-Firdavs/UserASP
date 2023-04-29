using System.ComponentModel.DataAnnotations;

namespace ClassWork.Service.DTOs.Users
{
    public class UserForCreationDto
    {
        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
