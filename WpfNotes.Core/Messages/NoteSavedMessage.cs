using WpfNotes.Core.Models;

namespace WpfNotes.Core.Messages;

// Message envoyé par NoteViewModel après un enregistrement
public class NoteSavedMessage
{
    public NoteSavedMessage(Note note)
    {
        Note = note;
    }

    // La note enregistrée
    public Note Note { get; }
}
