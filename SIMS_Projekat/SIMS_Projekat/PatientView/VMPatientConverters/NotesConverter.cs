using SIMS_Projekat.Model;
using SIMS_Projekat.PatientView.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.PatientView.VMPatientConverters
{
    public class NotesConverter
    {
        static int id;
        public NoteViewModel ConvertModelToViewModel(Note note)
        {
            NoteViewModel noteViewModel = new NoteViewModel();

            noteViewModel.Title = note.title;
            noteViewModel.Content = note.content;
            noteViewModel.Index = id;
            noteViewModel.Date = note.creationDate.Date.ToString("dd.MM.yyyy.");
            noteViewModel.Time = note.creationDate.TimeOfDay.ToString(@"hh\:mm");
            noteViewModel.NoteID = note.noteID.ToString();            
            //noteViewModel.PatientID = note.patient.ID;
            id++;
            return noteViewModel;
        }

        public ObservableCollection<NoteViewModel> ConvertCollectionToViewModel(ObservableCollection<Note> notes)
        {
            id = 1;
            ObservableCollection<NoteViewModel> vmNotes = new ObservableCollection<NoteViewModel>();
            NoteViewModel noteViewModel;
            foreach (Note n in notes)
            {
                noteViewModel = ConvertModelToViewModel(n);
                vmNotes.Add(noteViewModel);
            }
            return vmNotes;
        }

        public Note ConvertViewModelToModel(NoteViewModel noteFromView, Patient patient)
        {
            Note note = new Note()
            {
                content = noteFromView.Content,
                title = noteFromView.Title,
                creationDate = DateTime.Now,
                patient = patient,
                noteID = Convert.ToInt32(noteFromView.NoteID)
            };
            return note;
        }
        
        public Note DeleteModel(NoteViewModel noteFromView, Patient patient)
        {
            App.finishedAppointmentController.EraseNoteForAppointmentIfExists(Convert.ToInt32(noteFromView.NoteID), patient);
            Note note = App.noteController.GetNoteByID(Convert.ToInt32(noteFromView.NoteID));
            App.noteController.DeleteNote(note);
            
            return note;
        }
    }
}
