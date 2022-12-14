#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CrimeReport.Data;
using CrimeReport.Features.Laws;
using MediatR;

namespace CrimeReport.Pages
{
    public class AllLawsModel : PageModel
    {
        private readonly IMediator _mediator;

        public AllLawsModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IList<Law> Laws { get;set; }

        public async Task OnGetAsync()
        {
            Laws = await _mediator.Send(new GetAllLawsQuery());
        }
    }
}
