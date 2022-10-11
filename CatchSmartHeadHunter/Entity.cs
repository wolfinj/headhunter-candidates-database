using System.ComponentModel.DataAnnotations;

namespace CatchSmartHeadHunter;

public class Entity
{
    [Key]
    [System.Text.Json.Serialization.JsonPropertyOrder(-2)]
    public int Id { get; set; }
}
