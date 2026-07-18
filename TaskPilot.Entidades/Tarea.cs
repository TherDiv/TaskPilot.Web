using System;
using System.Collections.Generic;

namespace TaskPilot.Entidades
{
    public class Tarea
    {
        public int IdTarea { get; set; }

        public string Titulo { get; set; }

        public string Descripcion { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime FechaFin { get; set; }

        public string Estado { get; set; }

        public string Prioridad { get; set; }

        // Se mantiene para compatibilidad
        public int IdUsuario { get; set; }

        // Lista de usuarios seleccionados
        public List<int> UsuariosSeleccionados { get; set; }

        // Para mostrar los nombres
        public string NombreUsuario { get; set; }

        public Tarea()
        {
            UsuariosSeleccionados = new List<int>();
        }
    }
}