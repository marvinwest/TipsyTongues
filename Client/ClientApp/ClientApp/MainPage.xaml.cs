using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

namespace ClientApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void NavigateButton_OnClicked(object sender, EventArgs e)
        {
            if(await checkOrGetPermissions())
            {
                await Navigation.PushAsync(new SecondPage());
            }
            else
            {
                await Navigation.PushAsync(new ErrorPage("This app needs Permission to Microphone and Storage, otherwise it can#t be used"));
            }
        }

        //checks wether the needed Permissions are given, requests them if not
        // TODO: maybe extract into own service, to use in later sites (e. g. when microphone is used)
        //       maybe split into checking and getting permissions.
        //       works for now, but if user deactivates Permissions while running later parts of the app -> Exception (fix it)
        private async Task<bool> checkOrGetPermissions()
        {
            bool permissionsGranted = true;
            var permissionsList = new List<Permission>()
            {
                Permission.Microphone,
                Permission.Storage
            };

            var permissionsNeededList = new List<Permission>();
            foreach(var permission in permissionsList)
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(permission);
                if (status != PermissionStatus.Granted)
                {
                    permissionsNeededList.Add(permission);
                }
            }

            var results = await CrossPermissions.Current.RequestPermissionsAsync(permissionsNeededList.ToArray());
            foreach(var permission in permissionsNeededList)
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
