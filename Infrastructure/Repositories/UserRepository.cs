using ILS_BE.Domain.Interfaces;
using ILS_BE.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ILS_BE.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<User?> GetUserDetailAsync(int id)
        {
            try
            {
                return await _dbSet
                    .Include(u => u.Profile)
                    .Include(u => u.Permissions)
                    .Include(u => u.Roles)
                    .FirstOrDefaultAsync(u => u.Id == id);
            }
            catch (Exception ex)
            {
                // Log exception
                throw new Exception($"Could not retrieve entity with id {id}", ex);
            }
            
        }
        public async Task<List<Permission>> GetUserPermissionsAsync(int userId)
        {
            try
            {
                var user = await _dbSet
                    .Include(u => u.Permissions)
                    .FirstOrDefaultAsync(u => u.Id == userId)
                    ?? throw new Exception("User not found");
                return user.Permissions;
                
            }
            catch (Exception ex)
            {
                // Log exception
                throw new Exception($"Could not permissions of user has id {userId}", ex);
            }
        }
        public async Task<List<Role>> GetUserRolesAsync(int userId)
        {
            try
            {
                var user = await _dbSet
                    .Include(u => u.Roles)
                    .FirstOrDefaultAsync(u => u.Id == userId)
                    ?? throw new Exception("User not found");
                return user.Roles;
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not retrievw roles of user has id {userId}", ex);
            }
        }
        public async Task<UserProfile> GetUserProfileAsync(int userId)
        {
            try
            {
                var userProfile = await _dbSet
                    .Include(u => u.Profile)
                    .FirstOrDefaultAsync(up => up.Id == userId)
                    ?? throw new Exception("User profile not found");

                return userProfile.Profile;
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not retrieve user profile of user has id {userId}", ex);
            }
        }
        public User? GetUserDetail(int id)
        {
            try
            {
                return _dbSet
                    .Include(u => u.Profile)
                    .Include(u => u.Permissions)
                    .FirstOrDefault(u => u.Id == id);
            }
            catch (Exception ex)
            {
                // Log exception
                throw new Exception($"Could not retrieve entity with id {id}", ex);
            }
        }
        public List<Permission> GetUserPermissions(int userId)
        {
            try
            {
                var user = _dbSet
                    .Include(u => u.Permissions)
                    .FirstOrDefault(u => u.Id == userId)
                    ?? throw new Exception("User not found");

                return user.Permissions;

            }
            catch (Exception ex)
            {
                // Log exception
                throw new Exception($"Could not permissions of user has id {userId}", ex);
            }
        }
        public List<Role> GetUserRoles(int userId)
        {
            try
            {
                var user = _dbSet
                    .Include(u => u.Roles)
                    .FirstOrDefault(u => u.Id == userId)
                    ?? throw new Exception("User not found");

                return user.Roles;
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not retrievw roles of user has id {userId}", ex);
            }
        }
        public UserProfile GetUserProfile(int userId)
        {
            try
            {
                var userProfile = _dbSet
                    .Include(u => u.Profile)
                    .FirstOrDefault(up => up.Id == userId)
                    ?? throw new Exception("User profile not found");
                return userProfile.Profile;
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not retrieve user profile of user has id {userId}", ex);
            }
        }
    }
}
