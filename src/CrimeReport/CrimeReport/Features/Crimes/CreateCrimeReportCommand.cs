using System;
using MediatR;

namespace CrimeReport.Features.Crimes
{
    public record CreateCrimeReportCommand(Crime Crime): IRequest<Crime>;
}

