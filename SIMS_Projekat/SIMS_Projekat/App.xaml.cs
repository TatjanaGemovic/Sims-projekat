﻿using SIMS_Projekat.Controller;
using SIMS_Projekat.Model;
using SIMS_Projekat.Repository;
using SIMS_Projekat.Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;


namespace SIMS_Projekat
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static ResourceDictionary ThemeDictionary => Application.Current.Resources.MergedDictionaries[0];
        private static string APPOINTMENT_FILE = @".\..\..\..\Resources\appointment.txt";
        public static AppointmentRepository appointmentRepo;
        public static AppointmentService appointmentService;
        public static AppointmentController appointmentController;

        private static string FINISHED_APPOINTMENT_FILE = @".\..\..\..\Resources\finished_appointment.txt";
        public static FinishedAppointmentRepository finishedAppointmentRepo;
        public static FinishedAppointmentService finishedAppointmentService;
        public static FinishedAppointmentController finishedAppointmentController;

        private static string NOTE_FILE = @".\..\..\..\Resources\notes.txt";
        public static NoteRepository noteRepository;
        public static NoteService noteService;
        public static NoteController noteController;

        private static string RECEIPT_FILE = @".\..\..\..\Resources\receipt.txt";
        public static ReceiptRepository receiptRepository;

        private static string PATIENTS_CSV = @".\..\..\..\Resources\patients.txt";
        private static string DOCTORS_CSV = @".\..\..\..\Resources\doctors.txt";
        public static string MEDICALRECORD_CSV = @".\..\..\..\Resources\patient_carton.txt";
        public static string ALLERGENS_CSV = @".\..\..\..\Resources\allergens.txt";
        private static string EQUIPMENT_ORDERS_CSV = @".\..\..\..\Resources\equipment_orders.txt";


        public static string THERAPY_NOTIFICATION_CSV = @".\..\..\..\Resources\therapy_notifications.txt";
        public static TherapyNotificationRepository therapyNotificationRepository;
        public static TherapyNotificationService therapyNotificationService;
        public static TherapyNotificationController therapyNotificationController;

        public static string EVALUATION_CSV = @".\..\..\..\Resources\evaluation.txt";
        public static EvaluationRepository evaluationRepository;
        public static EvaluationService evaluationService;
        public static EvaluationController evaluationController;

        public static AccountRepository accountRepository;
        public static MedicalRecordRepository medRecordRepository;
        public static AccountService accountService;
        public static AccountController accountController;

        public static AllergenRepository AllergenRepository;
        public static AllergenService AllergenService;
        public static AllergenController AllergenController;

        public static EquipmentOrderRepository EquipmentOrderRepository;
        public static EquipmentOrderService EquipmentOrderService;
        public static EquipmentOrderController EquipmentOrderController;

        private static string ROOM_CSV = @".\..\..\..\Resources\rooms.txt";
        private static string EQUIPMENT_CSV = @".\..\..\..\Resources\equipment.txt";
        private static string ROOM_EQUIPMENT_CSV = @".\..\..\..\Resources\roomEquipmentDTO.txt";
        private static string EXCHANGE_EQ_CSV = @".\..\..\..\Resources\exchangeEquipmentRequest.txt";
        private static string RENOVATION_CSV = @".\..\..\..\Resources\renovation.txt";
        private static string REQUEST_FILE = @".\..\..\..\Resources\requests.txt";
        private static string MEDICINE_CSV = @".\..\..\..\Resources\medicine.txt";
        private static string MEDICINE_COMPONENT_CSV = @".\..\..\..\Resources\medicineComponentDTO.txt";
        private static string MEDICINE_REPLACMENT_CSV = @".\..\..\..\Resources\medicineReplacmentDTO.txt";

        public static FreeDayRequestRepository freeDayRequestRepository;
        public static RoomRepository roomRepository;
        public static RoomService roomService;
        public static RoomController roomController;
        public static EquipmentRepository equipmentRepository;
        public static EquipmentService equipmentService;
        public static EquipmentController equipmentController;
        public static ExchangeEquipmentRequestRepository exchangeEquipmentRequestRepository;
        public static ExchangeEquipmentRequestService exchangeEquipmentRequestService;
        public static ExchangeEquipmentRequestController exchangeEquipmentRequestController;
        public static RoomEquipmentDTORepository roomEquipmentDTORepository;
        public static RoomEquipmentDTOService roomEquipmentDTOService;
        public static RenovationRequestRepository renovationRequestRepository;
        public static RenovationRequestService renovationRequestService;
        public static RenovationRequestController renovationRequestController;
        public static MedicineRepository medicineRepository;
        public static MedicineComponentDTORepository medicineComponentsRepository;
        public static MedicineReplacmentDTORepository medicineReplacmentRepository;
        public static MedicineService medicineService;
        public static MedicineController medicineController;


        public App()
        {
            InitializeComponent();
            roomRepository = new RoomRepository(ROOM_CSV);
            roomService = new RoomService(roomRepository);
            roomController = new RoomController(roomService);
            equipmentRepository = new EquipmentRepository(EQUIPMENT_CSV);
            equipmentService = new EquipmentService(equipmentRepository);
            equipmentController = new EquipmentController(equipmentService);
            roomEquipmentDTORepository = new RoomEquipmentDTORepository(ROOM_EQUIPMENT_CSV);
            exchangeEquipmentRequestRepository = new ExchangeEquipmentRequestRepository(EXCHANGE_EQ_CSV);
            roomEquipmentDTOService = new RoomEquipmentDTOService(roomEquipmentDTORepository);
            exchangeEquipmentRequestService = new ExchangeEquipmentRequestService(exchangeEquipmentRequestRepository, roomEquipmentDTORepository, equipmentRepository);
            exchangeEquipmentRequestController = new ExchangeEquipmentRequestController(equipmentService, exchangeEquipmentRequestService);
            medRecordRepository = new MedicalRecordRepository(MEDICALRECORD_CSV);    
            receiptRepository = new ReceiptRepository(RECEIPT_FILE);
            freeDayRequestRepository = new FreeDayRequestRepository(REQUEST_FILE);
            renovationRequestRepository = new RenovationRequestRepository(RENOVATION_CSV);
            renovationRequestService = new RenovationRequestService(renovationRequestRepository, roomRepository, exchangeEquipmentRequestRepository);
            renovationRequestController = new RenovationRequestController(renovationRequestService);
            medicineRepository = new MedicineRepository(MEDICINE_CSV);
            medicineComponentsRepository = new MedicineComponentDTORepository(MEDICINE_COMPONENT_CSV, medicineRepository.GetMedicine());
            medicineReplacmentRepository = new MedicineReplacmentDTORepository(MEDICINE_REPLACMENT_CSV, medicineRepository);
            medicineService = new MedicineService(medicineRepository);
            medicineController = new MedicineController(medicineService);

            appointmentRepo = new AppointmentRepository(APPOINTMENT_FILE);
            appointmentService = new AppointmentService()
            {
                appointmentRepository = appointmentRepo
            };
            appointmentController = new AppointmentController()
            {
                appointmentService = appointmentService
            };

            finishedAppointmentRepo = new FinishedAppointmentRepository(FINISHED_APPOINTMENT_FILE);
            finishedAppointmentService = new FinishedAppointmentService()
            {
                finishedAppointmentRepository = finishedAppointmentRepo
            };
            finishedAppointmentController = new FinishedAppointmentController()
            {
                finishedAppointmentService = finishedAppointmentService
            };

            noteRepository = new NoteRepository(NOTE_FILE);
            noteService = new NoteService()
            {
                noteRepository = noteRepository
            };
            noteController = new NoteController()
            {
                noteService = noteService
            };

            accountRepository = new AccountRepository(PATIENTS_CSV, DOCTORS_CSV);
            accountService = new AccountService()
            {
                AccountRepository = accountRepository
            };
            accountController = new AccountController()
            {
                AccountService = accountService
            };


            EquipmentOrderRepository = new EquipmentOrderRepository(EQUIPMENT_ORDERS_CSV);
            EquipmentOrderService = new EquipmentOrderService()
            {
                EquipmentOrderRepository = EquipmentOrderRepository
            };
            EquipmentOrderController = new EquipmentOrderController()
            {
                EquipmentOrderService = EquipmentOrderService
            };


            AllergenRepository = new AllergenRepository(ALLERGENS_CSV);
            AllergenService = new AllergenService()
            {
                AllergenRepository = AllergenRepository
            };
            AllergenController = new AllergenController()
            {
                AllergenService = AllergenService
            };

            therapyNotificationRepository = new TherapyNotificationRepository(THERAPY_NOTIFICATION_CSV);
            therapyNotificationService = new TherapyNotificationService()
            {
                therapyNotificationRepository = therapyNotificationRepository
            };
            therapyNotificationController = new TherapyNotificationController()
            {
                therapyNotificationService = therapyNotificationService
            };

            evaluationRepository = new EvaluationRepository(EVALUATION_CSV);
            evaluationService = new EvaluationService()
            {
                evaluationRepository = evaluationRepository
            };
            evaluationController = new EvaluationController()
            {
                evaluationService = evaluationService
            };

            AllergenController.Deserialize();
            medRecordRepository.Deserialize();
            accountRepository.Deserialize();
            roomController.Deserialize();
            appointmentRepo.Deserialize();
            noteRepository.Deserialize();
            finishedAppointmentRepo.Deserialize();
            
            therapyNotificationRepository.Deserialize();
            equipmentController.Deserialize();
            roomEquipmentDTOService.Deserialize(roomController.GetRooms(), equipmentController.GetEquipment());
            exchangeEquipmentRequestController.Deserialize();
            renovationRequestController.Deserialize();

            freeDayRequestRepository.Deserialize();
            medicineController.Deserialize();
            receiptRepository.Deserialize();
            medicineComponentsRepository.Deserialize();
            medicineReplacmentRepository.Deserialize();

            evaluationRepository.Deserialize();

            EquipmentOrderController.Deserialize();

            if (SIMS_Projekat.Properties.Settings.Default.CurrentTheme == "Light")
            {
                ChangeTheme(new Uri("Theme/LightTheme.xaml", UriKind.RelativeOrAbsolute));
                SIMS_Projekat.Properties.Settings.Default.CurrentTheme = "Light";
                SIMS_Projekat.Properties.Settings.Default.Save();

            }
            else
            {
                ChangeTheme(new Uri("Theme/DarkTheme.xaml", UriKind.RelativeOrAbsolute));
                SIMS_Projekat.Properties.Settings.Default.CurrentTheme = "Dark";
                SIMS_Projekat.Properties.Settings.Default.Save();
            }

        }

        private void ChangeTheme(Uri uri)
        {
            App.ThemeDictionary.MergedDictionaries.Clear();
            App.ThemeDictionary.MergedDictionaries.Add(new ResourceDictionary() { Source = uri });

        }
    }
}
