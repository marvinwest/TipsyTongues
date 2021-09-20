using System;
using Xamarin.Forms;

namespace ClientApp
{
    /**
     * ResultPage:
     * The calculated result is displayed in the form of a filled beermug. Furthermore a sentence is shown.
     * By click on the button on the bottom, the RecordingPage is loaded, so the user can try again.
     **/
    public partial class ResultPage : ContentPage
    {
        private ElementSizeService elementSizeService;

        private String levelOfDrunkennessText;
        private String levelOfDrunkennessImage;

        private Double frameHeight;
        private Double frameWidth;
        private Double levelOfDrunkennessImageHeight;
        private Double levelOfDrunkennessImageWidth;
        private Double homeButtonHeight;
        private Double homeButtonWidth;

        /**
         * On Initilization:
         * ElementSizeService is loaded.
         * Width and Height of the shown elements are calculated.
         * ResultImage and ResultText are set according to the forwarded levelOfDrunkenness-Integer.
         **/
        public ResultPage(int levelOfDrunkenness)
        {
            elementSizeService = new ElementSizeService();

            FrameHeight = elementSizeService.calculateElementHeight(0.60);
            FrameWidth = elementSizeService.calculateElementWidth(0.9);
            LevelOfDrunkennessImageHeight = elementSizeService.calculateElementHeight(0.35);
            LevelOfDrunkennessImageWidth = elementSizeService.calculateElementWidth(0.9);
            HomeButtonHeight = elementSizeService.calculateElementHeight(0.125);
            HomeButtonWidth = elementSizeService.calculateElementWidth(0.9);

            BindingContext = this;
            BuildLevelOfDrunkennessDisplay(levelOfDrunkenness);
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        /**
         * On click the RecordingPage is loaded.
         **/ 
        private async void RecordingPage_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RecordingPage());
            Navigation.RemovePage(this);
        }

        /**
         * ResultImage and ResultText are set according to levelOfDrunkenness-Integer.
         **/
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

        // Following are the properties for valuebinding in the XAML.

        public String LevelOfDrunkennessText
        {
            get { return levelOfDrunkennessText; }
            set
            {
                levelOfDrunkennessText = value;
                OnPropertyChanged("LevelOfDrunkenness");
            }
        }

        public String LevelOfDrunkennessImage
        {
            get { return levelOfDrunkennessImage; }
            set
            {
                levelOfDrunkennessImage = value;
                OnPropertyChanged("LevelOfDrunkenness");
            }
        }

        public Double FrameHeight
        {
            get { return frameHeight; }
            set { frameHeight = value; }
        }

        public Double FrameWidth
        {
            get { return frameWidth; }
            set { frameWidth = value; }
        }

        public Double LevelOfDrunkennessImageHeight
        {
            get { return levelOfDrunkennessImageHeight; }
            set { levelOfDrunkennessImageHeight = value; }
        }

        public Double LevelOfDrunkennessImageWidth
        {
            get { return levelOfDrunkennessImageWidth; }
            set { levelOfDrunkennessImageWidth = value; }
        }

        public Double HomeButtonHeight
        {
            get { return homeButtonHeight; }
            set { homeButtonHeight = value; }
        }

        public Double HomeButtonWidth
        {
            get { return homeButtonWidth; }
            set { homeButtonWidth = value; }
        }

    }

}