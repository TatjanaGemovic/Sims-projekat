using SIMS_Projekat.Model;
using SIMS_Projekat.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.Repository
{
    public class NoteRepository
    {
        private string path { get; set; }
        private Serializer<Note> serializer;
        private static List<Note> notesList;
        private int id;
        public List<Note> NotesList
        {
            get
            {
                if (notesList == null)
                    notesList = new List<Note>();
                return notesList;
            }
            set
            {
                RemoveAllNotes();
                if (value != null)
                {
                    foreach (Note note in value)
                        AddNote(note);
                }
            }
        }

        public NoteRepository(string path)
        {
            this.path = path;
            serializer = new Serializer<Note>();
            id = 0;
        }


        public Note SetNote(Note note)
        {
            int index;
            foreach (Note n in notesList)
            {
                Note note1 = notesList.Find(n => n.noteID == note.noteID);
                index = notesList.IndexOf(note1);

                if (note1 != null)
                {
                    note1.patient = note.patient;
                    note1.creationDate = note.creationDate;
                    note1.title = note.title;
                    note1.content = note.content;
                    
                    return note1;
                }

            }
            return null;
        }

        public Note GetNoteByID(int noteID)
        {
            foreach (Note note in notesList)
            {
                Note note1 = notesList.Find(note => note.noteID == noteID);

                if (note1 != null)
                {
                    return note1;
                }
            }
            return null;
        }

        public List<Note> GetNotesByPatientID(string patientID)
        {
            List<Note> notesListForPatient = new List<Note>();
            foreach (Note note in notesList)
            {
                if (note.patient.ID.Equals(patientID))
                {
                    notesListForPatient.Add(note);
                }
            }
            return notesListForPatient;
        }

        public List<Note> GetAllNotes()
        {
            return notesList;
        }

        public Note AddNote(Note newNote)
        {
            id++;
            if (newNote == null)
                return null;
            if (notesList == null)
                notesList = new List<Note>();
            if (!notesList.Contains(newNote))
            {
                newNote.noteID = id;
                notesList.Add(newNote);
            }

            return newNote;
        }
        public Note DeleteNote(Note oldNote)
        {
            if (oldNote == null)
                return null;
            if (notesList != null)
                if (notesList.Contains(oldNote))
                    notesList.Remove(oldNote);
            return oldNote;
        }

        public void RemoveAllNotes()
        {
            if (notesList != null)
                notesList.Clear();
        }
        public void Serialize()
        {
            serializer.toCSV(path, notesList);
        }

        public void Deserialize()
        {
            notesList = serializer.fromCSV(path);

            foreach (Note note in notesList)
            {
                id = note.noteID;
            }
        }
    }
}
