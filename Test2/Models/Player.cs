using System.ComponentModel.DataAnnotations;

namespace Test2.Models;

public class Player
{
    public int PlayerId { get; set; }
    [MaxLength(50)]
    public string FirstName { get; set; }
    [MaxLength(100)]
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public ICollection<PlayerMatch> PlayerMatches { get; set; }
}