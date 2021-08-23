using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ClientApp
{
    public partial class ErrorPage : ContentPage
    {
        private String errorMessageDisplay;

        private ElementSizeService elementSizeService;

        private Double frameHeight;
        private Double frameWidth;

        private double menuButtonHeight;
        private double menuButtonWidth;

        public ErrorPage(String errorMessage)
        {
            errorMessageDisplay = errorMessage;

            elementSizeService = new ElementSizeService();

            FrameHeight = elementSizeService.calculateElementHeight(0.5);
            FrameWidth = elementSizeService.calculateElementWidth(0.9);

            menuButtonHeight = elementSizeService.calculateElementHeight(0.1);
            menuButtonWidth = elementSizeService.calculateElementWidth(0.4);

            BindingContext = this;
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void MainPage_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
            Navigation.RemovePage(this);
        }

        public String ErrorMessageDisplay
        {
            get { return errorMessageDisplay; }
            set
            {
                errorMessageDisplay = value;
                OnPropertyChanged("ErrorMessageDisplay");
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

        public Double MenuButtonWidth
        {
            get { return menuButtonWidth; }
            set { menuButtonWidth = value; }
        }

        public Double MenuButtonHeight
        {
            get { return menuButtonHeight; }
            set { menuButtonHeight = value; }
        }

    }

}