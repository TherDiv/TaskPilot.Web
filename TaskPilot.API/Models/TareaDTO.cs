using System;
using System.Collections.Generic;

namespace TaskPilot.API.Models
{
    public class TareaDTO
    {
        public int IdTarea { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Estado { get; set; }
        public string Prioridad { get; set; }
        public string NombreUsuario { get; set; }
        public List<int> UsuariosSeleccionados { get; set; }
    }
}