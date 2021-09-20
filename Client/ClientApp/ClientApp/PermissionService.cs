using System.Threading.Tasks;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

namespace ClientApp
{

    /**
     * PermissionService:
     * Inherits Methods to check Permissionstatus of microphone- and storagepermission.
     * Microphone and Storage permissions are needed for the app to function.
     **/
    class PermissionService
    {

        /**
         * Checks if the Microphone permission is given.
         * If the permission is not given, the user is asked for the permission.
         * Returns true if the permission is given, false if the permission is not given.
         **/
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

        /**
         * Checks if the Microphone permission is given.
         * If the permission is not given, the user is asked for the permission.
         * Returns true if the permission is given, false if the permission is not given.
         **/
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
