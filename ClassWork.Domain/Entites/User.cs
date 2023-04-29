using ClassWork.Domain.Commons;
using ClassWork.Domain.Enums;

namespace ClassWork.Domain.Entites
{
    public class User : Auditable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Password { get; set; }
        public UserRole Role { get; set; } = UserRole.User;
    }
}
