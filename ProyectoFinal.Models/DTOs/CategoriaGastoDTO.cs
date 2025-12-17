using System.Text.Json.Serialization;

namespace ProyectoFinal.Models.DTOs
{
    public class CategoriaGastoDTO
    {
        [JsonPropertyName("idCategoriaGasto")]
        public int IdCategoriaGasto { get; set; }

        [JsonPropertyName("nombreCategoria")]
        public string NombreCategoria { get; set; } = null!;
    }
}