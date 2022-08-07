using System;
using MediatR;
namespace CrimeReport.Features.Violations
{
    public record CreateViolationCommand(Violation Violation): IRequest<Violation>;
}

