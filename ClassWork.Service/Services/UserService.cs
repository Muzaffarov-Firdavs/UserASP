using AutoMapper;
using ClassWork.Data.IRepositories;
using ClassWork.Domain.Entites;
using ClassWork.Service.DTOs.Users;
using ClassWork.Service.Exceptions;
using ClassWork.Service.Extensions;
using ClassWork.Service.Helpers;
using ClassWork.Service.Interfaces;
using System.Linq.Expressions;

namespace ClassWork.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper mapper;
        private readonly IRepository<User> userRepositoy;
        private readonly IRepository<UserImage> userImageRepositoy;
        public UserService(IMapper mapper,
            IRepository<User> userRepositoy,
            IRepository<UserImage> userImageRepositoy)
        {
            this.mapper = mapper;
            this.userRepositoy = userRepositoy;
            this.userImageRepositoy = userImageRepositoy;
        }

        public async ValueTask<UserForResultDto> CreateAsync(UserForCreationDto dto)
        {
            User user = await this.userRepositoy.SelectAsync(t => t.Email == dto.Email);
            if (user is not null)
                throw new CustomException(403, "User is already exist with tihs email");

            var mappedUser = mapper.Map<User>(dto);
            var result = await this.userRepositoy.InsertAsync(mappedUser);
            await this.userRepositoy.SaveChangesAsync();
            return this.mapper.Map<UserForResultDto>(result);
        }

        public async ValueTask<bool> DeleteAsync(long id)
        {
            var user = await this.userRepositoy.SelectAsync(t => t.Id == id);
            if (user is null)
                throw new CustomException(404, "User is not found");

            await this.userRepositoy.DeleteAsync(user);
            await this.userRepositoy.SaveChangesAsync();
            return true;
        }

        public async ValueTask<IEnumerable<UserForResultDto>> RetriewAllAsync(Expression<Func<User, bool>> expression = null, string search = null)
        {
            var users = userRepositoy.SelectAll(expression, isTracking: false);
            var result = this.mapper.Map<IEnumerable<UserForResultDto>>(users);

            foreach (var item in result)
                item.Image = this.mapper.Map<UserImageForResultDto>(
                    await this.userImageRepositoy.SelectAsync(t => t.UserId == item.Id));

            if (!string.IsNullOrEmpty(search))
                return result.Where(
                    u => u.Firstname.ToLower().Contains(search.ToLower()) ||
                    u.Lastname.ToLower().Contains(search.ToLower()) ||
                    u.Email.ToLower().Contains(search.ToLower())).ToList();

            return result;
        }

        public async ValueTask<UserForResultDto> RetriewAsync(long id)
        {
            var user = await this.userRepositoy.SelectAsync(t => t.Id == id);
            if (user is null)
                throw new CustomException(404, "User is not found");

            var result = mapper.Map<UserForResultDto>(user);
            result.Image = mapper.Map<UserImageForResultDto>(
                await this.userImageRepositoy.SelectAsync(t => t.UserId == result.Id));

            return result;
        }

        public async ValueTask<UserForResultDto> UpdateAsync(UserForUpdateDto dto)
        {
            var updatingUser = await this.userRepositoy.SelectAsync(t => t.Id == dto.Id);
            if (updatingUser is null)
                throw new CustomException(404, "User is not found");

            this.mapper.Map(dto, updatingUser);
            updatingUser.UpdatedAt = DateTime.UtcNow;
            await this.userRepositoy.SaveChangesAsync();

            var result = mapper.Map<UserForResultDto>(updatingUser);
            result.Image = mapper.Map<UserImageForResultDto>(
                await this.userImageRepositoy.SelectAsync(t => t.UserId == result.Id));

            return result;
        }

        // user token
        public async ValueTask<UserForResultDto> CheckUserImageAsync(string email, string password = null)
        {
            var user = await this.userRepositoy.SelectAsync(t => t.Email == email);
            if (user is null)
                throw new CustomException(404, "User is not found");

            return this.mapper.Map<UserForResultDto>(user);
        }


        // user image
        public async ValueTask<UserImageForResultDto> RetriewUserImageAsync(long userId)
        {
            var userImage = await this.userImageRepositoy.SelectAsync(t => t.Id == userId);
            if (userImage is null)
                throw new CustomException(404, "User is not found");

            return mapper.Map<UserImageForResultDto>(userImage);
        }
        public async ValueTask<bool> DeleteUserImageAsync(long userId)
        {
            var userImage = await this.userImageRepositoy.SelectAsync(t => t.Id == userId);
            if (userImage is null)
                throw new CustomException(404, "User is not found");

            File.Delete(userImage.Path);
            await this.userImageRepositoy.DeleteAsync(userImage);
            await this.userImageRepositoy.SaveChangesAsync();

            return true;
        }

        public async ValueTask<UserImageForResultDto> ImageUploadAsync(UserImageForCreationDto dto)
        {
            var user = await this.userRepositoy.SelectAsync(t => t.Id == dto.UserId);
            if (user is null)
                throw new CustomException(404, "User is not found");

            byte[] image = dto.Image.ToByteArray();
            var fileExtension = Path.GetExtension(dto.Image.FileName);
            var fileName = Guid.NewGuid().ToString("N") + fileExtension;
            var webRootpath = EnvironmentHelper.WebHostPath;
            var folder = Path.Combine(webRootpath, "uploads","images");

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            var fullPath = Path.Combine(folder, fileName);
            using var imageStream = new MemoryStream(image);

            using var imagePath = new FileStream(fullPath, FileMode.CreateNew);
            imageStream.WriteTo(imagePath);

            var userImage = new UserImage
            {
                Name = fileName,
                Path = fullPath,
                Id = dto.UserId,
                User = user,
                CreatedAt = DateTime.UtcNow
            };

            var createdImage = await this.userImageRepositoy.InsertAsync(userImage);
            await this.userImageRepositoy.SaveChangesAsync();
            return mapper.Map<UserImageForResultDto>(createdImage);
        }
    }
}