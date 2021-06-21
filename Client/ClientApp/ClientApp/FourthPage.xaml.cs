using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ClientApp
{
    
    public partial class FourthPage : ContentPage
    {
        private int levelOfDrunkennessDisplay;
        public int LevelOfDrunkennessDisplay
        {
            get { return levelOfDrunkennessDisplay; }
            set { levelOfDrunkennessDisplay = value;
                OnPropertyChanged("LevelOfDrunkenness"); }
        }

        

        public FourthPage(int levelOfDrunkennness)
        {
            BindingContext = this;
            LevelOfDrunkennessDisplay = levelOfDrunkennness;
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);


        }

       

        private async void SecondPage_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SecondPage());
            Navigation.RemovePage(this);
        }
    }

}