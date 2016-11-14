using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;

namespace PrismUnityApp2.ViewModels
{


    public class HovedsideViewModel : BindableBase, INotifyPropertyChanged, INavigationAware
    {
        MobileService.VaksServiceClient ws = new MobileService.VaksServiceClient();
        INavigationService _navigationService;
        public event PropertyChangedEventHandler PropertyChanged;


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
        private string _longitude;

        public string longitude
        {
            get { return _longitude; }
            set { _longitude = value; }
        }




        public HovedsideViewModel(INavigationService navigationService)
        {

            SetRowData(); // initialize the grid

            MinimumSelectedDate = DateTime.Now;  // this one will be revmoved later. its fine that they can go back and watch but not edite it
            SelectedDate = DateTime.Now.AddDays(1);

            _navigationService = navigationService;
            Navigatetest = new DelegateCommand(Navigate);
            NavigateToBestilling = new DelegateCommand(_NavigateToBestilling);
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

        public async void OnNavigatedTo(NavigationParameters parameters)
        {
            Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
            {
                latitude = parameters["lat"].ToString();  // if the phone inst near latitude "55,7070631" and longitude "12,4235393" then remove the part where it gets data from parameters
                longitude = parameters["lon"].ToString();  // and use the two lines under this
                                                           //latitude = "55,7070631";
                                                           //longitude = "12,4235393";
                var ggg = await CallService(new MobileService.Toemning { Latitude = latitude, Longitude = longitude, date = new DateTime(2016, 11, 1) });  // the date is set to a day where there is data
                //var justatest = 0;
                ToemmeListe.Clear();
                foreach (var toemming in ggg)
                {

                    ToemningData _ToemningData = new ToemningData();
                    _ToemningData.Fraktion = toemming.Fraktion;
                    _ToemningData.ToemmeID = toemming.ToemmeId;
                    _ToemningData.Type = toemming.Type;
                    _ToemningData.Vaegt = toemming.Weight;
                    ToemmeListe.Add(_ToemningData);
                }

            });

        }

        MobileService.Toemning ToWCFServiceTodoItem(MobileService.Toemning item)
        {
            return new MobileService.Toemning
            {
                Fraktion = item.Fraktion,
                ToemmeId = item.ToemmeId,
                Type = item.Type,
                date = item.date,
                Latitude = item.Latitude,
                Longitude = item.Longitude,
                Weight = item.Weight
            };
        }


        public async Task<ObservableCollection<MobileService.Toemning>> CallService(MobileService.Toemning item)
        {
            var todoItem = ToWCFServiceTodoItem(item);
            MobileService.VaksServiceClient client = new MobileService.VaksServiceClient(new BasicHttpBinding(), new EndpointAddress("http://vf-kssweb2/Vaks2Svc/VaksService.svc?singleWsdl"));
            var t = Task<ObservableCollection<MobileService.Toemning>>.Factory.FromAsync(
                    ((MobileService.IVaksService)client.InnerChannel).BeginGetToemmeDatas,
                    ((MobileService.IVaksService)client.InnerChannel).EndGetToemmeDatas,
                     todoItem, TaskCreationOptions.None);
            return await t;
        }

        public void SetRowData()
        {

            try
            {
                toemmeListe = new ObservableCollection<ToemningData>();
            }
            catch (Exception ex)
            {

                throw;
            }

            ToemningData _ToemningData = new ToemningData();
            _ToemningData.Fraktion = "Igen Data";
            _ToemningData.ToemmeID = -1;
            _ToemningData.Type = "Igen Data";
            _ToemningData.Vaegt = "Igen Data";
            ToemmeListe.Add(_ToemningData);
        }


        private ObservableCollection<ToemningData> toemmeListe;

        public ObservableCollection<ToemningData> ToemmeListe
        {
            get { return toemmeListe; }
            set { this.toemmeListe = value; }
        }




    }
}
