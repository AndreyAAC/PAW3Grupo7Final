using System.Text.Json.Serialization;

namespace ProyectoFinal.Models.DTOs
{
    public class UsuarioRegisterDTO
    {
        [JsonPropertyName("nombre")]
        public string Nombre { get; set; } = null!;

        [JsonPropertyName("apellido")]
        public string Apellido { get; set; } = null!;

        [JsonPropertyName("correo")]
        public string Correo { get; set; } = null!;

        [JsonPropertyName("cedula")]
        public int Cedula { get; set; }

        [JsonPropertyName("telefono")]
        public string? Telefono { get; set; }

        [JsonPropertyName("contrasenia")]
        public string Contrasenia { get; set; } = null!;

        [JsonPropertyName("confirmarContrasenia")]
        public string ConfirmarContrasenia { get; set; } = null!;
    }
}