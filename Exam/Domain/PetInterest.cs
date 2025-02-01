namespace Domain;

public class PetInterest : BaseEntity
{
    public int PetId { get; set; }
    public Pet? Pet { get; set; }

    public int InterestId { get; set; }
    public Interest? Interest { get; set; }
}