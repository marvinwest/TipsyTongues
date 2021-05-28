using System;

using Xamarin.Forms;

namespace ClientApp
{
    public class ThirdPage : ContentPage
    {
        public ThirdPage()
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Hello ContentPage" }
                }
            };
        }
    }
}

