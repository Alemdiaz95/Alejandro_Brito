using Alejandro_Brito.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Alejandro_Brito.Servicios.Contrato;

namespace Alejandro_Brito.Servicios.Implementacion
{
    public class UsuarioService : IUsuarioService
    {
        private readonly AlejandroBritoContext _dbContext;

        public UsuarioService(AlejandroBritoContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Usuario> GetUsuario(string cedula, string clave)
        {
            Usuario usuario_encontrado = await _dbContext.Usuarios.Where(u => u.Cedula == cedula && u.Password == clave)
                 .FirstOrDefaultAsync();

            return usuario_encontrado;
        }

        public async Task<Usuario> GetUsuarioId(string cedula)
        {
            Usuario usuario_registrado = await _dbContext.Usuarios.Where(u => u.Cedula == cedula)
                 .FirstOrDefaultAsync();

            return usuario_registrado;
        }
        public async Task<Usuario> SaveUsuario(Usuario modelo)
        {
            _dbContext.Usuarios.Add(modelo);
            await _dbContext.SaveChangesAsync();
            return modelo;
        }
    }
}
