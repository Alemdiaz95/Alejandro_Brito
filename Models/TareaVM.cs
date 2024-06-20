using Microsoft.AspNetCore.Mvc.Rendering;

namespace Alejandro_Brito.Models
{
    public class TareaVM
    {
        public Tarea oTarea { get; set; }

        public List<SelectListItem> oListaEstado { get; set; }

    }
}
