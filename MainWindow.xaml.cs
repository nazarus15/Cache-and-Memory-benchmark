using System;
using System.Management;
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
using System.Windows.Forms;
using Cache_Memory_Benchmark.MVVM.ViewModel;
using Cache_Memory_Benchmark.MVVM.View;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using Forms = System.Windows.Forms;

namespace Cache_Memory_Benchmark
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Forms.NotifyIcon notifyIcon = new Forms.NotifyIcon();

        public MainWindow()
        {
            InitializeComponent();
            notifyIcon.Icon = new System.Drawing.Icon("Images\\Icon_small.ico");
            notifyIcon.Text = "Cache and Memory Benchmark";
            notifyIcon.Visible = true;

            notifyIcon.MouseClick += NotifyIcon_MouseClick; // Add event handler

            notifyIcon.ContextMenuStrip = new Forms.ContextMenuStrip();
            notifyIcon.ContextMenuStrip.Items.Add("Open app", System.Drawing.Image.FromFile("Images\\Icon_small.ico"), OnMaximizeClicked);
            notifyIcon.ContextMenuStrip.Items.Add("Hide app", System.Drawing.Image.FromFile("Images\\Icon_small.ico"), OnMinimizeClicked);
            notifyIcon.ContextMenuStrip.Items.Add("Exit", System.Drawing.Image.FromFile("Images\\Icon_small.ico"), OnExitClicked);
        }

        private void OnMaximizeClicked(object sender, EventArgs e)
        {
            Show(); // Show the application window
            WindowState = WindowState.Normal; // Restore the window if it was minimized
        }

        private void OnMinimizeClicked(object sender, EventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void OnExitClicked(object sender, EventArgs e)
        {
            this.Close();
            notifyIcon.Dispose();
        }
        private void NotifyIcon_MouseClick(object sender, Forms.MouseEventArgs e)
        {
            if (e.Button == Forms.MouseButtons.Left)
            {
                Show(); // Show the application window
                WindowState = WindowState.Normal; // Restore the window if it was minimized
            }
        }
    



        private void MovingWin(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Close_Btn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }



        private void Hide_Btn_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }


    }
}
