using API.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace API.Services;

public class NotesService(IConfiguration config)
{
    private readonly IConfiguration _config = config;

    public async Task<NoteModel> CreateNoteAsync(string content)
    {
        
        await using var connection = new SqlConnection(_config.GetConnectionString("Database"));
        await connection.OpenAsync();

        var id = await connection.ExecuteScalarAsync(
            "INSERT INTO Notes (Content, CreatedAt) OUTPUT INSERTED.ID VALUES (@Content, @CreatedAt)",
            new
            {
                Content = content,
                CreatedAt = DateTime.UtcNow
            }) ?? throw new InvalidOperationException("Failed to create note.");

        var note = await connection.QuerySingleOrDefaultAsync<NoteModel>(
            "SELECT * FROM Notes WHERE Id = @Id",
            new { Id = id })
            ?? throw new InvalidOperationException("Failed to retrieve note following creation.");

        return note;
    }
}
