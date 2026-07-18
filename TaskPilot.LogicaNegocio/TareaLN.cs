using System.Collections.Generic;
using TaskPilot.AccesoDatos;
using TaskPilot.Entidades;

namespace TaskPilot.LogicaNegocio
{
    public class TareaLN
    {
        TareaDA da = new TareaDA();
        public List<Tarea> Listar()
        {
            return da.Listar();
        }

        public Tarea Obtener(int id)
        {
            return da.Obtener(id);
        }

        public void Insertar(Tarea obj)
        {
            da.Insertar(obj);
        }
        public void Actualizar(Tarea obj)
        {
            da.Actualizar(obj);
        }

        public void Eliminar(int id)
        {
            da.Eliminar(id);
        }
    }
}
