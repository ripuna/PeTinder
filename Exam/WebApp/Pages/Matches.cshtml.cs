using DAL;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages;

public class Matches : PageModel
{
    private readonly AppDbContext _context;
    private readonly ILogger<IndexModel> _logger;

    public Matches(AppDbContext context, ILogger<IndexModel> logger)
    {
        _context = context;
        _logger = logger;
    }

    public string Id { get; set; } = default!;

    public Domain.Pet MyPet { get; set; } = default!;

    public List<Domain.Pet?> MyMatches { get; set; } = default!;

    public void OnGet()
    {
        Start();
    }

    private void Start()
    {
        Id = HttpContext.Request.Query["id"].ToString();
        if (!string.IsNullOrEmpty(Id) && int.TryParse(Id, out var petId))
        {
            MyPet = _context.Pets.First(p => p.Id == petId);
        }

        MyMatches = _context.Likes
            .Where(like => like.LikedId == MyPet.Id)
            .Select(like => like.Liker)
            .ToList();
    }
}
