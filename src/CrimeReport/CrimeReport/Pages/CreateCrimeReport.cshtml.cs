#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CrimeReport.Data;
using CrimeReport.Features.Crimes;
using MediatR;
using CrimeReport.Features.Violations;

namespace CrimeReport.Pages
{
    public class CreateCrimeReportModel : PageModel
    {
        private readonly IMediator _mediator;

        public CreateCrimeReportModel( IMediator mediator)
        {
            _mediator = mediator;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Crime Crime { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            IEnumerable<Violation> violations = await _mediator.Send(new GetAllViolationsQuery(Crime.TypeOfCrime));
            if(violations is null || !violations.Any())
            {
                return BadRequest();
            }

            Crime crime = await _mediator.Send(new CreateCrimeReportCommand(Crime.AddViolations(violations)));
            if (crime is null || !crime.Laws.Any())
            {
                ModelState.AddModelError(string.Empty, "Report Could Not be Created");
                return Page();
            }
            

            return RedirectToPage("./ReportDetails", new { id = Crime.Id });
        }
    }
}
