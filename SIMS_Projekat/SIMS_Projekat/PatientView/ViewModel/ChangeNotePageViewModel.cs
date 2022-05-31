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
        bool fromReport;
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
        public ChangeNotePageViewModel(Frame frame, NoteViewModel vmNote, bool fromRep)
        {
            mainFrame = frame;
            Note = vmNote;
            fromReport = fromRep;
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

            if(!fromReport)
            {
                ViewNotePage viewNotePage = new ViewNotePage(mainFrame, Note);
                mainFrame.NavigationService.Navigate(viewNotePage);
                return;
            }
         
            ReportViewModel reportViewModel = getReportModel();
            ViewReportPage viewReportPage = new ViewReportPage(mainFrame, reportViewModel);
            mainFrame.NavigationService.Navigate(viewReportPage);

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
