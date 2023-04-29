using System.ComponentModel.DataAnnotations;

namespace ClassWork.Service.DTOs.Users
{
    public class UserForUpdateDto
    {
        [Required]
        public long Id { get; set; }

        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
