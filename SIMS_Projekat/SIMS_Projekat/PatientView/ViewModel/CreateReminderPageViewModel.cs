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
    public class CreateReminderPageViewModel : BindableBase
    {
        Frame mainFrame;
        Patient patient;
        public Injector Inject { get; set; }
        public MyICommand GoBackCommand { get; set; }
        public MyICommand CreateCommand { get; set; }

        private ReminderViewModel newReminderViewModel = new ReminderViewModel();
        public ReminderViewModel NewReminderViewModel
        {
            get { return newReminderViewModel; }
            set
            {
                if (newReminderViewModel != value)
                {
                    newReminderViewModel = value;
                    OnPropertyChanged("NewReminderViewModel");
                    CreateCommand.RaiseCanExecuteChanged();

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
        private ObservableCollection<String> possibleTimes;
        public ObservableCollection<String> PossibleTimes
        {
            get { return possibleTimes; }
            set
            {
                if (possibleTimes != value)
                {
                    possibleTimes = value;
                    OnPropertyChanged("PossibleTimes");

                }
            }
        }

        private ObservableCollection<String> isRepeatable = new ObservableCollection<String>();
        public ObservableCollection<String> IsRepeatable
        {
            get { return isRepeatable; }
            set
            {
                if (isRepeatable != value)
                {
                    isRepeatable = value;
                    OnPropertyChanged("IsRepeatable");

                }
            }
        }
        public CreateReminderPageViewModel(Frame f, Patient p)
        {
            mainFrame = f;
            Inject = new Injector();  
            patient = p;
            
            PossibleTimes = new ObservableCollection<String>(App.appointmentController.CreateAppointmentTime());
            CreateListForRepeat();    


            GoBackCommand = new MyICommand(OnBack);
            CreateCommand = new MyICommand(OnCreate, CanCreate);
        }

        private void OnBack()
        {
            mainFrame.NavigationService.GoBack();
        }

        private void OnCreate()
        {
            string[] splitDate = Date.Split(" ");
            NewReminderViewModel.Date = splitDate[0];
            NewReminderViewModel.Content = Content;
            NewReminderViewModel.Time = Time;
            NewReminderViewModel.IsRepeatable = CanRepeat;
            Reminder newReminder = Inject.RemindersConverter.ConvertViewModelToModel(NewReminderViewModel, patient);
            newReminder = App.reminderController.AddReminder(newReminder);

            RemindersPage remindersPage = new RemindersPage(mainFrame, patient);
            mainFrame.NavigationService.Navigate(remindersPage);
        }

        private bool CanCreate()
        {
            if (CanRepeat == null || Content == "" || Time == null || Date == null)
                return false;
            return true;
        }
        private void CreateListForRepeat()
        {
            IsRepeatable.Add("Nikada");
            IsRepeatable.Add("Svaki dan");
            IsRepeatable.Add("Nedeljno");
            IsRepeatable.Add("Mesečno");
        }

        
        private string content;
        public string Content
        {
            get { return content; }
            set
            {
                if (content != value)
                {
                    content = value;
                    OnPropertyChanged("Content");
                    CreateCommand.RaiseCanExecuteChanged();

                }
            }
        }
        private string date;
        public string Date
        {
            get { return date; }
            set
            {
                if (date != value)
                {
                    date = value;
                    OnPropertyChanged("Date");
                    CreateCommand.RaiseCanExecuteChanged();

                }
            }
        }

        private string time;
        public string Time
        {
            get { return time; }
            set
            {
                if (time != value)
                {
                    time = value;
                    OnPropertyChanged("Time");
                    CreateCommand.RaiseCanExecuteChanged();

                }
            }
        }

        private string canRepeat;
        public string CanRepeat
        {
            get { return canRepeat; }
            set
            {
                if (canRepeat != value)
                {
                    canRepeat = value;
                    OnPropertyChanged("CanRepeat");
                    CreateCommand.RaiseCanExecuteChanged();

                }
            }
        }


    }
}

