using DAL;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages;

public class AddInterest : PageModel
{

    private readonly AppDbContext _context;
    private readonly ILogger<IndexModel> _logger;
    public AddInterest(AppDbContext context, ILogger<IndexModel> logger)
    {
        _context = context;
        _logger = logger;
    }

    public string Id { get; set; } = default!;
    public Domain.Pet Pet { get; set; } = default!;

    public List<Domain.Interest> Interests { get; set; } = default!;

    [BindProperty]
    public int InterestId { get; set; }

    public void OnGet()
    {
        Start();
    }

    private void Start()
    {
        Id = HttpContext.Request.Query["id"].ToString();
        if (!string.IsNullOrEmpty(Id) && int.TryParse(Id, out var petId))
        {
            Pet = _context.Pets.First(p => p.Id == petId);
        }

        Interests = _context.Interests.ToList();
    }

    public IActionResult OnPost()
    {
        Start();
        _context.PetInterests.Add(new PetInterest
        {
            InterestId = InterestId,
            PetId = Pet.Id
        });
        _context.SaveChangesAsync();
        return RedirectToPage("/Pet", new { id = Id, mine = Id });
    }


}