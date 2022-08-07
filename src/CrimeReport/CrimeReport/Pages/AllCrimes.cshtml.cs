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
    public class AllCrimesModel : PageModel
    {
        private readonly IMediator _mediator;

        public AllCrimesModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IList<Crime> Crimes { get;set; }

        public async Task OnGetAsync()
        {
            Crimes = await _mediator.Send(new GetAllCrimesQuery());
        }
    }
}
