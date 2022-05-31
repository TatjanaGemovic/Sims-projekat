using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.PatientView.ViewModel
{
    public class NoteViewModel : BindableBase
    {
        private string date { get; set; }
        private string time { get; set; }
        private string noteID { get; set; }
        private string content { get; set; }
        private string title { get; set; }
        private int index { get; set; }

        public string Date
        {
            get { return date; }
            set
            {
                if (date != value)
                {
                    date = value;
                    OnPropertyChanged("Date");

                }
            }
        }
        public string Time
        {
            get { return time; }
            set
            {
                if (time != value)
                {
                    time = value;
                    OnPropertyChanged("Time");

                }
            }
        }
        public string NoteID
        {
            get { return noteID; }
            set
            {
                if (noteID != value)
                {
                    noteID = value;
                    OnPropertyChanged("NoteID");

                }
            }
        }
        public string Content
        {
            get { return content; }
            set
            {
                if (content != value)
                {
                    content = value;
                    OnPropertyChanged("Content");

                }
            }
        }
        public string Title
        {
            get { return title; }
            set
            {
                if (title != value)
                {
                    title = value;
                    OnPropertyChanged("Title");

                }
            }
        }
        public int Index
        {
            get { return index; }
            set
            {
                if (index != value)
                {
                    index = value;
                    OnPropertyChanged("Index");

                }
            }
        }
    }
}
