using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using CrimeReport.Models.Interfaces;
using CrimeReport.Models.Enums;
using CrimeReport.Features.Laws;
using CrimeReport.Features.Violations;

namespace CrimeReport.Features.Crimes
{
    public class Crime: ICosmosEntity
    {
        public Crime()
        {

        }
        
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
        [Required, JsonPropertyName("laws")]
        public List<string> Laws { get; private set; } = new List<string>();

        public Crime AddViolation(Violation violation)
        {
            Laws.AddRange(violation.Laws);
            return this;
        }
        public Crime AddViolations(IEnumerable<Violation> violations)
        {
            foreach (var violation in violations)
            {
                Laws.AddRange(violation.Laws);
            }
            return this;
        }
    }
}

