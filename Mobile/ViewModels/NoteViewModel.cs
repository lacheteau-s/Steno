using Mobile.Models;

namespace Mobile.ViewModels;

public class NoteViewModel
{
    public string Content { get; }

    public string CreationDate { get; }

    public string CreationTime { get; }

    public bool IsImported { get; }

    public NoteViewModel(NoteModel note)
    {
        Content = note.Content;
        IsImported = note.ImportChecksum != null;

        var localDateTime = note.CreatedAt.ToLocalTime();

        CreationDate = localDateTime.ToLongDateString();
        CreationTime = localDateTime.ToShortTimeString();
    }
}
