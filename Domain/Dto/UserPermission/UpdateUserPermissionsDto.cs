namespace Domain.Dto.UserPermission
{
    public class UpdateUserPermissionsDto
    {
        public int Id { get; set; }
        public IEnumerable<int> PermissionIds { get; set; }
    }
}