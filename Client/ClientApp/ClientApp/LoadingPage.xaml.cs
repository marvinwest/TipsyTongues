using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ClientApp
{

    public partial class LoadingPage : ContentPage
    {

        //Lottie:
        //Placeholder-File = Mercury_navigation_refresh.json TODO: replace by actual animation, wait for design
        //  - Android: Add to Assets-folder, set Build action to AndroidAsset in Properties
        //  - Apple: Add to to root clientapp.ios-folder, set Build action to BundleResource
        public LoadingPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}