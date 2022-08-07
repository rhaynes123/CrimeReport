#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CrimeReport.Data;
using CrimeReport.Features.Crimes;
using MediatR;

namespace CrimeReport.Pages
{
    public class ReportDetailsModel : PageModel
    {
        private readonly IMediator _mediator;

        public ReportDetailsModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Crime Crime { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Crime = await _mediator.Send(new GetSingleCrimeQuery(id));

            if (Crime == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
