using Core.Application.Interfaces.Repositories;


namespace Core.Persistence.Repositories
{
    public class PermissionService : IPermissionService
    {
        private readonly IMenuPermissionRepository _menuPermissionRepository;

        public PermissionService(IMenuPermissionRepository menuPermissionRepository)
        {
            _menuPermissionRepository = menuPermissionRepository;
        }

        public  Dictionary<string, int> GetPermissionsAsync()
        {
            var permissions = new Dictionary<string, int>();
            var menuPermissions = _menuPermissionRepository.GetMenuPermissionsAsync();

            foreach ( var item in menuPermissions) 
            {
                permissions.Add("Permission."+item.ModName+"."+item.MnuName,item.MnuID);
            }
            return permissions;
        }
    }
}
