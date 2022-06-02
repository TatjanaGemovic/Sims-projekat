using SIMS_Projekat.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SIMS_Projekat.DoctorView.ViewModel
{
    public class MedicineInfoViewModel : BindableBase
    {
        private Frame frame;
        private Doctor doctor;
        private Medicine medicine;
        private string name;
        private string dose;
        private string use;
        private string comment;
        public MyICommand approveCommand { get; set; }
        public MyICommand denyCommand { get; set; }
        public MyICommand backCommand { get; set; }

        public MedicineInfoViewModel(Frame f, Doctor d, Medicine m)
        {
            frame = f;
            doctor = d;
            medicine = m;
            name = m.MedicineName;
            dose = m.MedicineDose.ToString();
            use = m.pMedicineUseType.ToString();

            approveCommand = new MyICommand(OnApprove);
            denyCommand = new MyICommand(OnDeny);
            backCommand = new MyICommand(OnBack);
        }

        private void OnDeny()
        {
            medicine.Verify = false;
            medicine.DoctorComment = Comment;
            medicine.OnObservation = false;

            App.medicineRepository.Serialize();
            frame.Content = new Medicines(frame, doctor);
        }

        private void OnApprove()
        {
            medicine.Verify = true;
            medicine.OnObservation = false;

            App.medicineRepository.Serialize();
            frame.Content = new Medicines(frame, doctor);
        }

        private void OnBack()
        {
            frame.Content = new Medicines(frame, doctor);
        }

        public string Name
        {
            get { return name; }
        }
        public string Dose
        {
            get { return dose; }
        }
        public string Use
        {
            get { return use; }
        }
        public string Comment
        {
            get { return comment; }
            set
            {
                if(Comment != value)
                {
                    comment = value;
                    OnPropertyChanged("Comment");
                }
            }
        }
    }
}
