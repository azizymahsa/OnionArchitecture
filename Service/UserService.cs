using Domain.DataAccess.Repository;
using Domain.Dto.User;
using Domain.Mapping;
using Domain.Service;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Service
{
    public class UserService : IUserService
    {
        #region Constructor
        private readonly ModelStateDictionary _modelState;
        private readonly IUserRepository _userRepository;

        public UserService
        (
            ModelStateDictionary modelState,
            IUserRepository userRepository
        )
        {
            _modelState = modelState;
            _userRepository = userRepository;
        }
        #endregion

        #region Get
        public async Task<UserDto> GetAsync(int id)
        {
            var model = await _userRepository.GetAsync(id);
            return model.ToDto();
        }
        public async Task<UserDto> GetByUsernameAsync(string username)
        {
            var model = await _userRepository.GetByUsernameAsync(username);
            return model.ToDto();
        }
        #endregion

        #region List
        public async Task<IEnumerable<UserDto>> ListAsync()
        {
            return (await _userRepository.ListAsync()).ToDtoList();
        }
        #endregion

        #region Create
        public async Task CreateAsync(CreateUserDto userDto)
        {
            var model = await GetByUsernameAsync(userDto.Username);
            if (model != null)
                _modelState.AddModelError(nameof(CreateUserDto.Username), "Username already exists");

            if (!_modelState.IsValid)
                return;

            await _userRepository.CreateAsync(userDto.ToDataModel());
        }
        #endregion

        #region Delete
        public async Task DeleteAsync(int id)
        {
            await _userRepository.DeleteAsync(id);
        }
        #endregion

    }
}
