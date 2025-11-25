using System.Text.Json.Serialization;

namespace ProyectoFinal.Models.DTOs
{
    public class UsuarioAdminDTO
    {
        [JsonPropertyName("idUsuario")]
        public int IdUsuario { get; set; }

        [JsonPropertyName("nombreUsuario")]
        public string NombreUsuario { get; set; } = null!;

        [JsonPropertyName("nombreApellido")]
        public string NombreApellido { get; set; } = null!;

        [JsonPropertyName("correo")]
        public string Correo { get; set; } = null!;

        [JsonPropertyName("cedula")]
        public int Cedula { get; set; }

        [JsonPropertyName("telefono")]
        public string? Telefono { get; set; }

        [JsonPropertyName("role")]
        public int Role { get; set; }

        [JsonPropertyName("nombreRole")]
        public string? NombreRole { get; set; }
    }
}