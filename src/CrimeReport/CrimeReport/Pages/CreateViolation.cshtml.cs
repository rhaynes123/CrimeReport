#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CrimeReport.Data;
using CrimeReport.Features.Violations;
using CrimeReport.Features.Laws;
using MediatR;

namespace CrimeReport.Pages
{
    public class CreateViolationModel : PageModel
    {
        private readonly IMediator _mediator;

        public CreateViolationModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IActionResult OnGetAsync()
        {
            return Page();
        }

        [BindProperty]
        public CreateViolationRequest Violation { get; set; }
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Violation violation = new(Violation.TypeOfCrime);
            var violationResponse = await _mediator.Send(new CreateViolationCommand(Violation: violation));
            if (violationResponse is null)
            {
                ModelState.AddModelError(string.Empty, "Violation was not Created");
                return Page();
            }

            return RedirectToPage("./ViolationDetails", new {id = violation.Id});
        }
    }
}
