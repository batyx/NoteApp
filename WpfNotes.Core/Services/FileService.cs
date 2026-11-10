using WpfNotes.Core.Models;

namespace WpfNotes.Core.Services;

// L'implémentation réelle du contrat : les notes sont des fichiers .txt sur le disque.
public class FileService : IFileService
{
    // Dossier où sont rangées les notes, par exemple C:\Users\toi\AppData\Local\WpfNotes\Notes
    private readonly string _folder;

    public FileService()
    {
        _folder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "WpfNotes",
            "Notes");
    }

    public List<Note> GetNotes()
    {
        Directory.CreateDirectory(_folder);

        List<Note> notes = new List<Note>();
        string[] paths = Directory.GetFiles(_folder, "*.txt");
        foreach (string path in paths)
        {
            Note note = new Note();
            note.Filename = Path.GetFileName(path);
            note.Text = File.ReadAllText(path);
            note.Date = File.GetLastWriteTime(path);
            notes.Add(note);
        }
        return notes;
    }

    public void SaveNote(Note note)
    {
        // Première sauvegarde : choisir un nom de fichier unique
        if (note.Filename == null)
        {
            note.Filename = "note-" + DateTime.Now.ToString("yyyyMMdd-HHmmss-fff") + ".txt";
        }

        Directory.CreateDirectory(_folder);
        string path = Path.Combine(_folder, note.Filename);
        File.WriteAllText(path, note.Text);
        note.Date = File.GetLastWriteTime(path);
    }

    public void DeleteNote(string filename)
    {
        string path = Path.Combine(_folder, filename);
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }
}
