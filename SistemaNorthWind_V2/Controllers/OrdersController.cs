using Microsoft.AspNetCore.Mvc;
using SistemaNorthWind_V2.Persistencia.DapperConexion.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaNorthWind_V2.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrders _OrdersRepository;
        public OrdersController(IOrders OrdersRepository)
        {
            _OrdersRepository = OrdersRepository;
        }


        public async Task<IActionResult> ObtenerOrdenes(int? page, string customerID = null)
        {
            var records = await _OrdersRepository.ObtenerPorCliente(customerID);
            return Json(records);
        }


        public async Task<IActionResult> ObtenerSumatoriaFreight(string customerID)
        {
            var resultado = await _OrdersRepository.ObtenerSumatoriaTransporte(customerID);
            return Json(data:new { resultado });
        }

        public IActionResult Agregar(string customerID)
        {
            ViewBag.customerID = customerID;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Agregar(OrdersModel modelo)
        {
            try
            {
                var resultado = await _OrdersRepository.Agregar(modelo);
                if(resultado==1)
                    return Json(data: new { status = true, mensaje = "La orden se agregó correctamente",customerID=modelo.CustomerID });
                else
                    return Json(data: new { status = false, mensaje = "No se pudo agregar la orden" });
            }
            catch (Exception)
            {
                return Json(data: new { status = false, mensaje = "Hubo un error en el servidor" });
            }
        }

        public async Task<IActionResult> Editar(int? orderID)
        {
            if (orderID == null)
                return StatusCode(400);
            var modelo = await _OrdersRepository.ObtenerPorId(orderID);
            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(OrdersModel modelo)
        {
            try
            {
                var resultado = await _OrdersRepository.Editar(modelo);
                if (resultado == 1)
                    return Json(data: new { status = true, mensaje = "La orden se editó correctamente", customerID = modelo.CustomerID });
                else
                    return Json(data: new { status = false, mensaje = "No se pudo editar la orden" });
            }
            catch (Exception)
            {
                return Json(data: new { status = false, mensaje = "Hubo un error en el servidor" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Eliminar(List<int> ordenesIDs, string customerID)
        {
            try
            {
                var resultado = await _OrdersRepository.Eliminar(ordenesIDs);
                if (resultado == 1)
                    return Json(data: new { status = true, mensaje = "Se eliminaron las ordenes correctamente", customerID =customerID });
                else
                    return Json(data: new { status = false, mensaje = "Algunas ordenes no se eliminaron" });
            }
            catch (Exception)
            {
                return Json(data: new { status = false, mensaje = "Hubo un error en el servidor" });
            }
        }


    }
}
