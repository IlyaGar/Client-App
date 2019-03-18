using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WpfClient.Models;

namespace WpfClient.ViewModels
{
public class ShipViewModel : INotifyPropertyChanged
    {
        private Ship ship;

        public ShipViewModel(Ship s)
        {
            ship = s;
        }

        public string Name
        {
            get { return ship.Name; }
            set
            {
                ship.Name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Path
        {
            get { return ship.Path; }
            set
            {
                ship.Path = value;
                OnPropertyChanged("Path");
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
