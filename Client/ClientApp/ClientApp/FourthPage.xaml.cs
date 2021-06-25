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
                case 0: LevelOfDrunkennessText = "sounds sober, amen"; LevelOfDrunkennessImage = "beer0.png"; break;
                case 1: LevelOfDrunkennessText = "the night is still young"; LevelOfDrunkennessImage = "beer1.png"; break;
                case 2: LevelOfDrunkennessText = "somebody is getting tipsy"; LevelOfDrunkennessImage = "beer2.png"; break;
                case 3: LevelOfDrunkennessText = "sloppy drunk (absolutely hammered?)"; LevelOfDrunkennessImage = "beer3.png"; break;
                case 4: LevelOfDrunkennessText = "you probably had one too many"; LevelOfDrunkennessImage = "beer4.png"; break;
                default:
                    await Navigation.PushAsync(new ErrorPage("Something wnet wrong, please try again"));
                    Navigation.RemovePage(this);
                    break;
            }
        }

    }

}