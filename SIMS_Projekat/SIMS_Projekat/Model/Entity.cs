using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SIMS_Projekat.Model
{
    public class Entity : Serialization.Serializable, INotifyPropertyChanged
    {
        private string id;
        public event PropertyChangedEventHandler PropertyChanged;

        public string ID
        {

            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged(nameof(ID));
            }
        }

        public virtual string[] toCSV()
        {
            throw new NotImplementedException();
        }

        public virtual void fromCSV(string[] values)
        {
            throw new NotImplementedException();
        }

        public void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
