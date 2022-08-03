#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CrimeReport.Data;
using CrimeReport.Models;

namespace CrimeReport.Pages
{
    public class AllLawsModel : PageModel
    {
        private readonly CrimeReport.Data.CrimeDbContext _context;

        public AllLawsModel(CrimeReport.Data.CrimeDbContext context)
        {
            _context = context;
        }

        public IList<Law> Law { get;set; }

        public async Task OnGetAsync()
        {
            Law = await _context.Laws.ToListAsync();
        }
    }
}
