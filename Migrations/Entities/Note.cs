namespace Migrations.Entities;

internal class Note
{
    public required string Content { get; set; }

    public required DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? ImportChecksum { get; set; }
}
