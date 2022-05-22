using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaNorthWind_V2.Persistencia.DapperConexion.Orders
{
    public class OrdersRepository : IOrders
    {
        private readonly IFactoryConnection _factoryConnection;

        public OrdersRepository(IFactoryConnection factoryConnection)
        {
            _factoryConnection = factoryConnection;
        }

        public async Task<List<OrdersModel>> ObtenerPorCliente(string customerID)
        {
            string storeProcedure = "proc_ordenes_por_clienteid";
            List<OrdersModel> ordenes = null;
            try
            {
                var connection = _factoryConnection.GetConnection();
                ordenes = (await connection.QueryAsync<OrdersModel>(
                         storeProcedure,
                         new
                         {
                             ID = customerID
                         }, commandType: CommandType.StoredProcedure)).ToList();
            }
            catch (Exception)
            {
                throw new Exception("Error en la consulta de datos");
            }
            finally
            {
                _factoryConnection.CloseConnection();
            }
            return ordenes;
        }

        public async Task<double> ObtenerSumatoriaTransporte(string customerID)
        {
            string storeProcedure = "proc_suma_transporte";
            double sumatoria = 0.0;
            try
            {
                var connection = _factoryConnection.GetConnection();
                sumatoria = (await connection.QueryFirstOrDefaultAsync<double>(
                         storeProcedure,
                         new
                         {
                             ID = customerID
                         }, commandType: CommandType.StoredProcedure));
            }
            catch (Exception)
            {
                throw new Exception("Error en la consulta de datos");
            }
            finally
            {
                _factoryConnection.CloseConnection();
            }
            return sumatoria;
        }
        public async Task<int> Agregar(OrdersModel modelo)
        {
            string storeProcedure = "proc_nueva_orden";
            try
            {
                var connection = _factoryConnection.GetConnection();
                var resultado=await connection.ExecuteAsync(storeProcedure, 
                    new 
                    {
                        CustomerID=modelo.CustomerID,
                        ShipCity=modelo.ShipCity,
                        Freight=modelo.Freight
                    }, 
                    commandType: CommandType.StoredProcedure);
                return resultado;
            }
            catch (Exception )
            {
                throw new Exception("Error en la consulta de datos");
            }
            finally
            {
                _factoryConnection.CloseConnection();
            }
        }

        public async Task<OrdersModel> ObtenerPorId(int? orderID)
        {
            string storeProcedure = "proc_obtener_orden_id";
            OrdersModel orden = null;
            try
            {
                var connection = _factoryConnection.GetConnection();
                orden=(await connection.QueryFirstOrDefaultAsync<OrdersModel>(
                         storeProcedure,
                         new
                         {
                             OrderID = orderID
                         }, commandType: CommandType.StoredProcedure));
            }
            catch (Exception)
            {
                throw new Exception("Error en la consulta de datos");
            }
            finally
            {
                _factoryConnection.CloseConnection();
            }
            return orden;
        }

        public async Task<int> Editar(OrdersModel modelo)
        {
            string storeProcedure = "proc_actualizar_freight";
            try
            {
                var connection = _factoryConnection.GetConnection();
                var resultado = await connection.ExecuteAsync(storeProcedure,
                    new
                    {
                        OrderID = modelo.OrderID,
                        Freight = modelo.Freight
                    },
                    commandType: CommandType.StoredProcedure);
                return resultado;
            }
            catch (Exception )
            {
                throw new Exception("Error en la consulta de datos");
            }
            finally
            {
                _factoryConnection.CloseConnection();
            }
        }

        public async Task<int> Eliminar(List<int> ordenesIDs)
        {
            string storeProcedure = "proc_eliminar_orden";
            int resultado = 0;
            int rowAfectados = 0;
            try
            {
                foreach(var id in ordenesIDs)
                {
                    var connection = _factoryConnection.GetConnection();
                    resultado = await connection.ExecuteAsync(storeProcedure,
                        new
                        {
                            OrderID = id,
                        },
                        commandType: CommandType.StoredProcedure);
                    _factoryConnection.CloseConnection();
                    rowAfectados += resultado;
                }
                if (ordenesIDs.Count == rowAfectados)
                    return 1;
                else
                    return 0;
            }
            catch (Exception )
            {
                throw new Exception("Error en la consulta de datos");
            }
            finally
            {
                _factoryConnection.CloseConnection();
            }
        }
    }
}
