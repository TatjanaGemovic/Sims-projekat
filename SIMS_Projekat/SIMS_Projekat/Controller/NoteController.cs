using SIMS_Projekat.Model;
using SIMS_Projekat.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.Controller
{
    public class NoteController
    {
        public NoteService noteService;
        public Note SetNote(Note note)
        {
            return noteService.SetNote(note);
        }

        public Note DeleteNote(Note note)
        {
            return noteService.DeleteNote(note);
        }

        public Note AddNote(Note note)
        {
            return noteService.AddNote(note);
        }

        public Note GetNoteByID(int notetID)
        {
            return noteService.GetNoteByID(notetID);
        }

        public List<Note> GetNotesByPatientID(string patientID)
        {
            return noteService.GetNotesByPatientID(patientID);
        }

        public List<Note> GetAllNotes()
        {
            return noteService.GetAllNotes();
        }
    }
}
