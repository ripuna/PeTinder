using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DAL;
using Domain;

namespace WebApp.Pages_Control_Likes
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
        ViewData["LikedId"] = new SelectList(_context.Pets, "Id", "Name");
        ViewData["LikerId"] = new SelectList(_context.Pets, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Like Like { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Likes.Add(Like);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
