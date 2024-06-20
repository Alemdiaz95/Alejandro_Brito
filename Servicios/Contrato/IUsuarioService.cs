using Alejandro_Brito.Models;
using Microsoft.EntityFrameworkCore;

namespace Alejandro_Brito.Servicios.Contrato
{
    public interface IUsuarioService
    {
        Task<Usuario> GetUsuario(string cedula, string clave);
        Task<Usuario> GetUsuarioId(string cedula);
        Task<Usuario> SaveUsuario(Usuario modelo);

    }
}
