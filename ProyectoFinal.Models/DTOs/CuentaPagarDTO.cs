using System.Text.Json.Serialization;

namespace ProyectoFinal.Models.DTOs
{
    public class CuentaPagarDTO
    {
        [JsonPropertyName("idCuentaPagar")]
        public int IdCuentaPagar { get; set; }

        [JsonPropertyName("motivo")]
        public string Motivo { get; set; } = null!;

        [JsonPropertyName("fechaCuentaPagar")]
        public DateOnly FechaCuentaPagar { get; set; }

        [JsonPropertyName("descripcion")]
        public string? Descripcion { get; set; }

        [JsonPropertyName("monto")]
        public decimal Monto { get; set; }

        [JsonPropertyName("plazoPagar")]
        public string? PlazoPagar { get; set; }
    }
}