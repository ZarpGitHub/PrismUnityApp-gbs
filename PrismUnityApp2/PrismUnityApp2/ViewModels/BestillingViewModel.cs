using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace PrismUnityApp2.ViewModels
{
    public class BestillingViewModel : BindableBase, INavigationAware, INotifyPropertyChanged
    {

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        //public DelegateCommand DateChanged { get; private set; }
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
                            new PropertyChangedEventArgs("SelectedDate")); // this only gets hit when the user clicks ok but not sure if its the same on iphne
                        
                        // get the data for type flyting and Fraktion here
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



        public BestillingViewModel()
        {
            MinimumSelectedDate = DateTime.Now;
            SelectedDate = DateTime.Today.AddDays(1);
            
            //DateChanged = new DelegateCommand(DateSelected);
        }

        //private void DateSelected()
        //{

        //}

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("title"))
                Title = (string)parameters["title"] + " and Prism";
        }

    }
}
