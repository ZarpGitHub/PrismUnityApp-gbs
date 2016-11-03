using System;
using System.ComponentModel;

namespace PrismUnityApp2.ViewModels
{
    public class ToemningData : INotifyPropertyChanged
    {
        public ToemningData()
        { }

        private int toemmeID { get; set; }
        public int ToemmeID
        {
            get
            {
                return toemmeID;
            }
            set
            {
                this.toemmeID = value;
                RaisePropertyChanged("ToemmeID");
            }
        }

        private string fraktion { get; set; }
        public string Fraktion
        {
            get
            {
                return fraktion;
            }
            set
            {
                this.fraktion = value;
                RaisePropertyChanged("Fraktion");
            }
        }

        private string type { get; set; }
        public string Type
        {
            get
            {
                return type;
            }
            set
            {
                this.type = value;
                RaisePropertyChanged("Type");
            }

        }

        private string vaegt { get; set; }
        public string Vaegt
        {
            get
            {
                return vaegt;
            }
            set
            {
                this.vaegt = value;
                RaisePropertyChanged("Vaegt");
            }
        }

        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(String Name)
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(Name));
        }

        #endregion
    }
}