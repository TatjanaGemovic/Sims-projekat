using SIMS_Projekat.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SIMS_Projekat.PatientView.ViewModel
{
    public class SingleReminderViewModel: BindableBase
    {
        Frame mainFrame;
        private ReminderViewModel reminder;
        private Reminder reminderModel;
        public Injector Inject { get; set; }
        public MyICommand BackCommand { get; set; }
        public MyICommand ChangeCommand { get; set; }
        public MyICommand DeleteCommand { get; set; }
        public ReminderViewModel Reminder
        {
            get { return reminder; }
            set
            {
                if (reminder != value)
                {
                    reminder = value;
                    OnPropertyChanged("Reminder");

                }
            }
        }
        public SingleReminderViewModel(Frame frame, ReminderViewModel vmReminder)
        {
            mainFrame = frame;
            Reminder = vmReminder;
            Inject = new Injector();
            reminderModel = App.reminderController.GetReminderByID(Convert.ToInt32(Reminder.ReminderID));

            BackCommand = new MyICommand(OnBack);
            ChangeCommand = new MyICommand(OnChange);
            DeleteCommand = new MyICommand(OnDelete);
        }

        private void OnChange()
        {
            ChangeReminderPage changeReminderPage = new ChangeReminderPage(mainFrame, Reminder);
            mainFrame.NavigationService.Navigate(changeReminderPage);
        }

        private void OnDelete()
        {
            if (MessageBox.Show("Jeste li sigurni da zelite da obrišete podsetnik?",
            "Brisanje beleške", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                App.reminderController.DeleteReminder(reminderModel);

                RemindersPage remindersPage = new RemindersPage(mainFrame, reminderModel.patient);
                mainFrame.NavigationService.Navigate(remindersPage);
            }
        }
        private void OnBack()
        {
            RemindersPage remindersPage = new RemindersPage(mainFrame, reminderModel.patient);
            mainFrame.NavigationService.Navigate(remindersPage);
        }
    }
}
