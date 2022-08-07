using System;
using MediatR;

namespace CrimeReport.Features.Laws
{
    public record GetSingleLawQuery(string Id): IRequest<Law>;
}

