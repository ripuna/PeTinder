namespace Domain;

public class Follow : BaseEntity
{
    public int FollowerId { get; set; }
    public Pet? Follower { get; set; } = default!;

    public int FollowedId { get; set; }
    public Pet? Followed { get; set; } = default!;
}