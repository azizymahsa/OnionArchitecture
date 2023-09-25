using Domain.DataModel.Contracts;

namespace Domain.DataModel
{
    public class User : IIntegerBaseEntity
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public ICollection<UserPermission> UserPermissions { get; set; }
    }
}
