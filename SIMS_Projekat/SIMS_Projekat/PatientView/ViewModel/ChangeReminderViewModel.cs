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
    public class ChangeReminderViewModel : BindableBase
    {
        Frame mainFrame;
        bool fromReport;
        public Injector Inject { get; set; }
        public MyICommand ChangeCommand { get; set; }
        public MyICommand BackCommand { get; set; }

        private ReminderViewModel reminderViewModel;
        public ReminderViewModel Reminder
        {
            get { return reminderViewModel; }
            set
            {
                if (reminderViewModel != value)
                {
                    reminderViewModel = value;
                    OnPropertyChanged("Reminder");

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
        public ChangeReminderViewModel(Frame frame, ReminderViewModel vmReminder)
        {
            mainFrame = frame;
            Reminder = vmReminder;
            //fromReport = fromRep;
            Inject = new Injector();

            PossibleTimes = new ObservableCollection<String>(App.appointmentController.CreateAppointmentTime());
            CreateListForRepeat();

            BackCommand = new MyICommand(OnBack);
            ChangeCommand = new MyICommand(OnChange, CanChange);

            FillFields();
        }

        private void OnChange()
        {
            Reminder.Content = Content;
            Reminder.Time = Time;
            Reminder.IsRepeatable = CanRepeat;
            DateTime date = DateTime.Parse(Date);
            Reminder.Date = date.Date.ToString("dd.MM.yyyy.");

            Reminder newReminder = Inject.RemindersConverter.ConvertViewModelToModel(Reminder, App.reminderController.GetReminderByID(Convert.ToInt32(Reminder.ReminderID)).patient);
            App.reminderController.SetReminder(newReminder);

            //if (!fromReport)
            //{
            //    ViewNotePage viewNotePage = new ViewNotePage(mainFrame, Note);
            //    mainFrame.NavigationService.Navigate(viewNotePage);
            //    return;
            //}

            //ReportViewModel reportViewModel = getReportModel();
            ViewReminderPage viewReminderPage = new ViewReminderPage(mainFrame, Reminder);
            mainFrame.NavigationService.Navigate(viewReminderPage);

        }
        private bool CanChange()
        {
            if (CanRepeat == null || Content == "" || Time == null || Date == null)
                return false;
            return true;
        }
        //private ReportViewModel getReportModel()
        //{
        //    FinishedAppointment appointment = App.finishedAppointmentController.GetAppointmentByNoteID(Convert.ToInt32(Note.NoteID));
        //    return Inject.ReportsConverter.ConvertModelToViewModel(appointment);
        //}
        private void OnBack()
        {
            mainFrame.NavigationService.GoBack();
        }      
        private void FillFields()
        {
            Content = Reminder.Content;
            Time = Reminder.Time;
            CanRepeat = Reminder.IsRepeatable;
            ParseDate();
        }

        private void ParseDate()
        {
            string[] dateParts = Reminder.Date.Split(".");
            Date = dateParts[1] + "/" + dateParts[0] + "/" + dateParts[2];
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
                    ChangeCommand.RaiseCanExecuteChanged();

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
                    ChangeCommand.RaiseCanExecuteChanged();

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
                    ChangeCommand.RaiseCanExecuteChanged();

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
                    ChangeCommand.RaiseCanExecuteChanged();

                }
            }
        }
    }
}
