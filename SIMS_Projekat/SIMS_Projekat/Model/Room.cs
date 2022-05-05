using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SIMS_Projekat.Model
{
    public class Room : Serialization.Serializable, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string roomID;
        private int floor;
        private int roomNumber;
        private RoomType roomType;
        private Boolean available;

        private List<Equipment> equipment;
        private List<int> equipmentQuantity;

        public Room() 
        {
            equipment = new List<Equipment>();
            equipmentQuantity = new List<int>();
        }

        public string RoomID
        {

            get { return roomID; }
            set
            {
                roomID = value;
                OnPropertyChanged(nameof(RoomID));
            }
        }

        public int Floor
        {

            get { return floor; }
            set
            {
                floor = value;
                OnPropertyChanged(nameof(Floor));
            }
        }

        public int RoomNumber
        {

            get { return roomNumber; }
            set
            {
                roomNumber = value;
                OnPropertyChanged(nameof(RoomNumber));
            }
        }

        public Boolean Available
        {

            get { return available; }
            set
            {
                available = value;
                OnPropertyChanged(nameof(Available));
            }
        }

        public RoomType pRoomType
        {

            get { return roomType; }
            set
            {
                roomType = value;
                OnPropertyChanged(nameof(pRoomType));
            }
        }

        public List<Equipment> pEquipment
        {

            get { return equipment; }
            set
            {
                equipment = value;
                OnPropertyChanged(nameof(pEquipment));
            }
        }

        public List<int> pEquipmentQuantity
        {

            get { return equipmentQuantity; }
            set
            {
                equipmentQuantity = value;
                OnPropertyChanged(nameof(pEquipmentQuantity));
            }
        }

        public void fromCSV(string[] values)
        {
            roomID = values[0];
            RoomNumber = int.Parse(values[1]);
            Floor = int.Parse(values[2]);
            roomType = (RoomType)int.Parse(values[3]);
            if (values[4].Equals("True")) { Available = true; } else { Available = false; }

        }

        public string[] toCSV()
        {
            string[] values = {
                roomID,
                RoomNumber.ToString(),
                Floor.ToString(),
                ((int)roomType).ToString(),
                Available.ToString()
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