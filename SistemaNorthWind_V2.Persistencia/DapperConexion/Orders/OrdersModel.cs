using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SistemaNorthWind_V2.Persistencia.DapperConexion.Orders
{
    public class OrdersModel
    {
        public int OrderID { get; set; }
        public string CustomerID { get; set; }
        public string CompanyName { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        [Range(0,double.MaxValue,ErrorMessage ="Por favor introducir una cantidad valida")]
        public decimal? Freight { get; set; }
        [StringLength(15,ErrorMessage ="Deben ser como máximo 15 caracteres")]
        public string ShipCity { get; set; }
    }
}
