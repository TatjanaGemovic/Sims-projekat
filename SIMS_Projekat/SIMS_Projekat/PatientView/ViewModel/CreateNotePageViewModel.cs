﻿using SIMS_Projekat.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SIMS_Projekat.PatientView.ViewModel
{
    public class CreateNotePageViewModel : BindableBase
    {
        Frame mainFrame;
        bool fromReport;
        ReportViewModel vmReport;
        Patient patient;
        public Injector Inject { get; set; }
        public MyICommand GoBackCommand { get; set; }
        public MyICommand CreateCommand { get; set; }

        private NoteViewModel newNoteViewModel = new NoteViewModel();
        public NoteViewModel NewNoteViewModel
        {
            get { return newNoteViewModel; }
            set
            {
                if (newNoteViewModel != value)
                {
                    newNoteViewModel = value;
                    OnPropertyChanged("NewNoteViewModel");
                    CreateCommand.RaiseCanExecuteChanged();

                }
            }
        }


        //public Test TestVM
        //{
        //    get { return testVM; }
        //    set
        //    {
        //        Test oldValue = testVM;
        //        if (SetProperty(ref testVM, value))
        //        {
        //            if (oldValue != null)
        //            {
        //                oldValue.PropertyChanged -= TestPropertyChanged;
        //            }

        //            if (testVM != null)
        //            {
        //                testVM.PropertyChanged += TestPropertyChanged;
        //            }
        //        }
        //    }
        //}

        //void TestPropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    // filter if necessary
        //    if (e.PropertyName == "...")
        //    {
        //        StartTestCommand.RaiseCanExecuteChanged();
        //    }
        //}


        private ObservableCollection<NoteViewModel> notes;
        public ObservableCollection<NoteViewModel> Notes
        {
            get { return notes; }
            set
            {
                if (notes != value)
                {
                    notes = value;
                    OnPropertyChanged("Notes");

                }
            }
        }
        public CreateNotePageViewModel(Frame f, bool fromRep, Patient p, ReportViewModel vmReport)
        {
            mainFrame = f;
            Inject = new Injector();
            fromReport = fromRep;
            patient = p;
            this.vmReport = vmReport;
            GoBackCommand = new MyICommand(OnBack);
            CreateCommand = new MyICommand(OnCreate, CanCreate);
        }

        private void OnBack()
        {      
            mainFrame.NavigationService.GoBack();          
        }

        private void OnCreate()
        {
            NewNoteViewModel.Title = Title;
            NewNoteViewModel.Content = Content;
            Note newNote = Inject.NotesConverter.ConvertViewModelToModel(NewNoteViewModel, patient);
            newNote = App.noteController.AddNote(newNote);
            
            if (fromReport)
            {
                vmReport.NoteID = newNote.noteID.ToString();
                App.finishedAppointmentController.AddNoteToAppointment(vmReport.FinishedAppointmentID, Convert.ToInt32(vmReport.NoteID));
                ViewReportPage viewReportPage = new ViewReportPage(mainFrame, vmReport);
                mainFrame.NavigationService.Navigate(viewReportPage);
                return;
            }

            NotesPage notesPage = new NotesPage(mainFrame, patient);
            mainFrame.NavigationService.Navigate(notesPage);
        }

        private bool CanCreate()
        {
            if (Title == "" || Content == "")
                return false;
            return true;
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
                    CreateCommand.RaiseCanExecuteChanged();

                }
            }
        }
    }
}
