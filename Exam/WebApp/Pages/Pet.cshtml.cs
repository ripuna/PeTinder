using DAL;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages;

public class Pet : PageModel
{

    private readonly AppDbContext _context;
    private readonly ILogger<IndexModel> _logger;
    public Pet(AppDbContext context, ILogger<IndexModel> logger)
    {
        _context = context;
        _logger = logger;
    }

    public string Id { get; set; } = default!;
    public Domain.Pet LPet { get; set; } = default!;

    public string MyPetId { get; set; } = default!;
    public Domain.Pet MyPet { get; set; } = default!;
    public int Likes { get; set; }

    [BindProperty] public string? Com { get; set; }

    public List<Interest?> Interests { get; set; } = default!;

    public bool LikeExists { get; set; }
    public bool FollowExists { get; set; }

    public void Start()
    {
        Id = HttpContext.Request.Query["id"].ToString();
        if (!string.IsNullOrEmpty(Id) && int.TryParse(Id, out var petId))
        {
            LPet = _context.Pets.First(p => p.Id == petId);
        }
        MyPetId = HttpContext.Request.Query["mine"].ToString();
        if (!string.IsNullOrEmpty(MyPetId) && int.TryParse(MyPetId, out var myPetId))
        {
            MyPet = _context.Pets.First(p => p.Id == myPetId);
            LikeExists = _context.Likes.Any(like => like.LikerId == MyPet.Id && like.LikedId == LPet.Id);
            FollowExists = _context.Follows.Any(like => like.FollowerId == MyPet.Id && like.FollowedId == LPet.Id);
        }
        Interests = _context.PetInterests
            .Where(pi => pi.PetId == LPet.Id)
            .Select(pi => pi.Interest)
            .ToList();
        Likes = _context.Likes.Count(like => like.Liked!.Id == LPet.Id);

    }

    public void OnGet()
    {
        Start();
    }

    public IActionResult OnPost()
    {
        Start();
        switch (Com)
        {
            case "like":
                _context.Likes.Add(new Like
                {
                    LikerId = MyPet.Id,
                    Liker = MyPet,
                    LikedId = LPet.Id,
                    Liked = LPet
                });
                _context.SaveChangesAsync();
                break;
            case "follow":
                _context.Follows.Add(new Follow()
                {
                    FollowerId = MyPet.Id,
                    Follower = MyPet,
                    FollowedId = LPet.Id,
                    Followed = LPet
                });
                _context.SaveChangesAsync();
                break;
            case "add":
                return RedirectToPage("/AddInterest", new {id=MyPetId});
            case "followers":
                return RedirectToPage("/Friends", new { id = MyPetId });
            case "matches":
                return RedirectToPage("/Matches", new { id = MyPetId });
            case "search":
                return RedirectToPage("/Search", new { id = MyPetId });
        }

        return RedirectToPage("", new {id=Id,mine=MyPetId});
    }
}