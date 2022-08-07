#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CrimeReport.Data;
using CrimeReport.Models;
using CrimeReport.Features.Laws;
using MediatR;

namespace CrimeReport.Pages
{
    public class CreateLawModel : PageModel
    {
        private readonly IMediator _mediator;

        public CreateLawModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Law Law { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Law savedLaw = await _mediator.Send(new CreateLawCommand(Law: Law));
            if(savedLaw is null || savedLaw == default)
            {
                ModelState.AddModelError(string.Empty, "Law Could Not be Saved");
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
