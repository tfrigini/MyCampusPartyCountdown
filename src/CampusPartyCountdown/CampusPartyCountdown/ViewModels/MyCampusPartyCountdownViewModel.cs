using System;
using System.Threading.Tasks;
using System.Windows.Input;
using CampusPartyCountdown.Models;
using CampusPartyCountdown.ViewModels.Base;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Xamarin.Forms;

namespace CampusPartyCountdown.ViewModels
{
    public class MyCampusPartyCountdownViewModel : BaseViewModel
    {
        private CampusParty _campusParty;
        private Countdown _countdown;
        private int _days;
        private int _hours;
        private int _minutes;
        private double _progress;

        public MyCampusPartyCountdownViewModel()
        {
            _countdown = new Countdown();
        }

        public CampusParty MyCampusParty
        {
            get => _campusParty;
            set => SetProperty(ref _campusParty, value);
        }

        public int Days
        {
            get => _days;
            set => SetProperty(ref _days, value);
        }

        public int Hours
        {
            get => _hours;
            set => SetProperty(ref _hours, value);
        }

        public int Minutes
        {
            get => _minutes;
            set => SetProperty(ref _minutes, value);
        }

        public double Progress
        {
            get => _progress;
            set => SetProperty(ref _progress, value);
        }

        public ICommand RestartCommand => new Command(Restart);

        public ICommand LinkCommand => new Command<string>((url) =>
        {
            Device.OpenUri(new Uri(url));
        });

        public override Task LoadAsync()
        {
            LoadCampusParty();

            _countdown.EndDate = MyCampusParty.Date;
            _countdown.Start();

            _countdown.Ticked += OnCountdownTicked;
            _countdown.Completed += OnCountdownCompleted;

            return base.LoadAsync();
        }

        public override Task UnloadAsync()
        {
            _countdown.Ticked -= OnCountdownTicked;
            _countdown.Completed -= OnCountdownCompleted;

            return base.UnloadAsync();
        }

        void OnCountdownTicked()
        {
            Days = _countdown.RemainTime.Days;
            Hours = _countdown.RemainTime.Hours;
            Minutes = _countdown.RemainTime.Minutes;

            var totalSeconds = (MyCampusParty.Date - MyCampusParty.Creation).TotalSeconds;
            var remainSeconds = _countdown.RemainTime.TotalSeconds;

            try
            {
                Progress = remainSeconds / totalSeconds;
            }
            catch (Exception exception)
            {
                Crashes.TrackError(exception);
            }
        }

        void OnCountdownCompleted()
        {
            Days = 0;
            Hours = 0;
            Minutes = 0;

            Progress = 0;
        }

        void LoadCampusParty()
        {
            var endCampusParty = new DateTime(2019, 2, 17);

            var campusParty = new CampusParty()
            {
                Picture = "gandalf",
                Date = endCampusParty,
                Creation = DateTime.Now.AddHours(-8)
            };

            MyCampusParty = campusParty;
            Analytics.TrackEvent("Load countdown timer info");
        }

        void Restart() => MessagingCenter.Send(this, "Restart");
    }
}