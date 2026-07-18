using System;
using System.Collections.Generic;
using TaskPilot.AccesoDatos;
using TaskPilot.Entidades;

namespace TaskPilot.LogicaNegocio
{
    public class UsuarioLN
    {
        UsuarioDA da = new UsuarioDA();
        public Usuario Validar (string usuario, string clave)
        {
            return da.Validar (usuario, clave);
        }
        public List <Usuario> Listar()
        {
            return da.Listar();
        }
        public void Insertar (Usuario obj)
        {
            da.Insertar(obj);
        }
        public Usuario Obtener(int id)
        {
            return da.Obtener(id);
        }
        public void Actualizar(Usuario obj)
        {
            da.Actualizar(obj);
        }
        public void Eliminar(int id)
        {
            da.Eliminar(id);
        }
    }
}
