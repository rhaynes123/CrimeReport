using System;
using CrimeReport.Models.Enums;

namespace CrimeReport.Features.Violations
{
    public record CreateViolationRequest(string Id, TypeOfCrime TypeOfCrime);
}

