using Domain.DataModel;
using Domain.Dto._Base;
using Domain.Dto.Employee;
using Domain.Dto.Permission;
using Domain.Dto.User;

namespace Domain.DataAccess.Repository
{
    public interface IUserRepository
    {
        Task<User> GetByUsernameAsync(string username);
        Task<User> GetAsync(int id);
        Task<IEnumerable<User>> ListAsync();
        Task<int> CreateAsync(User model);
        Task<int> DeleteAsync(int id);
    }
}
