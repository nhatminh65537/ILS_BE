﻿using ILS_BE.Domain.DTOs;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace ILS_BE.Application.Interfaces
{
    public interface IMyUserService
    {
        public Task<UserDetailDTO> GetMyUser();
        public Task UpdateMyUserAsync(UserDetailDTO myUserDTO);
        public Task<List<PermissionDTO>> GetPermissionsInMyUserAsync();
        public Task<List<RoleDTO>> GetRolesInMyUserAsync();
        public Task<UserProfileDTO> GetUserProfileInMyUserAsync();
        public Task<List<UserModuleProgressDTO>> GetModuleProgressAsync();
        public Task UpdateLearnModuleProgress(UserModuleProgressCreateOrUpdateDTO progressDTO);
        public Task UpdateLearnLessonFinish(int lessonId);
        public Task<List<int>> GetLessonFinishAsync(int moduleId);
    }
}
