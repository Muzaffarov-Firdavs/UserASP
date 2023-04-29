using ClassWork.Domain.Entites;
using ClassWork.Service.DTOs.Users;
using System.Linq.Expressions;

namespace ClassWork.Service.Interfaces
{
    public interface IUserService
    {
        ValueTask<UserForResultDto> CreateAsync(UserForCreationDto dto);
        ValueTask<UserForResultDto> UpdateAsync(UserForUpdateDto dto);
        ValueTask<UserForResultDto> RetriewAsync(long id);
        ValueTask<IEnumerable<UserForResultDto>> RetriewAllAsync(
            Expression<Func<User, bool>> expression = null, string search = null);
        ValueTask<bool> DeleteAsync(long id);

        // for token
        ValueTask<UserForResultDto> CheckUserImageAsync(string email, string password = null);

        // user image methods

        ValueTask<UserImageForResultDto> ImageUploadAsync(UserImageForCreationDto dto);
        ValueTask<bool> DeleteUserImageAsync(long userId);
        ValueTask<UserImageForResultDto> RetriewUserImageAsync(long userId);


    }
}
