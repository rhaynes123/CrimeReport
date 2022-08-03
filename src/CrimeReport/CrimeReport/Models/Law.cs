using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using CrimeReport.Models.Interfaces;
namespace CrimeReport.Models
{
    public class Law : ICosmosEntity
    {
        public Law()
        {
            
            PartionKey = Id;
        }
        [Required, JsonPropertyName("id")]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [JsonPropertyName("partionKey")]
        public string PartionKey { get; set; }
        [Required, JsonPropertyName("description")]
        public string Description { get; set; } = default!;
    }
}

