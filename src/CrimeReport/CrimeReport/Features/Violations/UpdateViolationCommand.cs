using System;
using MediatR;
namespace CrimeReport.Features.Violations
{
    public record UpdateViolationCommand(Violation Violation): IRequest<Violation>;
}

