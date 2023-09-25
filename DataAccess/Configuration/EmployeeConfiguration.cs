using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Configuration.Register;
using Domain.DataModel;

namespace DataAccess.Configuration
{
    class EmployeeConfiguration : IEntityConfig
    {
        public void SetConfig()
        {
            EntityConfig.GetDefaultEntityMapping<Employee>()
                .SetTableName("portal_employees")
                .SetProperty(p => p.Id, ps => ps.SetPrimaryKey().SetDatabaseGenerated(DatabaseGeneratedOption.None));
        }
    }
}
