using System.Text.Json.Serialization;

namespace ProyectoFinal.Models.DTOs
{
    public class GastoDTO
    {
        [JsonPropertyName("idGasto")]
        public int IdGasto { get; set; }

        [JsonPropertyName("motivo")]
        public string Motivo { get; set; } = null!;

        [JsonPropertyName("fechaGasto")]
        public DateOnly FechaGasto { get; set; }

        [JsonPropertyName("descripcion")]
        public string? Descripcion { get; set; }

        [JsonPropertyName("monto")]
        public decimal Monto { get; set; }

        [JsonPropertyName("idCategoriaGasto")]
        public int? IdCategoriaGasto { get; set; }

        [JsonPropertyName("nombreCategoria")]
        public string? NombreCategoria { get; set; }
    }
}