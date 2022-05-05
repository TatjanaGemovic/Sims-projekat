using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.Model
{
    public class Equipment : Serialization.Serializable, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string equipmentID;
        private string equipmentName;
        private int quantity;
        private EquipmentType equipmentType;

        private ObservableCollection<Room> rooms;
        private List<int> equipmentQuantityInRooms;

        public Equipment()
        {
            rooms = new ObservableCollection<Room>();
            equipmentQuantityInRooms = new List<int>();
        }

        public string EquipmentID {

            get { return equipmentID; }
            set
            {
                equipmentID = value;
                OnPropertyChanged(nameof(EquipmentID));
            }
        }

        public string EquipmentName
        {

            get { return equipmentName; }
            set
            {
                equipmentName = value;
                OnPropertyChanged(nameof(EquipmentName));
            }
        }
        public EquipmentType pEquipmentType
        {

            get { return equipmentType; }
            set
            {
                equipmentType = value;
                OnPropertyChanged(nameof(pEquipmentType));
            }
        }

        public int Quantity
        {

            get { return quantity; }
            set
            {
                quantity = value;
                OnPropertyChanged(nameof(Quantity));
            }
        }

        public ObservableCollection<Room> Rooms
        {

            get { return rooms; }
            set
            {
                rooms = value;
                OnPropertyChanged(nameof(Rooms));
            }
        }

        public List<int> ppEquipmentQuantity
        {

            get { return equipmentQuantityInRooms; }
            set
            {
                equipmentQuantityInRooms = value;
                OnPropertyChanged(nameof(ppEquipmentQuantity));
            }
        }

        public void fromCSV(string[] values)
        {
            equipmentID = values[0];
            equipmentName = values[1];
            quantity = int.Parse(values[2]);
            equipmentType = (EquipmentType)int.Parse(values[3]);
        }

        public string[] toCSV()
        {
            string[] values = {
                equipmentID,
                equipmentName,
                quantity.ToString(),
                ((int)equipmentType).ToString()
            };

            return values;
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

