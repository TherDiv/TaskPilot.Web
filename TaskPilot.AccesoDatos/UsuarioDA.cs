using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Claims;
using TaskPilot.Entidades;
using TaskPilot.Utiles;

namespace TaskPilot.AccesoDatos
{
    public class UsuarioDA
    {
        public Usuario Validar(string usuario, string clave)
        {
            Usuario obj = null;
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                cn.Open();
                string sql = @"SELECT *
                               FROM Usuario
                               where Usuario = @usuario
                               AND Clave = @clave";
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@usuario", usuario);
                cmd.Parameters.AddWithValue("@clave", clave);

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    obj = new Usuario();
                    obj.IdUsuario = Convert.ToInt32(dr["IdUsuario"]);
                    obj.NombreCompleto = dr["NombreCompleto"].ToString();
                    obj.Correo = dr["Correo"].ToString();
                    obj.UsuarioLogin = dr["Usuario"].ToString();
                    obj.Clave = dr["Clave"].ToString();
                    obj.Rol = dr["Rol"].ToString();
                }
            }
            return obj;
        }
        public List<Usuario> Listar()
        {
            List<Usuario> lista = new List<Usuario>();
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                cn.Open();
                string sql = "SELECT * FROM Usuario";
                SqlCommand cmd = new SqlCommand(sql, cn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Usuario obj = new Usuario();
                    obj.IdUsuario = Convert.ToInt32(dr["IdUsuario"]);
                    obj.NombreCompleto = dr["NombreCompleto"].ToString();
                    obj.Correo = dr["Correo"].ToString();
                    obj.UsuarioLogin = dr["Usuario"].ToString();
                    obj.Clave = dr["Clave"].ToString();
                    obj.Rol = dr["Rol"].ToString();

                    lista.Add(obj);
                }
            }
            return lista;
        }
        public void Insertar (Usuario obj)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                cn.Open();
                string sql = @"INSERT INTO Usuario
                ( 
                    NombreCompleto,
                    Correo,
                    Usuario,
                    Clave,
                    Rol
                )
                VALUES
                (
                    @Nombre,
                    @Correo,
                    @Usuario,
                    @Clave,
                    @Rol
                )";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@Nombre", obj.NombreCompleto);
                cmd.Parameters.AddWithValue("@Correo", obj.Correo);
                cmd.Parameters.AddWithValue("@Usuario", obj.UsuarioLogin);
                cmd.Parameters.AddWithValue("@Clave", obj.Clave);
                cmd.Parameters.AddWithValue("@Rol", obj.Rol);

                cmd.ExecuteNonQuery();
            }
        }
        public Usuario Obtener(int id)
        {
            Usuario obj = null;
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                cn.Open();
                string sql = @"SELECT *
                               FROM Usuario
                               WHERE IdUsuario=@id";
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    obj = new Usuario();

                    obj.IdUsuario = Convert.ToInt32(dr["IdUsuario"]);
                    obj.NombreCompleto = dr["NombreCompleto"].ToString();
                    obj.Correo = dr["Correo"].ToString();
                    obj.UsuarioLogin = dr["Usuario"].ToString();
                    obj.Clave = dr["Clave"].ToString();
                    obj.Rol = dr["Rol"].ToString();
                }
            }
            return obj;
        }
        public void Actualizar(Usuario obj)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                cn.Open();
                string sql = @"UPDATE Usuario
                               SET NombreCompleto=@Nombre,
                                   Correo=@Correo,
                                   Usuario=@Usuario,
                                   Clave=@Clave,
                                   Rol=@Rol
                                WHERE IdUsuario=@Id";
                SqlCommand cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@Nombre", obj.NombreCompleto);
                cmd.Parameters.AddWithValue("@Correo", obj.Correo);
                cmd.Parameters.AddWithValue("@Usuario", obj.UsuarioLogin);
                cmd.Parameters.AddWithValue("@Clave", obj.Clave);
                cmd.Parameters.AddWithValue("@Rol", obj.Rol);
                cmd.Parameters.AddWithValue("@Id", obj.IdUsuario);

                cmd.ExecuteNonQuery();
            }
        }
        public void Eliminar(int id)
        {
            using(SqlConnection cn = Conexion.ObtenerConexion())
            {
                cn.Open();
                string sql = @"DELETE FROM Usuario
                               WHERE IdUsuario=@id";
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
