using System;
using CrimeReport.Models.Enums;
using MediatR;

namespace CrimeReport.Features.Violations
{
    public record GetAllViolationsQuery(TypeOfCrime TypeOfCrime): IRequest<IEnumerable<Violation>>;
}

