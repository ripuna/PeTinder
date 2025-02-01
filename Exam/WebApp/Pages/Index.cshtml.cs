using DAL;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages;

public class IndexModel : PageModel
{
    private readonly AppDbContext _context;
    private readonly ILogger<IndexModel> _logger;
    public IndexModel(AppDbContext context, ILogger<IndexModel> logger)
    {
        _context = context;
        _logger = logger;
    }

    [BindProperty]
    public string MyPetId { get; set; } = default!;

    public Domain.Pet MyPet = default!;

    public List<Domain.Pet> Results { get; set; } = [];




    public void Start()
    {
        MyPetId = HttpContext.Request.Query["id"].ToString();
        if (!string.IsNullOrEmpty(MyPetId) && int.TryParse(MyPetId, out var petId))
        {
            Results = _context.Pets
                .Where(p => p.Id != petId)
                .ToList();
            MyPet = _context.Pets.First(p => p.Id == petId);
        }
        else
        {
            Results = _context.Pets.ToList();
        }
    }

    public void OnGet()
    {
        Start();
    }

    public IActionResult OnPost()
    {
        if (string.IsNullOrEmpty(MyPetId))
        {
            return Page();
        }

        return RedirectToPage("./Index", new { id = MyPetId });
    }
}