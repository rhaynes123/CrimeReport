#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CrimeReport.Data;
using CrimeReport.Features.Violations;
using MediatR;

namespace CrimeReport.Pages
{
    public class ViolationDetailsModel : PageModel
    {
        private readonly IMediator _mediator;

        public ViolationDetailsModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Violation Violation { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Violation = await _mediator.Send(new GetSingleViolationQuery(id));

            if (Violation == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
