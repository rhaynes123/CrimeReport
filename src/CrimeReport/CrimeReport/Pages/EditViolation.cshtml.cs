#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CrimeReport.Data;
using CrimeReport.Features.Violations;
using MediatR;
using CrimeReport.Features.Laws;

namespace CrimeReport.Pages
{
    public class EditViolationModel : PageModel
    {
        private readonly CrimeReport.Data.CrimeDbContext _context;
        private readonly IMediator _mediator;

        public EditViolationModel(CrimeReport.Data.CrimeDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        [BindProperty]
        public Violation Violation { get; set; }
       
        public SelectList Laws { get; set; }
        [BindProperty]
        public string LawId { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Violation = await _mediator.Send(new GetSingleViolationQuery(Id: id)) ;
            var laws = await _mediator.Send(new GetAllLawsQuery());
            Laws = new SelectList(items: laws, dataTextField: nameof(Law.Description), dataValueField: nameof(Law.Id));

            if (Violation == null)
            {
                return NotFound();
            }
            return Page();
        }


        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Law law = new();
            if(!string.IsNullOrWhiteSpace(LawId))
            {
                law = await _mediator.Send(new GetSingleLawQuery(LawId));
            }
            if (law is not null || law != default)
            {
                Violation.Laws.Add(law.Description);
            }
            var violation = await _mediator.Send(new UpdateViolationCommand(Violation: Violation));
            if(violation is null || violation == default!)
            {
                ModelState.AddModelError(string.Empty, "Edit Failed");
                return Page();
            }

            return RedirectToPage("./EditViolation", new { id = Violation.Id});
        }

    }
}
