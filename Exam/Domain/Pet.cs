using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Pet : BaseEntity
{
    [MaxLength(128)]
    public string Animal { get; set; } = default!;
    [MaxLength(128)]
    public string Breed { get; set; } = default!;
    [MaxLength(128)]
    public string Name { get; set; } = default!;
    [MaxLength(128)]
    public string Password { get; set; } = default!;
    [MaxLength(128)]
    public string Owner { get; set; } = default!;
    public int Age { get; set; } = default!;
}