using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using CrimeReport.Models.Interfaces;
using CrimeReport.Models.Enums;
namespace CrimeReport.Models
{
    public class Crime: ICosmosEntity
    {
        
        public Crime(TypeOfCrime typeOfCrime)
        {
            if (typeOfCrime == TypeOfCrime.NA)
            {
                throw new ArgumentException("No Type For Crime Was Provided");
            }
            Id = Guid.NewGuid().ToString();
            TypeOfCrime = typeOfCrime;
            PartionKey = nameof(typeOfCrime);
        }
        [Required, JsonPropertyName("id")]
        public string Id { get; set; }
        [Required, JsonPropertyName("partionKey")]
        public string PartionKey { get; set; }
        [Required, JsonPropertyName("summary")]
        public string Summary { get; set; } = default!;
        [Required, JsonPropertyName("typeOfCrime")]
        public TypeOfCrime TypeOfCrime { get; set; }
        [Required, JsonPropertyName("lawsBroken")]
        public IList<Law> LawsBroken { get; set; } = new List<Law>();
    }
}

