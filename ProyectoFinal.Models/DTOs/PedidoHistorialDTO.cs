using System.Text.Json.Serialization;

namespace ProyectoFinal.Models.DTOs
{
    public class PedidoHistorialDTO
    {
        [JsonPropertyName("idPedido")]
        public int IdPedido { get; set; }

        [JsonPropertyName("fechaDeInicio")]
        public DateOnly FechaDeInicio { get; set; }

        [JsonPropertyName("fechaDeEntrega")]
        public DateOnly? FechaDeEntrega { get; set; }

        [JsonPropertyName("idEstadoPedido")]
        public int IdEstadoPedido { get; set; }

        [JsonPropertyName("nombreEstado")]
        public string NombreEstado { get; set; } = null!;

        [JsonPropertyName("total")]
        public decimal Total { get; set; }

        [JsonPropertyName("cantidadProductos")]
        public int CantidadProductos { get; set; }
    }
}
