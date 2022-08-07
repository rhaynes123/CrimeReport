using System;
using MediatR;

namespace CrimeReport.Features.Crimes
{
    public record GetSingleCrimeQuery(string Id): IRequest<Crime>;
}

