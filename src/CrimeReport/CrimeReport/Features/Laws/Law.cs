using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using CrimeReport.Models.Interfaces;
using CrimeReport.Models;
namespace CrimeReport.Features.Laws
{
    public class Law : ICosmosEntity
    {
        public Law()
        {
            PartionKey = Id;
        }
        [Required, JsonPropertyName("id")]
        public string Id { get; set; } = new Identifier().New();
        [JsonPropertyName("partionKey")]
        public string PartionKey { get; set; }
        [Required, JsonPropertyName("description")]
        public string Description { get; set; } = default!;
    }
}

