using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace PrismUnityApp2.ViewModels
{
    public class HovedsideViewModel : BindableBase, INotifyPropertyChanged , INavigationAware
    {
        INavigationService _navigationService;
        public event PropertyChangedEventHandler PropertyChanged;
        MobileService.VaksServiceClient ws = new MobileService.VaksServiceClient();

        private DateTime selectedDate;
        public DateTime SelectedDate
        {
            get { return selectedDate; }
            set
            {
                if (selectedDate != value)
                {
                    selectedDate = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("SelectedDate"));
                    }
                }
            }
        }

        private DateTime minimumSelectedDate;
        public DateTime MinimumSelectedDate
        {
            get { return minimumSelectedDate; }
            set
            {
                if (minimumSelectedDate != value)
                {
                    minimumSelectedDate = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("MinimumSelectedDate"));
                    }
                }
            }
        }

        public DelegateCommand Navigatetest { get; private set; }
        public DelegateCommand NavigateToBestilling { get; private set; }

        public string latitude { get; set; }
        public string longitude { get; set; }

        public HovedsideViewModel(INavigationService navigationService)
        {
            ws.GetToemmeDatasCompleted += Ws_GetToemmeDatasCompleted;

            MinimumSelectedDate = DateTime.Now;
            SelectedDate = DateTime.Now.AddDays(1);

            _navigationService = navigationService;
            Navigatetest = new DelegateCommand(Navigate);
            NavigateToBestilling = new DelegateCommand(_NavigateToBestilling);
        }

        private void Ws_GetToemmeDatasCompleted(object sender, MobileService.GetToemmeDatasCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void _NavigateToBestilling()
        {
            _navigationService.NavigateAsync("Bestilling");
        }

        private void Navigate()
        {
           _navigationService.NavigateAsync("Scanner");
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            latitude = parameters["lat"].ToString();
            longitude = parameters["lon"].ToString();
            ws.GetToemmeDatasAsync(latitude, longitude, DateTime.Today);
        }
    }
}
