namespace ILS_BE.Application.Authorization
{
    public class UserPermissionLoaderHostedService : IHostedService
    {
        private readonly UserPermissionStore _userPermissionStore;

        public UserPermissionLoaderHostedService(UserPermissionStore userPermissionStore)
        {
            _userPermissionStore = userPermissionStore;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _userPermissionStore.LoadPermissions();
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
