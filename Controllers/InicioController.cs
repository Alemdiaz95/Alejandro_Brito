using Microsoft.AspNetCore.Mvc;

using Alejandro_Brito.Models;
using Alejandro_Brito.Recursos;
using Alejandro_Brito.Servicios.Contrato;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Numerics;
namespace Alejandro_Brito.Controllers
{
    public class InicioController : Controller
    {
        private readonly IUsuarioService _usuarioServicio;
        public InicioController(IUsuarioService usuarioServicio)
        {
            _usuarioServicio = usuarioServicio;
        }

        public IActionResult Registrarse()
        {
            if(User.Identity!.IsAuthenticated) return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registrarse(Usuario modelo, string cedula)
        {

            //Usuario usuario_registrado = await _usuarioServicio.GetUsuarioId(cedula);
            //var _usuario = !String.IsNullOrWhiteSpace(usuario_registrado.Cedula);
            //if (_usuario != false )
            //{
            //    if (usuario_registrado.Cedula != null)
            //     ViewData["Mensaje"] = "Usted ya está registrado";
            //    return View();
            //}

            modelo.Password = Utilidades.EncriptarClave(modelo.Password);

            Usuario usuario_creado = await _usuarioServicio.SaveUsuario(modelo);

            if (usuario_creado.IdUsuario > 0)
                return RedirectToAction("IniciarSesion", "Inicio");

            ViewData["Mensaje"] = "No se pudo crear el usuario";
            return View();
        }

        public IActionResult IniciarSesion()
        {
            if (User.Identity!.IsAuthenticated) return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IniciarSesion(string cedula, string clave)
        {

            Usuario usuario_encontrado = await _usuarioServicio.GetUsuario(cedula, Utilidades.EncriptarClave(clave));

            if (usuario_encontrado == null)
            {
                ViewData["Mensaje"] = "No se encontraron coincidencias";
                return View();
            }

            List<Claim> claims = new List<Claim>() {
                new Claim(ClaimTypes.Name, usuario_encontrado.Nombre),
                new Claim(ClaimTypes.Upn, usuario_encontrado.Apellido)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                properties
                );

            return RedirectToAction("Index", "Home");
        }
    }
}
