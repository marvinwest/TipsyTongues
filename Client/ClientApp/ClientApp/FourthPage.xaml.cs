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
        private String levelOfDrunkennessText;
        public String LevelOfDrunkennessText
        {
            get { return levelOfDrunkennessText; }
            set
            {
                levelOfDrunkennessText = value;
                OnPropertyChanged("LevelOfDrunkenness");
            }
        }

        private String levelOfDrunkennessImage;
        public String LevelOfDrunkennessImage
        {
            get { return levelOfDrunkennessImage; }
            set
            {
                levelOfDrunkennessImage = value;
                OnPropertyChanged("LevelOfDrunkenness");
            }
        }

        public FourthPage(int levelOfDrunkenness)
        {
            BindingContext = this;
            BuildLevelOfDrunkennessDisplay(levelOfDrunkenness);
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void SecondPage_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SecondPage());
            Navigation.RemovePage(this);
        }

        private async void BuildLevelOfDrunkennessDisplay(int levelOfDrunkenness)
        {
            switch (levelOfDrunkenness)
            {
                case 0: LevelOfDrunkennessText = "YOU'RE SOBER, WHY?"; LevelOfDrunkennessImage = "beer0.png"; break;
                case 1: LevelOfDrunkennessText = "SLIGHTLY TIPSY - MAYBE A\nSHOT WOULD HELP"; LevelOfDrunkennessImage = "beer1.png"; break;
                case 2: LevelOfDrunkennessText = "SOMEBODY'S GETTING\nTIPSY"; LevelOfDrunkennessImage = "beer2.png"; break;
                case 3: LevelOfDrunkennessText = "SLOPPY DRUNK - OF COURSE\nIT'S A GOOD IDEA TO GET\nANOTHER ROUND"; LevelOfDrunkennessImage = "beer3.png"; break;
                case 4: LevelOfDrunkennessText = "ABSOLUTELY HAMMERED - GO\nGET YOURSELF A GLASS OF WATER"; LevelOfDrunkennessImage = "beer4.png"; break;
                default:
                    await Navigation.PushAsync(new ErrorPage("Something went wrong, please try again"));
                    Navigation.RemovePage(this);
                    break;
            }
        }

    }

}