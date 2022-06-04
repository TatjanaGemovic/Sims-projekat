using SIMS_Projekat.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SIMS_Projekat.PatientView.ViewModel
{
    public class RemindersPageViewModel : BindableBase
    {
        Frame mainFrame;
        Patient patient;
        public Injector Inject { get; set; }
        public MyICommand DetailsCommand { get; set; }
        public MyICommand NewReminderCommand { get; set; }

        private ReminderViewModel reminderViewModel;
        public ReminderViewModel ReminderViewModel
        {
            get { return reminderViewModel; }
            set
            {
                if (reminderViewModel != value)
                {
                    reminderViewModel = value;
                    OnPropertyChanged("ReminderViewModel");

                }
            }
        }

        private ObservableCollection<ReminderViewModel> reminders;
        public ObservableCollection<ReminderViewModel> Reminders
        {
            get { return reminders; }
            set
            {
                if (reminders != value)
                {
                    reminders = value;
                    OnPropertyChanged("Reminders");

                }
            }
        }
        public RemindersPageViewModel(Frame f, Patient p)
        {
            mainFrame = f;
            patient = p;

            ObservableCollection<Reminder> reminders = new ObservableCollection<Reminder>(App.reminderController.GetRemindersByTypeAndPatient("Podsetnik: ", patient.ID));
            Inject = new Injector();
            Reminders = new ObservableCollection<ReminderViewModel>(Inject.RemindersConverter.ConvertCollectionToViewModel(reminders));
            DetailsCommand = new MyICommand(OnDetail);
            NewReminderCommand = new MyICommand(OnCreate);
        }

        private void OnDetail()
        {
            //ViewNotePage viewNotePage = new ViewNotePage(mainFrame, NoteViewModel);
            //mainFrame.NavigationService.Navigate(viewNotePage);
        }

        private void OnCreate()
        {
            CreateReminderPage createReminderPage = new CreateReminderPage(mainFrame, patient);
            mainFrame.NavigationService.Navigate(createReminderPage);
        }
    }
}
