using System.Text.Json.Serialization;

namespace ProyectoFinal.Models.DTOs
{
    public class ProductoDTO
    {
        [JsonPropertyName("idProducto")]
        public int IdProducto { get; set; }

        [JsonPropertyName("nombre")]
        public string Nombre { get; set; } = null!;

        [JsonPropertyName("imagen")]
        public string? Imagen { get; set; }

        [JsonPropertyName("descripcion")]
        public string? Descripcion { get; set; }

        [JsonPropertyName("precio")]
        public decimal Precio { get; set; }

        [JsonPropertyName("idTipoProducto")]
        public int? IdTipoProducto { get; set; }

        [JsonPropertyName("nombreTipoProducto")]
        public string? NombreTipoProducto { get; set; }
    }
}
