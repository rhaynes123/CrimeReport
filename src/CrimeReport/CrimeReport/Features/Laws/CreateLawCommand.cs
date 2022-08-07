using System;
using MediatR;

namespace CrimeReport.Features.Laws
{
    public record CreateLawCommand(Law Law): IRequest<Law>;
}

