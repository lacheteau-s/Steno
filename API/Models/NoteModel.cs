namespace API.Models;

public class NoteModel
{
    public required int Id { get; set; }

    public required string Content { get; set; }
    
    public required DateTime CreatedAt { get; set; }
    
    public DateTime? UpdatedAt { get; set; }

    public string? ImportChecksum { get; set; }
}
