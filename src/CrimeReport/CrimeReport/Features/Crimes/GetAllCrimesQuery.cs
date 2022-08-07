using System;
using MediatR;

namespace CrimeReport.Features.Crimes
{
    public record GetAllCrimesQuery(): IRequest<IList<Crime>>;
}

