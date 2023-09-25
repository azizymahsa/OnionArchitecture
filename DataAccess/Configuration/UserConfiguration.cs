using DataAccess.Configuration.Register;
using Domain.DataModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Configuration
{
    public class UserConfiguration : IEntityConfig
    {
        public void SetConfig()
        {
            EntityConfig.GetDefaultEntityMapping<User>()
                .SetTableName("portal_users")
                .SetProperty(p => p.Id, ps => ps.SetPrimaryKey().SetDatabaseGenerated(DatabaseGeneratedOption.None));
        }
    }
}