using CampusPartyCountdown.ViewModels;
using CampusPartyCountdown.ViewModels.Base;
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

            MessagingCenter.Subscribe<MyCampusPartyCountdownViewModel>(this, "Restart", (sender) =>
            {
                DisplayAlert("Já está com saudades da CPBR?", "Só ano que vem", "OK");
            }
           );

        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();
            var vm = BindingContext as BaseViewModel;
            await vm?.UnloadAsync();
        }
    }
}