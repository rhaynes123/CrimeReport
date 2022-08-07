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

namespace CrimeReport.Pages
{
    public class CreateLawModel : PageModel
    {
        private readonly CrimeReport.Data.CrimeDbContext _context;

        public CreateLawModel(CrimeReport.Data.CrimeDbContext context)
        {
            _context = context;
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

            _context.Laws.Add(Law);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
