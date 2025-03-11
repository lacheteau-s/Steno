using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace API.Functions;

public class Notes(ILogger<Notes> logger)
{
    private readonly ILogger<Notes> _logger = logger;

    [Function("CreateNote")]
    [OpenApiOperation("CreateNote")]
    [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
    [OpenApiRequestBody("application/json", typeof(string))]
    [OpenApiResponseWithBody(System.Net.HttpStatusCode.OK, "application/json", typeof(string))]
    public async Task<IResult> CreateNote(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "notes")] HttpRequest req
    )
    {
        var note = await req.ReadFromJsonAsync<string>();

        if (string.IsNullOrWhiteSpace(note))
            return TypedResults.BadRequest("Note content is required.");

        return TypedResults.Ok(note);
    }

    [Function("GetNotes")]
    [OpenApiOperation("GetNotes")]
    [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
    [OpenApiResponseWithBody(System.Net.HttpStatusCode.OK, "application/json", typeof(IEnumerable<NoteModel>))]
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
