using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TaskPilot.Entidades;
using TaskPilot.Utiles;

namespace TaskPilot.AccesoDatos
{
    public class TareaDA
    {
        public List<Tarea> Listar()
        {
            List<Tarea> lista = new List<Tarea>();

            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                cn.Open();

                string sql = @"
                SELECT
                    T.IdTarea,
                    T.Titulo,
                    T.Descripcion,
                    T.FechaInicio,
                    T.FechaFin,
                    T.Estado,
                    T.Prioridad,
                    STRING_AGG(U.NombreCompleto, ', ') AS NombreUsuario

                FROM Tarea T

                INNER JOIN TareaUsuario TU
                    ON T.IdTarea = TU.IdTarea

                INNER JOIN Usuario U
                    ON TU.IdUsuario = U.IdUsuario

                GROUP BY
                    T.IdTarea,
                    T.Titulo,
                    T.Descripcion,
                    T.FechaInicio,
                    T.FechaFin,
                    T.Estado,
                    T.Prioridad";

                SqlCommand cmd = new SqlCommand(sql, cn);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Tarea obj = new Tarea();

                    obj.IdTarea = Convert.ToInt32(dr["IdTarea"]);
                    obj.Titulo = dr["Titulo"].ToString();
                    obj.Descripcion = dr["Descripcion"].ToString();
                    obj.FechaInicio = Convert.ToDateTime(dr["FechaInicio"]);
                    obj.FechaFin = Convert.ToDateTime(dr["FechaFin"]);
                    obj.Estado = dr["Estado"].ToString();
                    obj.Prioridad = dr["Prioridad"].ToString();
                    obj.NombreUsuario = dr["NombreUsuario"].ToString();

                    lista.Add(obj);
                }
            }

            return lista;
        }
        public Tarea Obtener(int id)
        {
            Tarea obj = null;

            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                cn.Open();

                string sql = @"
                    SELECT
                        T.IdTarea,
                        T.Titulo,
                        T.Descripcion,
                        T.FechaInicio,
                        T.FechaFin,
                        T.Estado,
                        T.Prioridad,
                        STRING_AGG(U.NombreCompleto, ', ') AS NombreUsuario

                    FROM Tarea T

                    INNER JOIN TareaUsuario TU
                        ON T.IdTarea = TU.IdTarea

                    INNER JOIN Usuario U
                        ON TU.IdUsuario = U.IdUsuario

                    WHERE T.IdTarea=@id

                    GROUP BY
                        T.IdTarea,
                        T.Titulo,
                        T.Descripcion,
                        T.FechaInicio,
                        T.FechaFin,
                        T.Estado,
                        T.Prioridad";

                SqlCommand cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@id", id);

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    obj = new Tarea();

                    obj.IdTarea = Convert.ToInt32(dr["IdTarea"]);
                    obj.Titulo = dr["Titulo"].ToString();
                    obj.Descripcion = dr["Descripcion"].ToString();
                    obj.FechaInicio = Convert.ToDateTime(dr["FechaInicio"]);
                    obj.FechaFin = Convert.ToDateTime(dr["FechaFin"]);
                    obj.Estado = dr["Estado"].ToString();
                    obj.Prioridad = dr["Prioridad"].ToString();
                    obj.NombreUsuario = dr["NombreUsuario"].ToString();
                }

                dr.Close();

                if (obj != null)
                {
                    obj.UsuariosSeleccionados = new List<int>();

                    string sqlUsuarios = @"
                        SELECT IdUsuario
                        FROM TareaUsuario
                        WHERE IdTarea=@id";

                    SqlCommand cmdUsuarios =
                        new SqlCommand(sqlUsuarios, cn);

                    cmdUsuarios.Parameters.AddWithValue("@id", id);

                    SqlDataReader drUsuarios =
                        cmdUsuarios.ExecuteReader();

                    while (drUsuarios.Read())
                    {
                        obj.UsuariosSeleccionados.Add(
                            Convert.ToInt32(drUsuarios["IdUsuario"]));
                    }

                    drUsuarios.Close();
                }
            }

            return obj;
        }
        public void Insertar(Tarea obj)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                cn.Open();

                string sql = @"
                INSERT INTO Tarea
                (
                Titulo,
                Descripcion,
                FechaInicio,
                FechaFin,
                Estado,
                Prioridad
                )

                VALUES
                (
                @Titulo,
                @Descripcion,
                @FechaInicio,
                @FechaFin,
                @Estado,
                @Prioridad
                );

                SELECT SCOPE_IDENTITY();";

                SqlCommand cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@Titulo", obj.Titulo);
                cmd.Parameters.AddWithValue("@Descripcion", obj.Descripcion);
                cmd.Parameters.AddWithValue("@FechaInicio", obj.FechaInicio);
                cmd.Parameters.AddWithValue("@FechaFin", obj.FechaFin);
                cmd.Parameters.AddWithValue("@Estado", obj.Estado);
                cmd.Parameters.AddWithValue("@Prioridad", obj.Prioridad);

                int idTarea =
                    Convert.ToInt32(
                        cmd.ExecuteScalar());
                obj.IdTarea = idTarea;

                if (obj.UsuariosSeleccionados != null)
                {
                    foreach (int idUsuario in obj.UsuariosSeleccionados)
                    {
                        string sqlRelacion = @"
                        INSERT INTO TareaUsuario
                        (
                            IdTarea,
                            IdUsuario
                        )

                        VALUES
                        (
                            @IdTarea,
                            @IdUsuario
                        )";

                        SqlCommand cmdRelacion =
                            new SqlCommand(sqlRelacion, cn);

                        cmdRelacion.Parameters.AddWithValue("@IdTarea", idTarea);

                        cmdRelacion.Parameters.AddWithValue("@IdUsuario", idUsuario);

                        cmdRelacion.ExecuteNonQuery();
                    }
                }
            }
        }
        public void Actualizar(Tarea obj)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                cn.Open();

                string sql = @"
                    UPDATE Tarea
                    SET
                    Titulo=@Titulo,
                    Descripcion=@Descripcion,
                    FechaInicio=@FechaInicio,
                    FechaFin=@FechaFin,
                    Estado=@Estado,
                    Prioridad=@Prioridad
                    WHERE IdTarea=@Id";

                SqlCommand cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@Titulo", obj.Titulo);
                cmd.Parameters.AddWithValue("@Descripcion", obj.Descripcion);
                cmd.Parameters.AddWithValue("@FechaInicio", obj.FechaInicio);
                cmd.Parameters.AddWithValue("@FechaFin", obj.FechaFin);
                cmd.Parameters.AddWithValue("@Estado", obj.Estado);
                cmd.Parameters.AddWithValue("@Prioridad", obj.Prioridad);
                cmd.Parameters.AddWithValue("@Id", obj.IdTarea);

                cmd.ExecuteNonQuery();

                string sqlEliminar = @"
                    DELETE FROM TareaUsuario
                    WHERE IdTarea=@Id";

                SqlCommand cmdEliminar =
                    new SqlCommand(sqlEliminar, cn);

                cmdEliminar.Parameters.AddWithValue("@Id", obj.IdTarea);

                cmdEliminar.ExecuteNonQuery();

                if (obj.UsuariosSeleccionados != null)
                {
                    foreach (int idUsuario in obj.UsuariosSeleccionados)
                    {
                        string sqlRelacion = @"
                            INSERT INTO TareaUsuario
                            (
                                IdTarea,
                                IdUsuario
                            )

                            VALUES
                            (
                                @IdTarea,
                                @IdUsuario
                            )";

                        SqlCommand cmdRelacion =
                            new SqlCommand(sqlRelacion, cn);

                        cmdRelacion.Parameters.AddWithValue(
                            "@IdTarea",
                            obj.IdTarea);

                        cmdRelacion.Parameters.AddWithValue(
                            "@IdUsuario",
                            idUsuario);

                        cmdRelacion.ExecuteNonQuery();
                    }
                }
            }
        }
        public void Eliminar(int id)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                cn.Open();

                string sql = @"DELETE FROM TareaUsuario
                            WHERE IdTarea=@id; 
                            DELETE FROM Tarea
                            WHERE IdTarea=@id;";

                SqlCommand cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
            }
        }
    }
}