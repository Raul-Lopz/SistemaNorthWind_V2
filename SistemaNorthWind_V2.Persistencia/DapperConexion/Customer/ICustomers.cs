using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaNorthWind_V2.Persistencia.DapperConexion.Customer
{
    public interface ICustomers
    {
        Task<List<string>> ObtenerPaises();

        Task<List<CustomerModel>> ObtenerPorPais(string pais);
    }
}
