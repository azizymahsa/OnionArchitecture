using Domain.DataModel;
using Domain.Dto.Permission;

namespace Domain.Mapping
{
    public static class PermissionMapping
    {

        #region Permission => PermissionDto
        public static PermissionDto ToDto(this Permission model)
        {
            return new PermissionDto
            {
                Id = model.Id,
                Name = model.Name,
            };
        }

        #endregion

        #region IEnumerable<Permission> => IEnumerable<PermissionDto>

        public static IEnumerable<PermissionDto> ToDtoList(this IEnumerable<Permission> model) => model.Select(ToDto);

        #endregion

    }
}
