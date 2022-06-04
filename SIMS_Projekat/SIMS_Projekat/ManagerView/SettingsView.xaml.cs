using SIMS_Projekat.Properties;
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

namespace SIMS_Projekat.ManagerView
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : Page
    {
        public SettingsView()
        {
            InitializeComponent();
            if (Settings.Default.CurrentTheme == "Dark")
                temaCombo.SelectedIndex = 0;
            else
                temaCombo.SelectedIndex = 1;
        }

        private void ChangeTheme(Uri uri)
        {
            App.ThemeDictionary.MergedDictionaries.Clear();
            App.ThemeDictionary.MergedDictionaries.Add(new ResourceDictionary() { Source = uri });

        }

        private void temaCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (temaCombo.SelectedIndex == 1)
            {
                ChangeTheme(new Uri("Theme/LightTheme.xaml", UriKind.RelativeOrAbsolute));
                Settings.Default.CurrentTheme = "Light";
                Settings.Default.Save();

            }
            else
            {
                ChangeTheme(new Uri("Theme/DarkTheme.xaml", UriKind.RelativeOrAbsolute));
                Settings.Default.CurrentTheme = "Dark";
                Settings.Default.Save();
            }
        }
    }
}
