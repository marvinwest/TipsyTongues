using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Lottie.Forms;

namespace ClientApp
{

    public partial class LoadingPage : ContentPage
    {
        private ElementSizeService elementSizeService;

        public LoadingPage()
        {
            InitializeComponent();
            
            double animationHeight = animationView.Height;
            Console.WriteLine(animationHeight);
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}