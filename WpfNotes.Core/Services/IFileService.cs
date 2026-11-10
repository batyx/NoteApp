using WpfNotes.Core.Models;

namespace WpfNotes.Core.Services;

// Le contrat : ce que l'application sait faire avec les fichiers de notes.
// Ceux qui l'utilisent ne savent pas où ni comment les notes sont rangées.
public interface IFileService
{
    // Lit toutes les notes enregistrées
    List<Note> GetNotes();

    // Enregistre la note. Lui donne un nom de fichier si elle n'en a pas encore, et met sa date à jour.
    void SaveNote(Note note);

    // Supprime le fichier d'une note
    void DeleteNote(string filename);
}
