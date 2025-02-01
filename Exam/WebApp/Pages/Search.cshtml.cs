using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages;

public class Search : PageModel
{

    private readonly AppDbContext _context;
    private readonly ILogger<IndexModel> _logger;

    public Search(AppDbContext context, ILogger<IndexModel> logger)
    {
        _context = context;
        _logger = logger;
    }

    [BindProperty]
    public string SearchedId { get; set; } = default!;
    public Domain.Interest SearchedInterest { get; set; } = default!;
    public string MyPetId { get; set; } = default!;
    public Domain.Pet MyPet { get; set; } = default!;
    public List<Domain.Pet?> Pets { get; set; } = default!;
    public List<Domain.Interest> Interests { get; set; } = default!;

    private void Start()
    {
        MyPetId = HttpContext.Request.Query["id"].ToString();
        SearchedId = HttpContext.Request.Query["s"].ToString();
        Interests = _context.Interests.ToList();
        if (!string.IsNullOrEmpty(MyPetId) && int.TryParse(MyPetId, out var petId))
        {
            MyPet = _context.Pets.First(p => p.Id == petId);
        }
    }

    public void OnGet()
    {
        Start();
        if (!string.IsNullOrEmpty(SearchedId) && int.TryParse(SearchedId, out var searchId))
        {
            SearchedInterest = _context.Interests.First(p => p.Id == searchId);
            Pets = _context.PetInterests
                .Where(pi => pi.InterestId == searchId && pi.PetId != MyPet.Id)
                .Select(pi => pi.Pet)
                .ToList();
        }
        else
        {
            Pets = _context.Pets.Where(p => p.Id != MyPet.Id).ToList()!;
        }
    }

    public IActionResult OnPost()
    {
        MyPetId = HttpContext.Request.Query["id"].ToString();
        return RedirectToPage("/Search", new { id = MyPetId, s = SearchedId });
    }
}