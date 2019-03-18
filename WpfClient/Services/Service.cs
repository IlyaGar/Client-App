using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using WpfClient.Models;

namespace WpfClient.Services
{
    public class Service : IService
    {
        private HttpClient httpClient;

        public Service()
        {
            httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:63161/")
            };
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task<Ship> AddShipAsync(Ship ship)
        {
            if (ship.Name.Length > 0)
            {
                var shipUpload = new ShipUpload(ship.Name, File.ReadAllBytes(ship.Path)); 
                HttpResponseMessage responseMessage = await httpClient.PostAsJsonAsync("api/values", shipUpload);
                if (responseMessage.IsSuccessStatusCode)
                {
                    ship = await responseMessage.Content.ReadAsAsync<Ship>();
                    MessageBox.Show(" New Ship\n Name: " + ship.Name);
                }
                else MessageBox.Show("Oops");
            }
            else MessageBox.Show("Enter ship name");

            return ship;
        }

        public async Task<IEnumerable<Ship>> GetShipsAsync()
        {

            IEnumerable<Ship> listShips = null;
            try
            {
                HttpResponseMessage responseMessage = httpClient.GetAsync("api/values").Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    listShips = await responseMessage.Content.ReadAsAsync<IEnumerable<Ship>>();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Server is disabled\n" + e.Message.ToString());
            }
            return listShips;
        }

        public async Task UpdateShipAsync(Ship shipUdate)
        {
            if (shipUdate != null)
            {
                byte[] byteArray = GetJPGFromImagePath(Path.GetFullPath(shipUdate.Path));

                HttpResponseMessage responseMessage = await httpClient.PutAsJsonAsync("api/values/" + shipUdate.Name, byteArray);

                if (responseMessage.IsSuccessStatusCode)
                {
                    MessageBox.Show("Image for " + shipUdate.Name + " changed.");
                }
                else MessageBox.Show("Oops");
            }
            else MessageBox.Show("Choose Ship.");
        }

        public async Task DeleteShipAsync(Ship ship)
        {
            if (ship.Name.Length > 0)
            {
                HttpResponseMessage responseMessage = await httpClient.DeleteAsync("api/values/" + ship.Name);
                if (responseMessage.IsSuccessStatusCode)
                {
                    MessageBox.Show("Ship " + ship.Name + " removed.");
                }
                else MessageBox.Show("Oops");
            }
            else MessageBox.Show("Enter ship name.");
        }

        private byte[] GetJPGFromImageControl(BitmapImage image)
        {
            using (var memStream = new MemoryStream())
            {
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(image));
                encoder.Save(memStream);
                return memStream.ToArray();
            }
        }

        private byte[] GetJPGFromImagePath(string path)
        {
            byte[] byteArray = File.ReadAllBytes(path);
            return byteArray;
        }

        private BitmapImage LoadImage(string url)
        {
            BitmapImage bmp = new BitmapImage(new Uri(url));
            return bmp;
        }
    }
}
