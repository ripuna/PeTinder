namespace Domain;

public class Like : BaseEntity
{
    public int LikerId { get; set; }
    public Pet? Liker { get; set; } = default!;

    public int LikedId { get; set; }
    public Pet? Liked { get; set; } = default!;
}