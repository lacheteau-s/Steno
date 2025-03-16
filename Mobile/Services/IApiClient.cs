using Refit;

namespace Mobile.Services;

public interface IApiClient
{
    [Post("/notes")]
    Task CreateNote([Body(BodySerializationMethod.Serialized)] string content);
}
