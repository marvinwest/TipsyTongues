using Xamarin.Forms;

[assembly: ExportFont("FjallaOne-Regular.ttf", Alias = "fjallaOne")]
namespace ClientApp
{
    
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

    }

}
