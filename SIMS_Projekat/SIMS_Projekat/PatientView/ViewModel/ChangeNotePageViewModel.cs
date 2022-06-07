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
    public class ChangeNotePageViewModel : BindableBase
    {
        Frame mainFrame;
        int fromWhere;
        public Injector Inject { get; set; }
        public MyICommand ChangeCommand { get; set; }
        public MyICommand BackCommand { get; set; }

        private NoteViewModel noteViewModel;
        public NoteViewModel Note
        {
            get { return noteViewModel; }
            set
            {
                if (noteViewModel != value)
                {
                    noteViewModel = value;
                    OnPropertyChanged("Note");

                }
            }
        }
        public ChangeNotePageViewModel(Frame frame, NoteViewModel vmNote, int fromWhere)
        {
            mainFrame = frame;
            Note = vmNote;
            this.fromWhere = fromWhere;
            Inject = new Injector();

            BackCommand = new MyICommand(OnBack);
            ChangeCommand = new MyICommand(OnChange, CanChange);

            Title = Note.Title;
            Content = Note.Content;
        }

        private void OnChange()
        {
            Note.Content = Content;
            Note.Title = Title;

            Note newNote = Inject.NotesConverter.ConvertViewModelToModel(Note, App.noteController.GetNoteByID(Convert.ToInt32(Note.NoteID)).patient);
            App.noteController.SetNote(newNote);

            if(fromWhere == 0)
            {
                ViewNotePage viewNotePage = new ViewNotePage(mainFrame, Note);
                mainFrame.NavigationService.Navigate(viewNotePage);
                return;
            }
            else if (fromWhere == 1)
            {
                ReportViewModel reportViewModel = getReportModel();
                ViewReportPage viewReportPage = new ViewReportPage(mainFrame, reportViewModel);
                mainFrame.NavigationService.Navigate(viewReportPage);
                return;
            }
            else
            {
                TherapyViewModel therapyViewModel = getTherapyModel();
                ViewTherapyPage viewTherapyPage = new ViewTherapyPage(mainFrame, therapyViewModel);
                mainFrame.NavigationService.Navigate(viewTherapyPage);
                return;
            }

            

        }
        private bool CanChange()
        {
            if (Title == "" || Content == "")
                return false;
            return true;
        }
        private ReportViewModel getReportModel()
        {
            FinishedAppointment appointment = App.finishedAppointmentController.GetAppointmentByNoteID(Convert.ToInt32(Note.NoteID));
            return Inject.ReportsConverter.ConvertModelToViewModel(appointment);
        }

        private TherapyViewModel getTherapyModel()
        {
            Receipt receipt = App.receiptController.GetReceiptByNoteID(Convert.ToInt32(Note.NoteID));
            return Inject.TherapyConverter.ConvertModelToViewModel(receipt);
        }
        private void OnBack()
        {
            mainFrame.NavigationService.GoBack();
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
        private string title;
        public string Title
        {
            get { return title; }
            set
            {
                if (title != value)
                {
                    title = value;
                    OnPropertyChanged("Title");
                    ChangeCommand.RaiseCanExecuteChanged();

                }
            }
        }
    }
}
