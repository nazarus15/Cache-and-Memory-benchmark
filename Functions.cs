using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows;
using WinForms = System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static System.Net.Mime.MediaTypeNames;
using System.Globalization;
using System.Threading;

namespace Cache_Memory_Benchmark
{
    public static class Functions
    {
        private static void RunScript(string file_name)
        {
            Process process = new Process();

            try
            {
                // Set the process start information
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = file_name + ".bat";      // The path to your .bat file
                startInfo.CreateNoWindow = true;              // Do not create a window
                startInfo.UseShellExecute = false;            // Do not use the shell to execute
                startInfo.RedirectStandardOutput = false;      // Redirect the output stream
                startInfo.RedirectStandardError = true;       // Redirect the error stream

                // Assign the start information to the process
                process.StartInfo = startInfo;

                // Start the process
                process.Start();

                // Wait for the process to exit
                process.WaitForExit();

            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {
                // Close the process to release resources
                process.Close();
                process.Dispose();
            }
        }



        private static string Read_Data(string file_name, bool avg_or_max)
        {
            string speed = "";
            try
            {
                StreamReader reader = new StreamReader("reports\\report_" + file_name + ".txt");
                string content = reader.ReadToEnd();
                reader.Close();


                string pattern = "";
                if(avg_or_max == true)
                {
                    pattern = @"average=(.*?),"; // Regular expression pattern
                }
                else
                {
                    pattern = @"max=(.*?),"; // Regular expression pattern
                }
                
                MatchCollection matches = Regex.Matches(content, pattern);

                if (matches.Count > 0)
                {
                    foreach (Match match in matches)
                    {
                        speed = match.Groups[1].Value.Trim();
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return speed;
        }

        public static string Benckmark_Info(string file_name, bool avg_or_max)
        {
            string data = "";

            RunScript(file_name);
            data = Read_Data(file_name, avg_or_max);

            return data;
        }


        public static string GetComponent(string hwclass, string syntax)
        {
            StringBuilder outputBuilder = new StringBuilder();

            try
            {
                ManagementObjectSearcher mos = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM " + hwclass);

                foreach (ManagementObject mj in mos.Get())
                {
                    string output = Convert.ToString(mj[syntax]);
                    outputBuilder.AppendLine(output);
                }
            }
            catch (Exception ex)
            {
                outputBuilder.AppendLine("An error occurred: " + ex.Message);
            }


            return outputBuilder.ToString();
        }



        public static string SplitString(string data, int num)
        {
            string[] parts = data.Split('\n');
            string result = "";

            if(num == 1)
            {
                result = parts[0].Trim() + " , " + parts[1].Trim();
            }
            if(num == 2)
            {
                result = parts[2].Trim();
            }
            if (num == 3)
            { 
                result = parts[3].Trim();  
            }

            return result;
        }

        public static void Write_Last_Result(string path, string text)
        {
            path = "History\\" + path;
            File.WriteAllText(path, string.Empty);
            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.WriteLine(text);
            }
        }



        public static void Save_Screenchot(FrameworkElement element, string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                // The string is either null or empty
                path = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            }
            path += "\\image.png";

            RenderTargetBitmap bmp = new RenderTargetBitmap((int)element.ActualWidth, (int)element.ActualHeight, 96, 96, PixelFormats.Pbgra32);
            bmp.Render(element);

            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bmp));

            FileStream fs = new FileStream(path, FileMode.Create);
            encoder.Save(fs);
            fs.Close();
        }
    }
}
