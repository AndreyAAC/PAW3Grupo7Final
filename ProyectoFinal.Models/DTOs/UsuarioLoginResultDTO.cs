using System.Text.Json.Serialization;

namespace ProyectoFinal.Models.DTOs
{
    public class UsuarioLoginResultDTO
    {
        [JsonPropertyName("idUsuario")]
        public int IdUsuario { get; set; }

        [JsonPropertyName("nombre")]
        public string Nombre { get; set; } = null!;

        [JsonPropertyName("correo")]
        public string Correo { get; set; } = null!;

        [JsonPropertyName("roleId")]
        public int RoleId { get; set; }
    }
}