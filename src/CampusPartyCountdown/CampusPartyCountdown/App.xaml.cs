using CampusPartyCountdown.Views;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace CampusPartyCountdown
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new CustomNavigationPage(new MyCampusPartyCountdownView());
        }

        protected override void OnStart()
        {
            base.OnStart();

            AppCenter.Start("android={9856b4a3-146a-42c4-9abf-b2148fdf198d};" +
                            "ios={Your iOS App secret here}",
                            typeof(Analytics), typeof(Crashes));
        }
    }
}
