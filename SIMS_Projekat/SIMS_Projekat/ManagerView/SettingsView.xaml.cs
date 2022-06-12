using MenuNavigation;
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

        private string currentLanguage;
        public string CurrentLanguage
        {
            get { return currentLanguage; }
            set
            {
                currentLanguage = value;
            }
        }
        public SettingsView()
        {
            InitializeComponent();
            if (Settings.Default.CurrentTheme == "Light")
                temaCombo.SelectedIndex = 1;
            else
                temaCombo.SelectedIndex = 0;


            if (Settings.Default.CurrentLanguage== "en-US")
            {
                jezikCombo.SelectedIndex = 1;
            }
            else
                jezikCombo.SelectedIndex = 0;
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

        private void jezikCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (jezikCombo.SelectedIndex == 0)
            {
                Execute_SwitchLanguageCommand("sr-LATN");
                Settings.Default.CurrentLanguage = "sr-LATN";
                Settings.Default.Save();
            }
            else 
            {
                Execute_SwitchLanguageCommand("en-US");
                Settings.Default.CurrentLanguage = "en-US";
                Settings.Default.Save();
            }
        }

        private void Execute_SwitchLanguageCommand(string st)
        {
            var app = (App)Application.Current;
            CurrentLanguage = st;
            app.ChangeLanguage(CurrentLanguage);
        }
    }
}

