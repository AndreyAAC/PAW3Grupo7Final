using System.Text.Json.Serialization;

namespace ProyectoFinal.Models.DTOs
{
    public class CitaHistorialDTO
    {
        [JsonPropertyName("idCita")]
        public int IdCita { get; set; }

        [JsonPropertyName("idCliente")]
        public int IdCliente { get; set; }

        [JsonPropertyName("nombreCliente")]
        public string NombreCliente { get; set; } = null!;

        [JsonPropertyName("motivo")]
        public string Motivo { get; set; } = null!;

        [JsonPropertyName("idProducto")]
        public int? IdProducto { get; set; }

        [JsonPropertyName("nombreProducto")]
        public string? NombreProducto { get; set; }

        [JsonPropertyName("detalle")]
        public string? Detalle { get; set; }

        [JsonPropertyName("fechaCita")]
        public DateOnly FechaCita { get; set; }

        [JsonPropertyName("horaCita")]
        public TimeOnly HoraCita { get; set; }
    }
}
