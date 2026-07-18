using System;
using System.Collections.Generic;
using System.Linq;
using TaskPilot.AccesoDatos;
using TaskPilot.Entidades;

namespace TaskPilot.LogicaNegocio
{
    public class DashboardLN
    {
        DashboardDA da = new DashboardDA();

        public List<Tarea> ObtenerTareas()
        {
            var tareas = da.ObtenerTareas();

            // Regla automática: recalcula prioridad según días restantes
            foreach (var t in tareas)
            {
                int dias = (t.FechaFin - DateTime.Today).Days;

                if (dias <= 2) t.Prioridad = "Alta";
                else if (dias <= 7) t.Prioridad = "Media";
                else t.Prioridad = "Baja";
            }

            return tareas;
        }

        public int ContarPorEstado(List<Tarea> tareas, string estado)
        {
            return tareas.Count(t => t.Estado == estado);
        }

        public List<Tarea> TareasCriticas(List<Tarea> tareas)
        {
            return tareas
                .Where(t => t.Prioridad == "Alta" && t.Estado != "Completada")
                .ToList();
        }
    }
}