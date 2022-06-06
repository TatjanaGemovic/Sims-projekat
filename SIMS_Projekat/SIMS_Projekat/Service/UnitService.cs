using SIMS_Projekat.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.Service
{
    public class UnitService
    {
        private static string EXCHANGE_EQ_CSV = @".\..\..\..\Resources\exchangeEquipmentRequest.txt";
        private static string EQUIPMENT_CSV = @".\..\..\..\Resources\equipment.txt";
        private static string ROOM_EQUIPMENT_CSV = @".\..\..\..\Resources\roomEquipmentDTO.txt";

        public ExchangeEquipmentRequestRepository _exchangeEquipmentRequestRepository { get; private set; }
        public EquipmentRepository _equipmentRepository { get; private set; }
        public RoomEquipmentDTORepository _roomEquipmentDTORepository { get; private set; }

        public UnitService() 
        {
            _exchangeEquipmentRequestRepository = new ExchangeEquipmentRequestRepository(EXCHANGE_EQ_CSV);
            _equipmentRepository = new EquipmentRepository(EQUIPMENT_CSV);
            _roomEquipmentDTORepository = new RoomEquipmentDTORepository(ROOM_EQUIPMENT_CSV);
        }

       
    }
}
