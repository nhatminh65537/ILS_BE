using ILS_BE.Domain.DTOs;
using ILS_BE.Domain.Models;
using ILS_BE.Domain.Interfaces;
using ILS_BE.Application.Interfaces;
using AutoMapper;
using ILS_BE.Application.Authorization;

namespace ILS_BE.Application.Services
{
    public class RoleService : DataService<Role, RoleDTO>, IRoleService
    {
        private readonly IRepository<Permission> _permissionRepository;
        private readonly IRepository<RolePermission> _rolePermissionRepository;
        private readonly UserPermissionStore _userPermissionStore;

        public RoleService(
            IRepository<Role> roleRepository,
            IRepository<Permission> permissionRepository,
            IRepository<RolePermission> rolePermissionRepository,
            UserPermissionStore userPermissionStore,
            IMapper mapper ) : base(roleRepository, mapper)
        {
            _permissionRepository = permissionRepository;
            _rolePermissionRepository = rolePermissionRepository;
            _userPermissionStore = userPermissionStore;
        }

        public override async Task UpdateAsync(RoleDTO roleDTO)
        {
            Role? role = await _repository.GetByIdAsync(roleDTO.Id) 
                ?? throw new KeyNotFoundException("Role not found");

            if (!role.Changeable) throw new Exception("Can not delete this role");

            _mapper.Map(roleDTO, role);

            await _repository.UpdateAsync(role);
            await _repository.SaveAsync();
        }

        public override async Task DeleteAsync(int roleId)
        {
            Role? role = await _repository.GetByIdAsync(roleId)
                ?? throw new KeyNotFoundException("Role not found");

            if (!role.Changeable) throw new Exception("Can not delete this role");

            await _repository.DeleteAsync(roleId);
            await _repository.SaveAsync();
        }

        public async Task<List<PermissionDTO>> GetPermissionsInRoleAsync(int roleId)
        {
            var rolePermissions = await _rolePermissionRepository.GetWhereAsync(
                rp => rp.RoleId == roleId
            );
            var permissionIds = rolePermissions
                                  .Select(rp => rp.PermissionId);

            var permissions = await _permissionRepository.GetWhereAsync(
                p => permissionIds.Contains(p.Id)
            );

            return _mapper.Map<List<PermissionDTO>>(permissions);
        }

        public async Task AddPermissionToRoleAsync(int roleId, int permissionId)
        {
            var rolePermission = new RolePermission
            {
                RoleId = roleId,
                PermissionId = permissionId,
            };
            await _rolePermissionRepository.AddAsync(rolePermission);
            await _rolePermissionRepository.SaveAsync();

            // Update the user permission store to reflect the new permission
            await _userPermissionStore.LoadPermissions();
        }

        public async Task RemovePermissionFromRoleAsync(int roleId, int permissionId)
        {
            await _rolePermissionRepository.DeleteWhereAsync(
                rp => rp.RoleId == roleId && rp.PermissionId == permissionId
            );
            await _rolePermissionRepository.SaveAsync();

            // Update the user permission store to reflect the removed permission
            await _userPermissionStore.LoadPermissions();
        }
    }
}
