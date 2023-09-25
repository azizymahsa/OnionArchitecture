
using Domain.Dto.Permission;
using Domain.Dto.User;

namespace Domain.Service
{
    public interface IUserService
    {
        Task<UserDto> GetAsync(int id);
        Task<UserDto> GetByUsernameAsync(string username);
        Task<IEnumerable<UserDto>> ListAsync();
        Task CreateAsync(CreateUserDto userDto);
        Task DeleteAsync(int id);
    }
}
