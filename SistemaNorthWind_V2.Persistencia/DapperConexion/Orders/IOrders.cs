using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaNorthWind_V2.Persistencia.DapperConexion.Orders
{
    public interface IOrders
    {
        Task<List<OrdersModel>> ObtenerPorCliente(string CustomerID);

        Task<OrdersModel> ObtenerPorId(int? orderID);

        Task<double> ObtenerSumatoriaTransporte(string CustomerID);

        Task<int> Agregar(OrdersModel modelo);

        Task<int> Editar(OrdersModel modelo);
        Task<int> Eliminar(List<int> ordenesIDs);
    }
}
