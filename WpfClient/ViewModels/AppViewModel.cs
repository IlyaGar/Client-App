using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WpfClient.Models;
using WpfClient.Services;
using System.Windows;
using System.IO;

namespace WpfClient.ViewModels
{
    public class AppViewModel : INotifyPropertyChanged
    {
        private Ship selectedShip;
        private string nameNewShip;
        private string pathImage;
        private string pathImageUpdate;
        private bool isSelect = false;
        private bool isChoose = false;
        private IService _service;

        public ObservableCollection<Ship> Ships { get; set; }

        public Ship SelectedShip
        {
            get { return selectedShip; }
            set
            {
                selectedShip = value;
                IsSelect = true;
                OnPropertyChanged("selectedShip");
            }
        }

        private Command addCommand;
        private Command chooseUpdateCommand;
        private Command chooseNewCommand;
        private Command updateCommand;
        private Command deleteCommand;

        public Command AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new Command(async obj =>
                  {
                      if (obj.ToString().Length > 0)
                      {
                          if (File.Exists(pathImage))
                          {
                              var shipUpload = new Ship() { Name = nameNewShip, Path = pathImage };
                              var ship = await _service.AddShipAsync(shipUpload);
                              Ships.Add(ship);
                          }
                          else MessageBox.Show("Choose Ship image.");
                      }
                      else MessageBox.Show("Enter Ship name.");
                  }));
            }
        }

        public Command ChooseNewCommand
        {
            get
            {
                return chooseNewCommand ??
                  (chooseNewCommand = new Command( obj =>
                  {
                      DefaultDialogService dialogService = new DefaultDialogService();
                      try
                      {
                          if (dialogService.OpenFileDialog() == true)
                          {
                              string filename = dialogService.FilePath;
                              PathImage = dialogService.FilePath;
                          }
                      }
                      catch (Exception ex)
                      {
                          dialogService.ShowMessage(ex.Message);
                      }
                  }));
            }
        }

        public Command ChooseUpdateCommand
        {
            get
            {
                return chooseUpdateCommand ??
                  (chooseUpdateCommand = new Command(obj =>
                  {
                      DefaultDialogService dialogService = new DefaultDialogService();
                      try
                      {
                          if (dialogService.OpenFileDialog() == true)
                          {
                              string filename = dialogService.FilePath;
                              PathImageUpdate = dialogService.FilePath;
                              IsChoose = true;
                          }
                      }
                      catch (Exception ex)
                      {
                          dialogService.ShowMessage(ex.Message);
                      }
                  }));
            }
        }

        public Command UpdateCommand
        {
            get
            {
                return updateCommand ??
                  (updateCommand = new Command(async obj =>
                  {
                      var shipUpload = new Ship() { Name = selectedShip.Name, Path = pathImageUpdate };
                      await _service.UpdateShipAsync(shipUpload);
                      Ships.Clear();
                      foreach (var item in await _service.GetShipsAsync())
                          Ships.Add(item);
                  }));
            }
        }

        public Command DeleteCommand
        {
            get
            {
                return deleteCommand ??
                  (deleteCommand = new Command(async obj =>
                  {
                      if (selectedShip != null)
                      {
                          await _service.DeleteShipAsync(selectedShip);
                          Ships.Remove(selectedShip);
                      }
                  },
                  (obj) => Ships.Count > 0));
            }
        }

        public string NameNewShip
        {
            get { return this.nameNewShip; }
            set
            {
                nameNewShip = value;
                OnPropertyChanged("NameNew");
            }
        }

        public string PathImage
        {
            get { return this.pathImage; }
            set
            {
                pathImage = value;
                OnPropertyChanged("PathImage");
            }
        }

        public string PathImageUpdate
        {
            get { return this.pathImageUpdate; }
            set
            {
                pathImageUpdate = value;
                OnPropertyChanged("PathImageUpdate");
            }
        }

        public bool IsSelect
        {
            get { return isSelect; }
            set
            {
                isSelect = value;
                OnPropertyChanged("IsSelect");
            }
        }

        public bool IsChoose
        {
            get { return isChoose; }
            set
            {
                isChoose = value;
                OnPropertyChanged("IsChoose");
            }
        }

        public AppViewModel(IService service)
        {
            _service = service;
            Ships = new ObservableCollection<Ship>();
            foreach (var item in _service.GetShipsAsync().Result)
                Ships.Add(item);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
