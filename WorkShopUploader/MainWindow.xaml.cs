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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace WorkShopUploader
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Rectangle_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // Note that you can have more than one file.
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                // Assuming you have one file that you care about, pass it off to whatever
                // handling code you have defined.
                txt_path.Text = files?[0] ?? throw new InvalidOperationException();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string[] args = Environment.GetCommandLineArgs();

            for (int i = 1; i < args.Length; i++)
            {
                if (args[i] == "-a")
                {
                    _archive = args[++i];
                }
                else if (args[i] == "-c")
                {
                    _buildfile = args[++i];
                }
                else if (args[i] == "-r")
                {
                    _rootpath = args[++i];
                }
            }

            lbl_filename.Content = $"Workshoptool is trying to upload a file.\nbig name:\t{_archive}\nroot path:\t{_rootpath}\nfiles list:\t\t{_buildfile}";
        }

        private string _archive = "Unknown";
        private string _rootpath = "Unknown";
        private string _buildfile = "Unknown";

        private void btn_browse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                txt_path.Text = openFileDialog.FileName;
        }

        private void btn_upload_Click(object sender, RoutedEventArgs e)
        {
            File.Copy(txt_path.Text, _archive);
            Application.Current.MainWindow?.Close();
        }
    }
}
