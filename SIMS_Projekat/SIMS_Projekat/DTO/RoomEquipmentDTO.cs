using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.DTO
{
    public class RoomEquipmentDTO : Serialization.Serializable, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string dtoID { get; set; }
        private string roomID { get; set; }
        private string equipmentID { get; set; }
        private int quantity { get; set; }

        public string RoomEquipmentDTOID
        {

            get { return dtoID; }
            set
            {
                dtoID = value;
                OnPropertyChanged(nameof(RoomEquipmentDTOID));
            }
        }
        public string RoomIDDTO
        {

            get { return roomID; }
            set
            {
                roomID = value;
                OnPropertyChanged(nameof(RoomIDDTO));
            }
        }

        public string EquipmentIDDTO
        {

            get { return equipmentID; }
            set
            {
                equipmentID = value;
                OnPropertyChanged(nameof(EquipmentIDDTO));
            }
        }

        public int QuantityDTO
        {

            get { return quantity; }
            set
            {
                quantity = value;
                OnPropertyChanged(nameof(QuantityDTO));
            }
        }
        public void fromCSV(string[] values)
        {
            roomID = values[0];
            equipmentID = values[1];
            quantity = int.Parse(values[2]);
            dtoID = values[3];
        }

        public string[] toCSV()
        {
            string[] values = {
                roomID,
                equipmentID,
                quantity.ToString(),
                dtoID
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
