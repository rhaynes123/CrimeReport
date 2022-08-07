using System;
using CrimeReport.Models;
using MediatR;

namespace CrimeReport.Features.Laws
{
    public record GetAllLawsQuery(): IRequest<IList<Law>>;
}

