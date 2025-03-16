namespace Mobile.Models;

public class NoteModel
{
    public required int Id { get; init; }

    public required string Content { get; init; }

    public required DateTime CreatedAt { get; init; }

    public DateTime? UpdatedAt { get; init; }

    public string? ImportChecksum { get; init; }
}
