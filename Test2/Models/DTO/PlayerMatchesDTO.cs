namespace Test2.Models.DTO;

public class PlayerMatchesDTO
{
    public int PlayerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public List<MatchDto> Matches { get; set; }
}

public class MatchDto
{
    public string Torunament { get; set; }
    public string Map { get; set; }
    public DateTime Date { get; set; }
    public int MVPs { get; set; }
    public double Rating { get; set; }
    public int Team1Score { get; set; }
    public int Team2Score { get; set; }
}