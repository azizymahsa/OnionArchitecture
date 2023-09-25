using Domain.DataAccess.Repository;
using Domain.Dto.Permission;
using Domain.Mapping;
using Domain.Service;

namespace Service
{
    public class PermissionService : IPermissionService
    {
        #region Constructor
        private readonly IPermissionRepository _permissionRepository;

        public PermissionService
        (
            IPermissionRepository permissionRepository
        )
        {
            _permissionRepository = permissionRepository;
        }
        #endregion

        #region List

        public async Task<IEnumerable<PermissionDto>> ListAsync()
        {
            return (await _permissionRepository.ListAsync()).ToDtoList();
        }
        #endregion
    }
}
