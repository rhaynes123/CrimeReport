using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using CrimeReport.Models.Enums;
using CrimeReport.Models.Interfaces;

namespace CrimeReport.Models
{
    public class Violation: ICosmosEntity
    {
        public Violation(TypeOfCrime typeOfCrime)
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
        [Required, JsonPropertyName("typeOfCrime")]
        public TypeOfCrime TypeOfCrime { get; set; }
        [Required, JsonPropertyName("lawsBroken")]
        public IList<Law> LawsBroken { get; set; } = new List<Law>();
    }
}

