using System.Text.Json.Serialization;

namespace ProyectoFinal.Models.DTOs
{
    public class TipoProductoDTO
    {
        [JsonPropertyName("idTipoProducto")]
        public int IdTipoProducto { get; set; }

        [JsonPropertyName("nombreTipo")]
        public string NombreTipo { get; set; } = null!;
    }
}
