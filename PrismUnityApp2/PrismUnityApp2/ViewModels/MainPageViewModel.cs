using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Plugin.Geolocator;

namespace PrismUnityApp2.ViewModels
{
    public class MainPageViewModel : BindableBase, INavigationAware
    {
        INavigationService _navigationService;

        private string _title = "Loging side";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private string _username;
        public string Username
        {
            get { return _username; }
            set { SetProperty(ref _username, value); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        public DelegateCommand Navigatetest { get; private set; }

        public MainPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            Navigatetest = new DelegateCommand(Navigate);

        }

        private async void Navigate()
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;

            var position =  await locator.GetPositionAsync(10000);
            var ttt=position.Latitude.GetType();


            // Gif køres her 

            await _navigationService.NavigateAsync("Bema");
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {

        }
    }
}
