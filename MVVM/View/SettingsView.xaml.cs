using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace Cache_Memory_Benchmark.MVVM.View
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : UserControl
    {
        public SettingsView()
        {
            InitializeComponent();
            //if (Functions.ReadLang() == "uk")
            //    ComboBox.SelectedIndex = 1;
            //else
            //    ComboBox.SelectedIndex = 0;
        }

        string selectedLang = "";
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;

            string selectedTag = selectedItem.Tag.ToString();

            selectedLang = selectedTag;
            UpdateLang();


            ChangeLanguage(selectedTag);
        }

        public static void ChangeLanguage(string LanguageCode)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(LanguageCode);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(LanguageCode);

            Application.Current.Resources.MergedDictionaries.Clear();
            ResourceDictionary resourceDictionary = new ResourceDictionary()
            {
                Source = new Uri($"/Localization/Dictionary-{LanguageCode}.xaml", UriKind.Relative)
            };
            Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);

        }

        private void UpdateLang()
        {
            try
            {
                string fileContent = selectedLang;

                // Write the modified content back to the .bat file
                //File.WriteAllText(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\Localization\\Last_language.txt", fileContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }
}
