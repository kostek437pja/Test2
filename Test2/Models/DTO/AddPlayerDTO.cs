namespace Test2.Models.DTO;

public class AddPlayerDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public List<AddPlayerMathDTO> matches { get; set; }
}

public class AddPlayerMathDTO
{
    public int MatchId { get; set; }
    public int MVPs { get; set; }
    public double Rating { get; set; }
}