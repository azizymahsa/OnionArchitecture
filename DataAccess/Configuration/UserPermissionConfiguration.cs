using DataAccess.Configuration.Register;
using Domain.DataModel;

namespace DataAccess.Configuration
{
    public class UserPermissionConfiguration : IEntityConfig
    {
        public void SetConfig()
        {
            EntityConfig.GetDefaultEntityMapping<UserPermission>()
                .SetTableName("portal_user_permissions")
                .SetProperty(p => p.UserId, ps => ps.SetChildParentRelationship<User>(nameof(User.Id)))
                .SetProperty(p => p.Permission, ps => ps.SetChildParentRelationship<User>(nameof(Permission.Id)));
            ;
        }
    }
}