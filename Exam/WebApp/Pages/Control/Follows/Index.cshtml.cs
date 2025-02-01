using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAL;
using Domain;

namespace WebApp.Pages_Control_Follows
{
    public class IndexModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public IndexModel(DAL.AppDbContext context)
        {
            _context = context;
        }

        public IList<Follow> Follow { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Follow = await _context.Follows
                .Include(f => f.Followed)
                .Include(f => f.Follower).ToListAsync();
        }
    }
}
