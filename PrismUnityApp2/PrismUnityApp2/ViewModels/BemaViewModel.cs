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
    public class BemaViewModel : BindableBase , INotifyPropertyChanged , INavigationAware
    {
        INavigationService _navigationService;
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Message> myClassList;
        public ObservableCollection<Message> MyClassList
        {
            get { return myClassList; }
            set
            {
                if (myClassList != value)
                {
                    myClassList = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("MyClassList"));
                    }
                }
            } 
        }





        private void Ws_GetMessageDatasCompleted(object sender, MobileService.GetMessageDatasCompletedEventArgs e)
        {
            //myClassList.Clear(); 
            var ttt = e.Result;
            foreach (MobileService.Message item in e.Result)
            {
                Message _Message = new Message();
                _Message.MessageDate = item.MessageTime;
                _Message.MessageData = item.messagedata;

                MyClassList.Add(_Message);
            }
            
        }



        public DelegateCommand Navigatetest { get; private set; }

        public BemaViewModel(INavigationService navigationService)
        {
            MobileService.Service1Client ws = new MobileService.Service1Client();
            ws.GetMessageDatasCompleted += Ws_GetMessageDatasCompleted;
            ws.GetMessageDatasAsync();
            myClassList = new ObservableCollection<Message>();


            _navigationService = navigationService;
            Navigatetest = new DelegateCommand(Navigate);
        }

        private void Navigate()
        {
            _navigationService.NavigateAsync("Hovedside");
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {

        }

    }



    public class Message
    {
        public DateTime MessageDate { get; set; }
        public String MessageData { get; set; }
    }
}
