using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace PrismUnityApp2.ViewModels
{


    public class HovedsideViewModel : BindableBase, INotifyPropertyChanged //, INavigationAware
    {
        MobileService.VaksServiceClient ws = new MobileService.VaksServiceClient();
        //INavigationService _navigationService;
       // public event PropertyChangedEventHandler PropertyChanged;


        //private DateTime selectedDate;
        //public DateTime SelectedDate
        //{
        //    get { return selectedDate; }
        //    set
        //    {
        //        if (selectedDate != value)
        //        {
        //            selectedDate = value;

        //            if (PropertyChanged != null)
        //            {
        //                PropertyChanged(this,
        //                    new PropertyChangedEventArgs("SelectedDate"));
        //            }
        //        }
        //    }
        //}

        //private DateTime minimumSelectedDate;
        //public DateTime MinimumSelectedDate
        //{
        //    get { return minimumSelectedDate; }
        //    set
        //    {
        //        if (minimumSelectedDate != value)
        //        {
        //            minimumSelectedDate = value;

        //            if (PropertyChanged != null)
        //            {
        //                PropertyChanged(this,
        //                    new PropertyChangedEventArgs("MinimumSelectedDate"));
        //            }
        //        }
        //    }
        //}

        //public DelegateCommand Navigatetest { get; private set; }
        //public DelegateCommand NavigateToBestilling { get; private set; }

        //public string latitude { get; set; }
        //public string longitude { get; set; }

        public HovedsideViewModel()
        {
            toemmeListe = new ObservableCollection<ToemningData>();
            SetRowData();
        }


        //public HovedsideViewModel(INavigationService navigationService)
        //{
        //    toemmeListe = new ObservableCollection<ToemningData>();
        //    SetRowData();

        //    MinimumSelectedDate = DateTime.Now;
        //    SelectedDate = DateTime.Now.AddDays(1);

        //    _navigationService = navigationService;
        //    Navigatetest = new DelegateCommand(Navigate);
        //    NavigateToBestilling = new DelegateCommand(_NavigateToBestilling); 
        //}

       

        //private void _NavigateToBestilling()
        //{
        //    _navigationService.NavigateAsync("Bestilling");
        //}

        //private void Navigate()
        //{
        //   _navigationService.NavigateAsync("Scanner");
        //}

        //public void OnNavigatedFrom(NavigationParameters parameters)
        //{
            
        //}

        //public void OnNavigatedTo(NavigationParameters parameters)
        //{
        //    //latitude = parameters["lat"].ToString();
        //    //longitude = parameters["lon"].ToString();
        //}

        public void SetRowData()
        {
            ws.GetToemmeDatasCompleted += Ws_GetToemmeDatasCompleted;
            ws.GetToemmeDatasAsync("55,7070679", "12,4234935",DateTime.Today.AddDays(-2));
        }


        private void Ws_GetToemmeDatasCompleted(object sender, MobileService.GetToemmeDatasCompletedEventArgs e)
        {
            //ToemmeListe.Clear();
            foreach (var toemming in e.Result)
            {
                ToemningData _ToemningData = new ToemningData();
                _ToemningData.Fraktion = toemming.Fraktion;
                _ToemningData.ToemmeID = toemming.ToemmeId;
                _ToemningData.Type = toemming.type;
                _ToemningData.Vaegt = toemming.weight;
                ToemmeListe.Add(_ToemningData);
            }
        }

        private ObservableCollection<ToemningData> toemmeListe;

        public ObservableCollection<ToemningData> ToemmeListe
        {
            get { return toemmeListe; }
            set { this.toemmeListe = value; }
        }

 


    }
}
