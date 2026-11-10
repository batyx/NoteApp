using WpfNotes.Core.Models;
using WpfNotes.Core.Services;

namespace WpfNotes.Tests.Fakes;

// Un faux service de fichiers : les notes restent en mémoire, aucun fichier n'est touché
public class FakeFileService : IFileService
{
    private int _counter = 0;

    // Les notes « enregistrées ». Un test peut en ajouter avant d'agir.
    public List<Note> Notes { get; } = new List<Note>();

    // Les noms de fichiers supprimés, dans l'ordre
    public List<string> DeletedFilenames { get; } = new List<string>();

    public List<Note> GetNotes()
    {
        List<Note> copy = new List<Note>(Notes);
        return copy;
    }

    public void SaveNote(Note note)
    {
        if (note.Filename == null)
        {
            _counter = _counter + 1;
            note.Filename = "note-" + _counter + ".txt";
        }

        // Une date fixe : un test ne doit pas dépendre de l'heure à laquelle il s'exécute
        note.Date = new DateTime(2026, 1, 1, 12, 0, 0);

        if (!Notes.Contains(note))
        {
            Notes.Add(note);
        }
    }

    public void DeleteNote(string filename)
    {
        DeletedFilenames.Add(filename);
    }
}
