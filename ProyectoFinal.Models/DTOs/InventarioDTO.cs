using System.Text.Json.Serialization;

namespace ProyectoFinal.Models.DTOs
{
    public class InventarioDTO
    {
        [JsonPropertyName("idInventario")]
        public int IdInventario { get; set; }

        [JsonPropertyName("idProducto")]
        public int IdProducto { get; set; }

        [JsonPropertyName("nombreProducto")]
        public string? NombreProducto { get; set; }

        [JsonPropertyName("cantidad")]
        public int Cantidad { get; set; }
    }
}
