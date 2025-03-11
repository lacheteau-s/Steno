using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace API.Functions;

public class Notes(ILogger<Notes> logger)
{
    private readonly ILogger<Notes> _logger = logger;

    [Function("GetNotes")]
    public IResult GetNotes(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "notes")] HttpRequest req
    )
    {
        var result = new[]
        {
            new NoteModel
            {
                Id = 1,
                Content = "Note 1",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new NoteModel
            {
                Id = 1,
                Content = "Note 2",
                CreatedAt = DateTime.UtcNow,
            },
        };

        return TypedResults.Ok(result);
    }
}
