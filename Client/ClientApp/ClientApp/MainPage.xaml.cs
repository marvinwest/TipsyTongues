using System;
using Xamarin.Forms;

namespace ClientApp
{

    /**
     * Startinng page of the application after splash page.
     * The whole page is an ImageButton.
     * On touch, the user is asked for the permission to use microphone and storage, if that is not given.
     * If the permissions are given, the recordingpage is loaded. Otherwise an Errorpage is shown.
     **/
    public partial class MainPage : ContentPage
    {
        
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void NavigateButton_OnClicked(object sender, EventArgs e)
        {
            if (await PermissionService.checkOrGetMicrophonePermission() && await PermissionService.checkOrGetStoragePermission())
            {
                await Navigation.PushAsync(new SecondPage());
            }
            else
            {
                await Navigation.PushAsync(new ErrorPage("This app needs Permission to Microphone and Storage, otherwise it can´t be used"));
            }
        }

    }

}
