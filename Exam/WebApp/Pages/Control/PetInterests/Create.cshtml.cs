using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DAL;
using Domain;

namespace WebApp.Pages_Control_PetInterests
{
    public class CreateModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public CreateModel(DAL.AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["InterestId"] = new SelectList(_context.Interests, "Id", "PetInterest");
        ViewData["PetId"] = new SelectList(_context.Pets, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public PetInterest PetInterest { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.PetInterests.Add(PetInterest);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
