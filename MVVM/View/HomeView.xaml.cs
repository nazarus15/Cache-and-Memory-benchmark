using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WinForms = System.Windows.Forms;

namespace Cache_Memory_Benchmark.MVVM.View
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();
            Renew_last_metrics();
        }

        private static string text = "";

        private void InitializeTextBoxes()
        {
            CPU_Name_tb.Text = (Functions.GetComponent("Win32_Processor", "Name")).TrimEnd();
            CPU_Type_tb.Text = (Functions.GetComponent("Win32_Processor", "Caption")).TrimEnd();
            CPU_Revision_tb.Text = (Functions.GetComponent("Win32_Processor", "Revision")).TrimEnd();
            CPU_Cores_tb.Text = (Functions.GetComponent("Win32_Processor", "NumberOfCores")).TrimEnd();
            CPU_LOgicalProc_tb.Text = (Functions.GetComponent("Win32_Processor", "NumberOfLogicalProcessors")).TrimEnd();
            CPU_ClockSpeed_tb.Text = (Functions.GetComponent("Win32_Processor", "MaxClockSpeed")).TrimEnd() + " MHZ";
            Motherboard_tb.Text = (Functions.GetComponent("Win32_BaseBoard", "SerialNumber")).TrimEnd();

            L1_Size_tb.Text = Functions.SplitString(((Functions.GetComponent("Win32_CacheMemory", "MaxCacheSize")).TrimEnd()),1) + " Kb";
            L2_Size_tb.Text = Functions.SplitString(((Functions.GetComponent("Win32_CacheMemory", "MaxCacheSize")).TrimEnd()), 2) + " Kb";
            L3_Size_tb.Text = Functions.SplitString(((Functions.GetComponent("Win32_CacheMemory", "MaxCacheSize")).TrimEnd()), 3) + " Kb";

            string dram = (Functions.GetComponent("Win32_ComputerSystem", "TotalPhysicalMemory")).TrimEnd();
            float megabytes = float.Parse(dram);
            megabytes = megabytes / 1073741824;
            DRAM_Size_tb.Text = megabytes.ToString("0.00") + " Gb";

            text = CPU_Name_tb.Text + "\n" + CPU_Type_tb.Text + "\n" + CPU_Revision_tb.Text + "\n" + CPU_Cores_tb.Text + "\n" + CPU_LOgicalProc_tb.Text + "\n" + CPU_ClockSpeed_tb.Text + "\n" + Motherboard_tb.Text + "\n" + L1_Size_tb.Text + "\n" + L2_Size_tb.Text + "\n" + L3_Size_tb.Text + "\n" + DRAM_Size_tb.Text;
        }

        private void Renew_last_metrics()
        {
            string path = "History\\Last_metrics.txt";

            if (new FileInfo(path).Length != 0)
            {
                string fileContent = File.ReadAllText(path);

                string[] parts = fileContent.Split('\n');

                CPU_Name_tb.Text = parts[0].Trim();
                CPU_Type_tb.Text = parts[1].Trim();
                CPU_Revision_tb.Text = parts[2].Trim();
                CPU_Cores_tb.Text = parts[3].Trim();
                CPU_LOgicalProc_tb.Text = parts[4].Trim();
                CPU_ClockSpeed_tb.Text = parts[5].Trim();
                Motherboard_tb.Text = parts[6].Trim();

                L1_Size_tb.Text = parts[7].Trim();
                L2_Size_tb.Text = parts[8].Trim();
                L3_Size_tb.Text = parts[9].Trim();

                DRAM_Size_tb.Text = parts[10].Trim();
            }

        }

        private void Start_Btn_Click(object sender, RoutedEventArgs e)
        {
            InitializeTextBoxes();

            File.WriteAllText("History\\Last_metrics.txt", text);
            //using (StreamWriter writer = new StreamWriter("History\\Last_metrics.txt"))
            //{
            //    writer.WriteLine(text);
            //}
            //Functions.Write_Last_Result("Last_metrics.txt", text);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WinForms.FolderBrowserDialog dialog = new WinForms.FolderBrowserDialog();

            string DestinationDirectory = "";

            WinForms.DialogResult result = dialog.ShowDialog();

            if (result == WinForms.DialogResult.OK)
            {
                DestinationDirectory = dialog.SelectedPath;
            }

            Functions.Save_Screenchot(this, DestinationDirectory);
        }
    }
}
