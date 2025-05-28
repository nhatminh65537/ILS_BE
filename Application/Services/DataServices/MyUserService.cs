using AutoMapper;
using ILS_BE.Application.Interfaces;
using ILS_BE.Domain.DTOs;
using ILS_BE.Domain.Interfaces;
using ILS_BE.Domain.Models;

namespace ILS_BE.Application.Services
{
    public class MyUserService : IMyUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        public MyUserService(
            IUserRepository userRepository,
            IAuthService authService,
            IMapper mapper,
            IUserService userService)
        {
            _userRepository = userRepository;
            _authService = authService;
            _mapper = mapper;
            _userService = userService;
        }
        public async Task<UserDetailDTO> GetMyUser()
        {
            var userId = _authService.GetUserId();
            var user = await _userRepository.GetUserDetailAsync(userId) ?? throw new Exception("User not found");
            var userDto = _mapper.Map<UserDetailDTO>(user);
            userDto.Permissions = await _userService.GetEffectivePermissionsOfUserAsync(userId);

            return userDto;
        }
        public async Task UpdateMyUserAsync(UserDetailDTO userDetailDTO)
        {
            var userId = _authService.GetUserId();
            var user = await _userRepository.GetUserDetailAsync(userId)
                ?? throw new Exception("User not found");

            _mapper.Map(userDetailDTO, user);

            await _userRepository.UpdateAsync(user);
            await _userRepository.SaveAsync();
        }

        public async Task<List<PermissionDTO>> GetPermissionsInMyUserAsync()
        {
            var userId = _authService.GetUserId();
            var permissions = await _userService.GetEffectivePermissionsOfUserAsync(userId);
            return permissions;
        }

        public async Task<List<RoleDTO>> GetRolesInMyUserAsync()
        {
            var userId = _authService.GetUserId();
            var roles = await _userRepository.GetUserRolesAsync(userId);
            return _mapper.Map<List<RoleDTO>>(roles);
        }

        public async Task<UserProfileDTO> GetUserProfileInMyUserAsync()
        {
            var userId = _authService.GetUserId();
            var userProfile = await _userRepository.GetUserProfileAsync(userId);
            return _mapper.Map<UserProfileDTO>(userProfile);
        }

        public async Task<List<UserModuleProgressDTO>> GetModuleProgressAsync()
        {
            var userId = _authService.GetUserId();
            var userModuleProgressDto = await _userService.GetUserModuleProgressAsync(userId);
            return userModuleProgressDto;
        }

        public async Task UpdateLearnModuleProgress(UserModuleProgressCreateOrUpdateDTO progressDTO)
        {
            var userId = _authService.GetUserId();
            progressDTO.UserId = userId;
            await _userService.UpdateUserLearnModuleProgressAsync(progressDTO);
        }

        public async Task UpdateLearnLessonFinish(int lessonId)
        {
            var userId = _authService.GetUserId();
            await _userService.UpdateUserLearnLessonFinishAsync(lessonId, userId);
        }

        public async Task<List<int>> GetLessonFinishAsync(int moduleId)
        {
            var userId = _authService.GetUserId();
            var lessons = await _userService.GetUserLessonFinishAsync(userId, moduleId);
            return lessons.Select(l => l.Id).ToList();
        }
    }
}
