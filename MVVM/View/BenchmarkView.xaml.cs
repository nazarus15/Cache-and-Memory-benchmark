using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
using static System.Net.Mime.MediaTypeNames;
using WinForms = System.Windows.Forms;

namespace Cache_Memory_Benchmark.MVVM.View
{
    /// <summary>
    /// Interaction logic for BenchmarkView.xaml
    /// </summary>
    public partial class BenchmarkView : UserControl
    {
        public BenchmarkView()
        {
            InitializeComponent();
            Renew_last_benchmark();
        }

        private void Start_Btn_Click(object sender, RoutedEventArgs e)
        {
            InitializeTextBoxes();
            //Functions.Write_Last_Result("Last_benchmark.txt", text);
            using (StreamWriter writer = new StreamWriter("History\\Last_benchmark.txt"))
            {
                writer.WriteLine(text);
            }
            Save_History();
        }

        private static string text = "";

        private void InitializeTextBoxes()
        {
            Read_L1.Text = Functions.Benckmark_Info("read_L1", false) + " MGb/s";
            Read_L2.Text = Functions.Benckmark_Info("read_L2", false) + " MGb/s";
            Read_L3.Text = Functions.Benckmark_Info("read_L3", false) + " MGb/s";
            Read_DRAM.Text = Functions.Benckmark_Info("read_DRAM", false) + " MGb/s";

            Write_L1.Text = Functions.Benckmark_Info("write_L1", false) + " MGb/s";
            Write_L2.Text = Functions.Benckmark_Info("write_L2", false) + " MGb/s";
            Write_L3.Text = Functions.Benckmark_Info("write_L3", false) + " MGb/s";
            Write_DRAM.Text = Functions.Benckmark_Info("write_DRAM", false) + " MGb/s";

            Copy_L1.Text = Functions.Benckmark_Info("copy_L1", false) + " MGb/s";
            Copy_L2.Text = Functions.Benckmark_Info("copy_L2", false) + " MGb/s";
            Copy_L3.Text = Functions.Benckmark_Info("copy_L3", false) + " MGb/s";
            Copy_DRAM.Text = Functions.Benckmark_Info("copy_DRAM", false) + " MGb/s";

            Latency_L1.Text = Functions.Benckmark_Info("latency_L1", true) + " ns";
            Latency_L2.Text = Functions.Benckmark_Info("latency_L2", true) + " ns";
            Latency_L3.Text = Functions.Benckmark_Info("latency_L3", true) + " ns";
            Latency_DRAM.Text = Functions.Benckmark_Info("latency_DRAM", true) + " ns";

            text = Read_L1.Text + "\n" + Read_L2.Text + "\n" + Read_L3.Text + "\n" + Read_DRAM.Text + "\n" + Write_L1.Text + "\n" + Write_L2.Text + "\n" + Write_L3.Text + "\n" + Write_DRAM.Text + "\n" + Copy_L1.Text + "\n" + Copy_L2.Text + "\n" + Copy_L3.Text + "\n" + Copy_DRAM.Text + "\n" + Latency_L1.Text + "\n" + Latency_L2.Text + "\n" + Latency_L3.Text + "\n" + Latency_DRAM.Text;
        }

        private void Renew_last_benchmark()
        {
            string path = "History\\Last_benchmark.txt";


            if(new FileInfo(path).Length != 0)
            {
                string fileContent = File.ReadAllText(path);

                string[] parts = fileContent.Split('\n');

                Read_L1.Text = parts[0].Trim();
                Read_L2.Text = parts[1].Trim();
                Read_L3.Text = parts[2].Trim();
                Read_DRAM.Text = parts[3].Trim();

                Write_L1.Text = parts[4].Trim();
                Write_L2.Text = parts[5].Trim();
                Write_L3.Text = parts[6].Trim();
                Write_DRAM.Text = parts[7].Trim();

                Copy_L1.Text = parts[8].Trim();
                Copy_L2.Text = parts[9].Trim();
                Copy_L3.Text = parts[10].Trim();
                Copy_DRAM.Text = parts[11].Trim();

                Latency_L1.Text = parts[12].Trim();
                Latency_L2.Text = parts[13].Trim();
                Latency_L3.Text = parts[14].Trim();
                Latency_DRAM.Text = parts[15].Trim();
            }

        }

        private void Save_History()
        {
            string path = "History\\History_benchmark.txt";


            string text = "**********************************************************************************************************\n";
            text += "\t"; 
            text += DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            text += "\n";
            text += "**********************************************************************************************************\n";

            text += "Read:\n";
            text += "L1: \t\t" + Read_L1.Text + "\n";
            text += "L2: \t\t" + Read_L2.Text + "\n";
            text += "L3: \t\t" + Read_L3.Text + "\n";
            text += "Memory: \t" + Read_DRAM.Text + "\n\n";

            text += "Write:\n";
            text += "L1: \t\t" + Read_L1.Text + "\n";
            text += "L2: \t\t" + Write_L2.Text + "\n";
            text += "L3: \t\t" + Write_L3.Text + "\n";
            text += "Memory: \t" + Write_DRAM.Text + "\n\n";

            text += "Copy:\n";
            text += "L1: \t\t" + Copy_L1.Text + "\n";
            text += "L2: \t\t" + Copy_L2.Text + "\n";
            text += "L3: \t\t" + Copy_L3.Text + "\n";
            text += "Memory: \t" + Copy_DRAM.Text + "\n\n";

            text += "Latency:\n";
            text += "L1: \t\t" + Latency_L1.Text + "\n";
            text += "L2: \t\t" + Latency_L2.Text + "\n";
            text += "L3: \t\t" + Latency_L3.Text + "\n";
            text += "Memory: \t" + Latency_DRAM.Text + "\n";

            text += "**********************************************************************************************************\n\n\n";

            string existingContent = File.ReadAllText(path);

            // Prepend the new text to the existing content
            string updatedContent = text + Environment.NewLine + existingContent;

            // Write the updated content back to the file
            File.WriteAllText(path, updatedContent);
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
