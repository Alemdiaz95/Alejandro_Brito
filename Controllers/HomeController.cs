using Alejandro_Brito.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

namespace Alejandro_Brito.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly AlejandroBritoContext _DBContext;

        public HomeController(AlejandroBritoContext context)
        {
            _DBContext = context;
        }

        public IActionResult Index()
        {
            List<Tarea> lista = _DBContext.Tareas.Include(c => c.oEstado).ToList();
            return View(lista);
        }

        [HttpGet]
        public IActionResult Tarea_Detalle(int idTarea)
        {
            TareaVM oTareaVM = new TareaVM()
            {
                oTarea = new Tarea(),
                oListaEstado = _DBContext.Estados.Select(Estado => new SelectListItem()
                {
                    Text = Estado.Descripcion,
                    Value = Estado.IdEstado.ToString()

                }).ToList()
            };

            if (idTarea != 0)
            {

                oTareaVM.oTarea = _DBContext.Tareas.Find(idTarea);
            }


            return View(oTareaVM);
        }

        [HttpPost]
        public IActionResult Tarea_Detalle(TareaVM oTareaVM)
        {
            if (oTareaVM.oTarea.IdTarea == 0)
            {
                _DBContext.Tareas.Add(oTareaVM.oTarea);

            }
            else
            {
                _DBContext.Tareas.Update(oTareaVM.oTarea);
            }

            _DBContext.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Eliminar(Tarea oTarea)
        {
            _DBContext.Tareas.Remove(oTarea);
            _DBContext.SaveChanges();

            return RedirectToAction("Index", "Home");
        }



        public async Task<IActionResult> Salir()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("IniciarSesion", "Inicio");
        }
    }
}
