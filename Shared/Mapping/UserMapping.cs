using Domain.Dto.User;
using Shared.Model.User;

namespace Shared.Mapping
{
    public static class UserMapping
    {
        #region CreateUserRequest => CreateUserDto

        public static CreateUserDto ToDto(this CreateUserRequest model)
        {
            return new CreateUserDto
            {
                Username = model.Username
            };
        }

        #endregion
    }
}
