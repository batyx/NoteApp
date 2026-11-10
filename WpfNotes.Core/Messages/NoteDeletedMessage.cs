using WpfNotes.Core.Models;

namespace WpfNotes.Core.Messages;

// Message envoyé par NoteViewModel après une suppression
public class NoteDeletedMessage
{
    public NoteDeletedMessage(Note note)
    {
        Note = note;
    }

    // La note supprimée
    public Note Note { get; }
}
