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

        public static async Task<bool> checkOrGetMicrophonePermission()
        {
            var status = await CrossPermissions.Current.CheckPermissionStatusAsync<MicrophonePermission>();
            if (status == PermissionStatus.Granted)
            {
                return true;
            }
            else
            {
                status = await CrossPermissions.Current.RequestPermissionAsync<MicrophonePermission>();
                if (status == PermissionStatus.Granted)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static async Task<bool> checkOrGetStoragePermission()
        {
            var status = await CrossPermissions.Current.CheckPermissionStatusAsync<StoragePermission>();
            if (status == PermissionStatus.Granted)
            {
                return true;
            }
            else
            {
                status = await CrossPermissions.Current.RequestPermissionAsync<StoragePermission>();
                if (status == PermissionStatus.Granted)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

    }
}
