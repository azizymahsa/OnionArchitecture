
using DataAccess.Configuration.Register;
using Domain.DataModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Configuration
{
    public class PermissionConfiguration: IEntityConfig
    {
        public void SetConfig()
        {
            EntityConfig.GetDefaultEntityMapping<Permission>()
                .SetTableName("portal_permissions")
                .SetProperty(p => p.Id, ps => ps.SetPrimaryKey().SetDatabaseGenerated(DatabaseGeneratedOption.None));
        }
    }
}
