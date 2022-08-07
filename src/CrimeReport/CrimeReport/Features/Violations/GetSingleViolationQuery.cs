using System;
using CrimeReport.Models.Enums;
using MediatR;

namespace CrimeReport.Features.Violations
{
    public record GetSingleViolationQuery(string Id, TypeOfCrime TypeOfCrime = default): IRequest<Violation>;
}

