using ILS_BE.Domain.DTOs;
using ILS_BE.Domain.Models;
using ILS_BE.Infrastructure.Repositories;
using ILS_BE.Domain.Interfaces;
using Microsoft.AspNetCore.Components;
using AutoMapper;
using ILS_BE.Application.Interfaces;

namespace ILS_BE.Application.Services
{
    public class UserService : DataService<User, UserDTO>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IGenericRepository<UserRole> _userRoleRepository;
        private readonly IGenericRepository<Permission> _permissionRepository;
        private readonly IGenericRepository<UserPermission> _userPermissionRepository;
        private readonly IGenericRepository<UserEffectivePermission> _userEffectivePermissionRepository;

        public UserService(
            IUserRepository userRepository,
            IGenericRepository<UserRole> userRoleRepository,
            IGenericRepository<Permission> permissionRepository,
            IGenericRepository<UserPermission> userPermissionRepository,
            IGenericRepository<UserEffectivePermission> userEffectivePermissionRepository,
            IMapper mapper) : base(userRepository, mapper)
        {
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            _permissionRepository = permissionRepository;
            _userPermissionRepository = userPermissionRepository;
            _userEffectivePermissionRepository = userEffectivePermissionRepository;
        }

        public async Task<UserDTO> GetByUsernameAsync(string username)
        {
            var user = await _userRepository.GetFirstWhereAsync(u => u.UserName == username);
            return _mapper.Map<UserDTO>(user);
        }
        public async Task<UserDTO> GetByEmailAsync(string email)
        {
            var user = await _userRepository.GetFirstWhereAsync(u => u.Email == email);
            return _mapper.Map<UserDTO>(user);
        }
        public async Task<UserProfileDTO> GetUserProfileAsync(int userId)
        {
            var userProfile = await _userRepository.GetUserProfileAsync(userId);
            return _mapper.Map<UserProfileDTO>(userProfile);
        }

        public async Task<List<RoleDTO>> GetRolesOfUserAsync(int userId)
        {
            var roles = await _userRepository.GetUserRolesAsync(userId);
            return _mapper.Map<List<RoleDTO>>(roles);
        }

        public async Task AddRoleToUserAsync(int userId, int roleId)
        {
            var userRole = new UserRole { UserId = userId, RoleId = roleId};
            await _userRoleRepository.AddAsync(userRole);
            await _userRoleRepository.SaveAsync();
        }

        public async Task RemoveRoleFromUserAsync(int userId, int roleId)
        {
            await _userRoleRepository.DeleteWhereAsync(ur => ur.UserId == userId && ur.RoleId == roleId);
            await _userRoleRepository.SaveAsync();
        }

        public async Task<List<PermissionDTO>> GetEffectivePermissionsOfUserAsync(int userId)
        {
            var userPermissions = await _userEffectivePermissionRepository.GetWhereAsync(upv => upv.UserId == userId);
            var permissionIds = userPermissions.Select(up => up.PermissionId);
            var permissions = await _permissionRepository.GetWhereAsync(p => permissionIds.Contains(userId));
            return _mapper.Map<List<PermissionDTO>>(permissions);
        }

        public async Task AddPermissionToUserAsync(int userId, int permissionId)
        {
            var userPermission = new UserPermission { UserId = userId, PermissionId = permissionId };
            await _userPermissionRepository.AddAsync(userPermission);
            await _userPermissionRepository.SaveAsync();
        }

        public async Task RemovePermissionFromUserAsync(int userId, int permissionId)
        {
            await _userPermissionRepository.DeleteWhereAsync(up => up.UserId == userId && up.PermissionId == permissionId);
            await _userPermissionRepository.SaveAsync();
        }


    }
}
