using DataAccess.Repository;
using Domain.DataAccess.Repository;
using Domain.Dto.Permission;
using Domain.Dto.UserPermission;
using Domain.Mapping;
using Domain.Service;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Service
{
    public class UserPermissionService : IUserPermissionService
    {
        #region Constructor
        private readonly ModelStateDictionary _modelState;
        private readonly IUserRepository _userRepository;
        private readonly IPermissionRepository _permissionRepository;
        private readonly IUserPermissionRepository _userPermissionRepository;

        public UserPermissionService
        (
            ModelStateDictionary modelState,
            IUserRepository userRepository,
            IPermissionRepository permissionRepository,
            IUserPermissionRepository userPermissionRepository)
        {
            _modelState = modelState;
            _userRepository = userRepository;
            _permissionRepository = permissionRepository;
            _userPermissionRepository = userPermissionRepository;
        }
        #endregion

        #region List
        public async Task<IEnumerable<PermissionDto>> ListAsync(int userId)
        {
            var user = await _userRepository.GetAsync(userId);
            var permissions = await _permissionRepository.ListByUserIdAsync(user.Id);
            return permissions.ToDtoList();
        }
        #endregion

        #region Update
        public async Task UpdateUserPermissionsAsync(UpdateUserPermissionsDto updateUserPermissionsDto)
        {
            var user = await _userRepository.GetAsync(updateUserPermissionsDto.Id);
            if (user == null)
                _modelState.AddModelError(nameof(UpdateUserPermissionsDto.Id), "User not found");

            await ValidatePermissionIdsAsync(updateUserPermissionsDto.PermissionIds);

            if (!_modelState.IsValid)
                return;

            var currentPermissionIds = (await _permissionRepository.ListByUserIdAsync(user.Id)).Select(p => p.Id);
            var toDeletePermission = currentPermissionIds.Except(updateUserPermissionsDto.PermissionIds);
            if (toDeletePermission.Any())
            {
                await _userPermissionRepository.DeletePermissionsForUserAsync(user.Id, toDeletePermission);
            }

            var toInsertPermissions = updateUserPermissionsDto.PermissionIds.Except(currentPermissionIds);
            if (toInsertPermissions.Any())
                await _userPermissionRepository.CreatePermissionsForUserAsync(user.Id, toInsertPermissions);
        }

        private async Task ValidatePermissionIdsAsync(IEnumerable<int> permissionIds)
        {
            var allPermissionsIds = (await _permissionRepository.ListAsync()).Select(p => p.Id);
            foreach (var permissionId in permissionIds)
            {
                if (!allPermissionsIds.Contains(permissionId))
                {
                    _modelState.AddModelError(nameof(UpdateUserPermissionsDto.PermissionIds), $"Invalid Permission id {permissionId}");
                }
            }
        }
        #endregion

    }
}
