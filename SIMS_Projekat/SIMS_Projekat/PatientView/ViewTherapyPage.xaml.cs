﻿using SIMS_Projekat.PatientView.ViewModel;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for ViewTherapyPage.xaml
    /// </summary>
    public partial class ViewTherapyPage : Page
    {
        public ViewTherapyPage(Frame frame, TherapyViewModel vmTherapy)
        {
            InitializeComponent();
            this.DataContext = new SingleTherapyPageViewModel(frame, vmTherapy);
        }
    }
}
