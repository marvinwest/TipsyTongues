using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

namespace ClientApp
{
    class PermissionService
    {
        public static async Task<bool> checkOrGetPermissions()
        {
            bool permissionsGranted = true;
            var permissionsList = new List<Permission>()
            {
                Permission.Microphone,
                Permission.Storage
            };

            var permissionsNeededList = new List<Permission>();
            foreach (var permission in permissionsList)
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(permission);
                if (status != PermissionStatus.Granted)
                {
                    permissionsNeededList.Add(permission);
                }
            }

            var results = await CrossPermissions.Current.RequestPermissionsAsync(permissionsNeededList.ToArray());
            foreach (var permission in permissionsNeededList)
            {
                var status = PermissionStatus.Unknown;
                if (results.ContainsKey(permission))
                {
                    status = results[permission];
                }
                if (status == PermissionStatus.Granted || status == PermissionStatus.Unknown)
                {
                    permissionsGranted = true;
                }
                else
                {
                    permissionsGranted = false;
                    break;
                }
            }

            return permissionsGranted;
        }
    }

}
