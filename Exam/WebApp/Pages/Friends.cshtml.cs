using DAL;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages;

public class Friends : PageModel
{
    private readonly AppDbContext _context;
    private readonly ILogger<IndexModel> _logger;
    public Friends(AppDbContext context, ILogger<IndexModel> logger)
    {
        _context = context;
        _logger = logger;
    }

    public string Id { get; set; } = default!;

    public Domain.Pet MyPet { get; set; } = default!;

    public List<Domain.Pet?> MyFriends { get; set; } = default!;

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

        MyFriends = _context.Follows
            .Where(f => f.FollowerId == MyPet.Id)
            .Join(_context.Follows,
                f1 => f1.FollowedId,
                f2 => f2.FollowerId,
                (f1, f2) => new { f1, f2 })
            .Where(joined => joined.f2.FollowedId == MyPet.Id)
            .Select(joined => joined.f1.Followed)
            .ToList();
    }


}