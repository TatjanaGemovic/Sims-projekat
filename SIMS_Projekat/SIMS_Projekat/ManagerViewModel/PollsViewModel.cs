using SIMS.CompositeComon;
using SIMS_Projekat.ManagerView;
using SIMS_Projekat.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.ManagerViewModel
{
    public class PollsViewModel : INotifyPropertyChanged
    {
        private Evaluation _selectedItem;
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Evaluation> _poll;
        private RelayCommand addMedicine;
        private RelayCommand pollView;

        public PollsViewModel()
        {
            Polls = new ObservableCollection<Evaluation>(App.evaluationController.GetAllEvaluations());

        }

        public Evaluation SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        public RelayCommand CreateReport
        {
            get
            {
                return addMedicine ?? (new RelayCommand(param => CreateReportBtn_Click()));
            }
        }

        public RelayCommand PollView
        {
            get
            {
                return pollView ?? (new RelayCommand(param => PrikaziAnketuBtn_Click(), param => canCommandExecut()));
            }
        }

        public ObservableCollection<Evaluation> Polls
        {
            get { return _poll; }
            set
            {
                _poll = value;
                OnPropertyChanged(nameof(Polls));
            }
        }


        private Boolean canCommandExecut()
        {
            return SelectedItem != null;
        }

        public void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void PrikaziAnketuBtn_Click()
        {
             ManagerHome.mainFrame.Content = new PollView(SelectedItem);
        }

        private void CreateReportBtn_Click()
        {
            ManagerHome.mainFrame.Content = new ManagerCreateReport();
        }

    }
}
