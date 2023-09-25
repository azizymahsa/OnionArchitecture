using Domain.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Dto._Base;
using Domain.Dto.User;

namespace Domain.Mapping
{
    public static class UserMapping
    {

        #region User => UserDto
        public static UserDto ToDto(this User model)
        {
            return new UserDto
            {
                Id = model.Id,
                Username = model.Username,
            };
        }

        #endregion

        #region IEnumerable<User> => IEnumerable<UserDto>

        public static IEnumerable<UserDto> ToDtoList(this IEnumerable<User> model) => model.Select(ToDto);

        #endregion

        #region CreateUserDto => User
        public static User ToDataModel(this CreateUserDto model)
        {
            return new User
            {
                Username = model.Username
            };
        }

        #endregion

    }
}
