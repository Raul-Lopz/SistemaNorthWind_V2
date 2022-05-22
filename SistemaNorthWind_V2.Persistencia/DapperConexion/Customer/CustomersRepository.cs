using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaNorthWind_V2.Persistencia.DapperConexion.Customer
{
    public class CustomersRepository : ICustomers
    {
        private readonly IFactoryConnection _factoryConnection;

        public CustomersRepository(IFactoryConnection factoryConnection)
        {
            _factoryConnection = factoryConnection;
        }

        public async Task<List<string>> ObtenerPaises()
        {
            string storeProcedure = "proc_paises_costumers";
            var paises = new List<string>();
            try
            {
                var connection = _factoryConnection.GetConnection();
                paises = (await connection.QueryAsync<string>(storeProcedure, null, commandType: CommandType.StoredProcedure)).ToList();
            }
            catch (Exception)
            {
                throw new Exception("Error en la consulta de datos");
            }
            finally
            {
                _factoryConnection.CloseConnection();
            }
            return paises;
        }

        public async Task<List<CustomerModel>> ObtenerPorPais(string pais)
        {
            string storeProcedure = "proc_clientes_por_pais";
            List<CustomerModel> clientes = null;
            try
            {
                var connection = _factoryConnection.GetConnection();
                clientes = (await connection.QueryAsync<CustomerModel>(
                         storeProcedure,
                         new { 
                            pais=pais
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
            return clientes;
        }
    }
}
