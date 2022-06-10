﻿using SIMS_Projekat.Controller;
using SIMS_Projekat.Model;
using System;
using System.Collections.Generic;
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

namespace SIMS_Projekat.ManagerView
{
    /// <summary>
    /// Interaction logic for EditEquipmentView.xaml
    /// </summary>
    public partial class EditEquipmentView : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Equipment selectedEquipment;
        private string _kolicina;
        private string _naziv;

        public string Kolicina
        {
            get { return _kolicina; }
            set { _kolicina = value; OnPropertyChanged(nameof(Kolicina)); }
        }

        public string Naziv
        {
            get { return _naziv; }
            set { _naziv = value; OnPropertyChanged(nameof(Naziv)); }
        }


        public EditEquipmentView(Equipment oldEquipment)
        {
            InitializeComponent();
            this.DataContext = this;
            selectedEquipment = oldEquipment;
            fillForm();
        }

        private void EditEquipment_Click(object sender, RoutedEventArgs e)
        {
            App.equipmentController.EditEquipment(selectedEquipment.EquipmentID, getEquipmentFromForm());
            ManagerHome.mainFrame.Content = new EquipmentView();
        }

        private Equipment getEquipmentFromForm()
        {
            Equipment newEquipment = new Equipment();
            newEquipment.EquipmentID = selectedEquipment.EquipmentID;
            newEquipment.EquipmentName = equipmentName.Text;
            newEquipment.Quantity = int.Parse(equipmentQuantity.Text);
            newEquipment.pEquipmentType = (EquipmentType)equipmentType.SelectedIndex;
            return newEquipment;
        }

        private void fillForm()
        {
            Naziv = selectedEquipment.EquipmentName;
            Kolicina= selectedEquipment.Quantity.ToString();
            equipmentType.SelectedIndex = (int)selectedEquipment.pEquipmentType;
        }

        private void QuitEditing_Click(object sender, RoutedEventArgs e)
        {
            ManagerHome.mainFrame.Content = new EquipmentView();
        }

        public void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void equipmentName_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            EditEquipment.IsEnabled = Validation.GetHasError(tb) == true ? false : true;
        }

        private void equipmentQuantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            EditEquipment.IsEnabled = Validation.GetHasError(tb) == true ? false : true;
        }
    }
}
