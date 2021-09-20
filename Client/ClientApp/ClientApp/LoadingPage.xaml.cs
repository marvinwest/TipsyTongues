using Xamarin.Forms;

namespace ClientApp
{

    /**
     * LoadingPage:
     * Loading animation is shown using the Lottie-Framework (in XAML).
     * Animation in the form of a beer that is filled.
     **/
    public partial class LoadingPage : ContentPage
    {

        public LoadingPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

    }

}