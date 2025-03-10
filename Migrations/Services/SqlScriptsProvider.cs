using Microsoft.Extensions.FileProviders;
using System.Text.RegularExpressions;

namespace Migrations.Services;

internal sealed partial class SqlScriptsProvider(IFileProvider fileProvider)
{
    private readonly IFileProvider _fileProvider = fileProvider;

    [GeneratedRegex(@"^\d{4}_(?:[\d\w]+)+.sql$", RegexOptions.Compiled)]
    private static partial Regex SqlFileRegex();

    public IEnumerable<(int Version, IFileInfo FileInfo)> GetScripts() =>
        _fileProvider.GetDirectoryContents("")
            .Where(x => SqlFileRegex().IsMatch(x.Name))
            .Select(x => (Version: int.Parse(x.Name[..4]), FileInfo: x))
            .OrderBy(x => x.Version);
}
