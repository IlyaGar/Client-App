using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using WpfClient.Services;
using WpfClient.ViewModels;

namespace WpfClient.Models
{
    public class ShipUpload
    {
        //public string Name { get; set; }

        //public byte[] ImgByte { get; set; }

        //public ShipUpload(string name, byte[] img)
        //{
        //    Name = name;
        //    ImgByte = img;
        //}

        //public ShipUpload()
        //{
        //}

        private string name;
        private byte[] imgByte;

        public ShipUpload()
        {
        }

        public ShipUpload(string name, byte[] v)
        {
            this.name = name;
            this.imgByte = v;
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public byte[] ImgByte
        {
            get { return imgByte; }
            set
            {
                imgByte = value;
                OnPropertyChanged("ImageByte");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
