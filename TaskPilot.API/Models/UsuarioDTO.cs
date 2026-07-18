namespace TaskPilot.API.Models
{
    public class UsuarioDTO
    {
        public int IdUsuario { get; set; }
        public string NombreCompleto { get; set; }
        public string Correo { get; set; }
        public string UsuarioLogin { get; set; }
        public string Rol { get; set; }
    }
}