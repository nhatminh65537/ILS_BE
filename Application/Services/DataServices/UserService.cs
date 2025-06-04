using ILS_BE.Domain.DTOs;
using ILS_BE.Domain.Models;
using ILS_BE.Infrastructure.Repositories;
using ILS_BE.Domain.Interfaces;
using Microsoft.AspNetCore.Components;
using AutoMapper;
using ILS_BE.Application.Interfaces;
using System.Collections.Generic;

namespace ILS_BE.Application.Services
{
    public class UserService : DataService<User, UserDTO>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRepository<UserProfile> _userProfileRepository;
        private readonly IRepository<UserRole> _userRoleRepository;
        private readonly IRepository<Permission> _permissionRepository;
        private readonly IRepository<UserPermission> _userPermissionRepository;
        private readonly IPaginatedRepository<LearnModule> _learnModuleRepository;
        private readonly IRepository<LearnNode> _learnNodeRepository;
        private readonly IRepository<UserFinishedLesson> _userFinishedLessonRepository;
        private readonly IRepository<UserModuleProgress> _userModuleProgressRepository;
        private readonly IRepository<LearnProgressState> _learnProgressStateRepository;
        private readonly UserEffectivePermissionRepository _userEffectivePermissionRepository;

        public UserService(
            IUserRepository userRepository,
            IRepository<UserProfile> userProfileRepository,
            IRepository<UserRole> userRoleRepository,
            IRepository<Permission> permissionRepository,
            IRepository<UserPermission> userPermissionRepository,
            IPaginatedRepository<LearnModule> learnModuleRepository,
            IRepository<LearnNode> learnNodeRepository,
            IRepository<UserFinishedLesson> userFinishedLessonRepository,
            IRepository<UserModuleProgress> userModuleProgressRepository,
            IRepository<LearnProgressState> learnProgressStateRepository,
            UserEffectivePermissionRepository userEffectivePermissionRepository,
            IMapper mapper) : base(userRepository, mapper)
        {
            _userRepository = userRepository;
            _userProfileRepository = userProfileRepository;
            _userRoleRepository = userRoleRepository;
            _permissionRepository = permissionRepository;
            _userPermissionRepository = userPermissionRepository;
            _learnModuleRepository = learnModuleRepository;
            _learnNodeRepository = learnNodeRepository;
            _userFinishedLessonRepository = userFinishedLessonRepository;
            _userModuleProgressRepository = userModuleProgressRepository;
            _learnProgressStateRepository = learnProgressStateRepository;
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
            var userRole = new UserRole { UserId = userId, RoleId = roleId };
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
            var userPermissions = await _userEffectivePermissionRepository.GetUserEffectivePermissionsAsync(userId);
            var permissionIds = userPermissions.Select(up => up.PermissionId);
            var permissions = await _permissionRepository.GetWhereAsync(p => permissionIds.Contains(p.Id));
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

        public async Task<List<UserModuleProgressDTO>> GetUserModuleProgressAsync(int userId)
        {
            var userModules = await _userModuleProgressRepository.GetWhereAsync(up => up.UserId == userId);
            List<UserModuleProgressDTO> userModuleProgressDTOs = new List<UserModuleProgressDTO>();
            for (int i = 0; i < userModules.Count; i++)
            {
                var userModule = userModules[i];
                var progressState = await _learnProgressStateRepository.GetByIdAsync(userModule.ProgressStateId);
                var lessons = await GetUserLessonFinishAsync(userId, userModule.ModuleId);
                var sumDuration = lessons.Sum(l => l.Duration);
                var module = await _learnModuleRepository.GetByIdAsync(userModule.ModuleId)
                    ?? throw new Exception($"Module with ID {userModule.ModuleId} not found");
                userModuleProgressDTOs.Add(new UserModuleProgressDTO
                {
                    UserId = userModule.UserId,
                    ModuleId = userModule.ModuleId,
                    ProgressState = _mapper.Map<LearnProgressStateDTO>(progressState),
                    ProgressPercentage = (float)sumDuration / module.Duration * 100
                });
            }
            return userModuleProgressDTOs;
        }

        public async Task<List<LearnLessonNodeDTO>> GetUserLessonFinishAsync(int userId, int moduleId)
        {
            var userFinishedLessonIds = (await _userFinishedLessonRepository.GetWhereAsync(ufl => ufl.UserId == userId)).Select(ufl => ufl.LessonId).ToList();
            var module = await _learnModuleRepository.GetByIdAsync(moduleId)
                ?? throw new Exception($"Module with ID {moduleId} not found");
            var path = '.' + module.Node.Id.ToString() + '.';
            var userModuleLesson = await _learnNodeRepository.GetWhereAsync(n => n.Path.StartsWith(path) && n.IsLesson && userFinishedLessonIds.Contains(n.Lesson!.Id));
            return _mapper.Map<List<LearnLessonNodeDTO>>(userModuleLesson.Select(n => n.Lesson));
        }

        public async Task UpdateUserLearnModuleProgressAsync(UserModuleProgressCreateOrUpdateDTO userModuleProgressCreateOrUpdateDTO)
        {
            var userModuleProgress = _mapper.Map<UserModuleProgress>(userModuleProgressCreateOrUpdateDTO);
            var existingProgress = await _userModuleProgressRepository.GetFirstWhereAsync(
                up => up.UserId == userModuleProgress.UserId && up.ModuleId == userModuleProgress.ModuleId);
            if (existingProgress != null)
            {
                existingProgress.ProgressStateId = userModuleProgress.ProgressStateId;
                await _userModuleProgressRepository.UpdateAsync(existingProgress);
            }
            else
            {
                await _userModuleProgressRepository.AddAsync(userModuleProgress);
            }
            await _userModuleProgressRepository.SaveAsync();
        }

        public async Task UpdateUserLearnLessonFinishAsync(int lessonId, int userId)
        {
            var userFinishedLesson = new UserFinishedLesson
            {
                UserId = userId,
                LessonId = lessonId,
                CreatedAt = DateTime.UtcNow
            };
            var existingLesson = await _userFinishedLessonRepository.GetFirstWhereAsync(
                ufl => ufl.UserId == userFinishedLesson.UserId && ufl.LessonId == userFinishedLesson.LessonId);
            if (existingLesson == null)
            {
                await _userFinishedLessonRepository.AddAsync(userFinishedLesson);
                var userProfile = await _userRepository.GetUserProfileAsync(userId)
                    ?? throw new Exception($"User with ID {userId} not found");
                var lessonNode = (await _learnNodeRepository.GetWhereAsync(n => n.LessonId == lessonId))
                    .FirstOrDefault()
                    ?? throw new Exception($"Lesson with ID {lessonId} not found");
                userProfile.Xp += lessonNode.Lesson!.Xp;

                userProfile.Level = (int)Math.Floor(userProfile.Xp / 1000.0);
                await _userProfileRepository.UpdateAsync(userProfile);
                await _userProfileRepository.SaveAsync();

                var rootId = lessonNode.Path.Split('.').Skip(1).FirstOrDefault();
                var module = await _learnModuleRepository.GetFirstWhereAsync(m => m.NodeId == int.Parse(rootId!));

                var lessons = await GetUserLessonFinishAsync(userId, module!.Id);
                var sumDuration = lessons.Sum(l => l.Duration);

                if (sumDuration == module.Duration)
                {
                    var progressState = await _learnProgressStateRepository.GetFirstWhereAsync(ps => ps.Name == "Completed");
                    if (progressState == null)
                    {
                        throw new Exception("Progress state 'Completed' not found");
                    }
                    var userModuleProgress = await _userModuleProgressRepository.GetFirstWhereAsync(
                        up => up.UserId == userId && up.ModuleId == module.Id);
                    if (userModuleProgress != null)
                    {
                        userModuleProgress.ProgressStateId = progressState.Id;
                        await _userModuleProgressRepository.UpdateAsync(userModuleProgress);
                    }
                    else
                    {
                        userModuleProgress = new UserModuleProgress
                        {
                            UserId = userId,
                            ModuleId = module.Id,
                            ProgressStateId = progressState.Id
                        };
                        await _userModuleProgressRepository.AddAsync(userModuleProgress);
                    }
                    await _userModuleProgressRepository.SaveAsync();
                }

            }
            await _userFinishedLessonRepository.SaveAsync();
        }

        public async Task<PaginatedResult<UserPublicDTO>> GetPaginatedAsync(int page, int pageSize)
        {
            var paginatedResult = await _userRepository.GetUserOrderByXpAsync(page, pageSize);
            return new PaginatedResult<UserPublicDTO>
            {
                CurrentPage = paginatedResult.CurrentPage,
                TotalItems = paginatedResult.TotalItems,
                TotalPages = paginatedResult.TotalPages,
                Items = _mapper.Map<List<UserPublicDTO>>(paginatedResult.Items)
            };
        }
    }
}
