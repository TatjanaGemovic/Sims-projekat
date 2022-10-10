using SIMS_Projekat.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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

namespace SIMS_Projekat.ManagerView
{
    /// <summary>
    /// Interaction logic for HelpView.xaml
    /// </summary>
    public partial class HelpView : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Stream docStream;

        public Stream DocumentStream
        {
            get 
            {
               
                    return docStream;
            }
            set
            {

                docStream = value;
                OnPropertyChanged(nameof(DocumentStream));


            }
            
        }

        public HelpView()
        {
            InitializeComponent();
            this.DataContext = this;
            

        }

        public void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void TextBox_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }
    }
}
