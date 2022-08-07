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

namespace CrimeReport.Pages
{
    public class AllViolationsModel : PageModel
    {
        private readonly CrimeReport.Data.CrimeDbContext _context;

        public AllViolationsModel(CrimeReport.Data.CrimeDbContext context)
        {
            _context = context;
        }

        public IList<Violation> Violation { get;set; }

        public async Task OnGetAsync()
        {
            Violation = await _context.Violations.ToListAsync();
        }
    }
}
