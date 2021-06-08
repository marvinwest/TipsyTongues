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
        public String ErrorMessageDisplay
        {
            get { return errorMessageDisplay; }
            set { errorMessageDisplay = value;
                OnPropertyChanged("ErrorMessageDisplay");}
        }

        public ErrorPage(String errorMessage)
        {
            BindingContext = this;
            ErrorMessageDisplay = errorMessage;
            InitializeComponent();
        }

        private async void MainPage_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
            Navigation.RemovePage(this);
        }
    }
}