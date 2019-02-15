using System.Threading.Tasks;
using CampusPartyCountdown.ViewModels;
using CampusPartyCountdown.ViewModels.Base;
using Microsoft.AppCenter.Analytics;
using Xamarin.Forms;

namespace CampusPartyCountdown.Views
{
    public partial class MyCampusPartyCountdownView : ContentPage
    {
        public MyCampusPartyCountdownView()
        {
            InitializeComponent();

            BindingContext = new MyCampusPartyCountdownViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var vm = BindingContext as BaseViewModel;
            await vm?.LoadAsync();

            MessagingCenter.Subscribe<MyCampusPartyCountdownViewModel>(this, "Restart", async (sender) =>
            {
                var answer = await DisplayAlert("Já está com saudades da CPBR, né!",
                                                "Não se preocupe, ano que vem tem mais aqui em SP. Você pretende vir?",
                                                "Sim", "Não");

                if (answer)
                    Analytics.TrackEvent("Return to CPBR SP");
                else
                    Analytics.TrackEvent("Not return to CPBR SP");
            }
           );
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<MyCampusPartyCountdownViewModel>(this, "Restart");

            var vm = BindingContext as BaseViewModel;
            await vm?.UnloadAsync();
        }
    }
}