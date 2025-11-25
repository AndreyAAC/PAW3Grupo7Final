using System.Text.Json.Serialization;

namespace ProyectoFinal.Models.DTOs
{
    public class UsuarioLoginDTO
    {
        [JsonPropertyName("correo")]
        public string Correo { get; set; } = null!;

        [JsonPropertyName("contrasenia")]
        public string Contrasenia { get; set; } = null!;
    }
}