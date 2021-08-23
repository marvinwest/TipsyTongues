using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ClientApp
{
    

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
