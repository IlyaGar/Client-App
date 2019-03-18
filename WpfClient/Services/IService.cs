using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfClient.Models;

namespace WpfClient.Services
{
    public interface IService
    {
        Task<IEnumerable<Ship>> GetShipsAsync();

        Task<Ship> AddShipAsync(Ship shipUpload);

        Task UpdateShipAsync(Ship shipUdate);

        Task DeleteShipAsync(Ship ship);
    }
}
