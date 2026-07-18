using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TaskPilot.Entidades;
using TaskPilot.Utiles;

namespace TaskPilot.AccesoDatos
{
    public class DashboardDA
    {
        public List<Tarea> ObtenerTareas()
        {
            List<Tarea> lista = new List<Tarea>();

            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                cn.Open();

                string sql = @"
                    SELECT
                        T.IdTarea,
                        T.Titulo,
                        T.FechaFin,
                        T.Estado,
                        T.Prioridad,
                        STRING_AGG(U.NombreCompleto, ', ') AS NombreUsuario
                    FROM Tarea T
                    INNER JOIN TareaUsuario TU ON T.IdTarea = TU.IdTarea
                    INNER JOIN Usuario U ON TU.IdUsuario = U.IdUsuario
                    GROUP BY
                        T.IdTarea, T.Titulo, T.FechaFin,
                        T.Estado, T.Prioridad";

                SqlCommand cmd = new SqlCommand(sql, cn);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Tarea obj = new Tarea();
                    obj.IdTarea = Convert.ToInt32(dr["IdTarea"]);
                    obj.Titulo = dr["Titulo"].ToString();
                    obj.FechaFin = Convert.ToDateTime(dr["FechaFin"]);
                    obj.Estado = dr["Estado"].ToString();
                    obj.Prioridad = dr["Prioridad"].ToString();
                    obj.NombreUsuario = dr["NombreUsuario"].ToString();
                    lista.Add(obj);
                }
            }

            return lista;
        }
    }
}