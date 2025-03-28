﻿using Mobile.Models;
using Refit;

namespace Mobile.Services;

public interface IApiClient
{
    [Post("/notes")]
    Task<NoteModel> CreateNote([Body(BodySerializationMethod.Serialized)] string content);

    [Get("/notes")]
    Task<IEnumerable<NoteModel>> GetNotes();
}
