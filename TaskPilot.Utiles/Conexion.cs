using System;
using System.Configuration;
using System.Data.SqlClient;

namespace TaskPilot.Utiles
{
    public class Conexion
    {
        public static SqlConnection ObtenerConexion()
        {
            return new SqlConnection(
                ConfigurationManager
                .ConnectionStrings["cnTaskPilot"]
                .ConnectionString);
        }
    }
}

