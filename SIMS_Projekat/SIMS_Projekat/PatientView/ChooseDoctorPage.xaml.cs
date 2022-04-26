using SIMS_Projekat.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SIMS_Projekat.PatientView
{
    /// <summary>
    /// Interaction logic for ChooseDoctorPage.xaml
    /// </summary>
    public partial class ChooseDoctorPage : Page
    {
        Patient patient;
        public ObservableCollection<DoctorInfo> doctorInfoList = new ObservableCollection<DoctorInfo>();
        string doctorName;
        public DoctorInfo doctorInfo;
        public ChooseDoctorPage(Patient p)
        {
            patient = p;
           
            InitializeComponent();

            if (patient.doctorLicenceNumber != "")
            {
                doctorName = App.accountController.GetDoctorAccountByLicenceNumber(patient.doctorLicenceNumber).FirstName
                                    + " " + App.accountController.GetDoctorAccountByLicenceNumber(patient.doctorLicenceNumber).LastName;
            }
            else
            {
                doctorName = "";
            }

            existing_doctor.Text = doctorName;

            InitializeComboBox();

        }
        public class DoctorInfo
        {
            public string doctorName { get; set; }
            public string licenceNumber { get; set; }
            
            public DoctorInfo(string doctor, string licence)
            {
                
                doctorName = doctor;
                licenceNumber = licence;
            }
        }
        public void InitializeComboBox()
        {
            foreach(Doctor doctor in App.accountController.GetAllDoctorAccounts())
            {
               doctorInfoList.Add(new DoctorInfo(doctor.FirstName + " " + doctor.LastName, doctor.LicenceNumber));
                
            }
            doctorComboBox.ItemsSource = doctorInfoList;
        }

        private void doctorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            var drInfo = doctorComboBox.SelectedItem as DoctorInfo;
            
            patient.doctorLicenceNumber = drInfo.licenceNumber;
            

        }
    }
}
