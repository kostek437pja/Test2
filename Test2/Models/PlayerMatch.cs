using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Test2.Models;

[Table("Player_Match")]
[PrimaryKey("MatchId", "PlayerId")]
public class PlayerMatch
{
    [ForeignKey("Match")]
    public int MatchId { get; set; }
    public Match Match { get; set; }
    [ForeignKey("Player")]
    public int PlayerId { get; set; }
    public Player Player { get; set; }
    public int MVPs { get; set; }
    [Column(TypeName = "decimal")]
    [Precision(4,2)]
    public double Rating { get; set; }
}