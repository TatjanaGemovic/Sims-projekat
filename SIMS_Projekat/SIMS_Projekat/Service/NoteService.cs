using SIMS_Projekat.Model;
using SIMS_Projekat.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.Service
{
    public class NoteService
    {
        public NoteRepository noteRepository;
        public Note SetNote(Note note)
        {
            return noteRepository.SetNote(note);
        }

        public Note DeleteNote(Note note)
        {
            return noteRepository.DeleteNote(note);
        }

        public Note AddNote(Note note)
        {
            return noteRepository.AddNote(note);
        }

        public Note GetNoteByID(int notetID)
        {
            return noteRepository.GetNoteByID(notetID);
        }

        public List<Note> GetNotesByPatientID(string patientID)
        {
            return noteRepository.GetNotesByPatientID(patientID);
        }

        public List<Note> GetAllNotes()
        {
            return noteRepository.GetAllNotes();
        }
    }
}
