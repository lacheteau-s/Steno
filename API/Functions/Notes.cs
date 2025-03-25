using API.Models;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace API.Functions;

public class Notes(
    ILogger<Notes> logger,
    NotesService notesService)
{
    private readonly ILogger<Notes> _logger = logger;

    private readonly NotesService _notesService = notesService;

    [Function("CreateNote")]
    [OpenApiOperation("CreateNote")]
    [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
    [OpenApiRequestBody("application/json", typeof(string))]
    [OpenApiResponseWithBody(System.Net.HttpStatusCode.Created, "application/json", typeof(NoteModel))]
    public async Task<IResult> CreateNote(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "notes")] HttpRequest req
    )
    {
        try
        {
            var content = await req.ReadFromJsonAsync<string>();

            if (string.IsNullOrWhiteSpace(content))
                return TypedResults.BadRequest("Note content is required.");

            var note = await _notesService.CreateNoteAsync(content);

            return TypedResults.Created($"{req.Scheme}://{req.Host}{req.Path}/{note.Id}", note);
        }
        catch (Exception ex)
        {
            return TypedResults.InternalServerError(ex.Message);
        }
    }

    [Function("GetNotes")]
    [OpenApiOperation("GetNotes")]
    [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
    [OpenApiResponseWithBody(System.Net.HttpStatusCode.OK, "application/json", typeof(IEnumerable<NoteModel>))]
    public async Task<IResult> GetNotes(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "notes")] HttpRequest req
    )
    {
        try
        {
            var result = await _notesService.GetNotesAsync();

            return TypedResults.Ok(result);
        }
        catch (Exception ex)
        {
            return TypedResults.InternalServerError(ex.Message);
        }
    }
}
