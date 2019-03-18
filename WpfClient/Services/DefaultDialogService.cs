using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfClient.Services
{
    public class DefaultDialogService : IDialogService
    {
        public string FilePath { get; set; }
        public string FileName { get; set; }

        public bool OpenFileDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                FilePath = openFileDialog.FileName;
                FileName = Path.GetFileName(FilePath);
                return true;
            }
            return false;
        }

        public void ShowMessage(string message) =>
            MessageBox.Show(message);
    }
}
