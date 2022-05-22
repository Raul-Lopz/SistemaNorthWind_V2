using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaNorthWind_V2.Persistencia.DapperConexion.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaNorthWind_V2.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ICustomers _CustomersRepository;
        public CustomersController(ICustomers CustomersRepository)
        {
            _CustomersRepository = CustomersRepository;
        }
        public async Task<IActionResult> Index()
        {

            var a= await _CustomersRepository.ObtenerPaises();
            ViewBag.paises = new SelectList(a);

            return View();
        }

        public async Task<IActionResult> ObtenerClientes(int? page,string pais=null)
        {
            var records = await _CustomersRepository.ObtenerPorPais(pais);
            return Json(records);
        }
    }
}
