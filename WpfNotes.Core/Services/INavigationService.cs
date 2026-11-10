using WpfNotes.Core.Models;

namespace WpfNotes.Core.Services;

// Changer d'écran. Les ViewModels demandent un écran ; ils ne savent pas comment il s'affiche.
public interface INavigationService
{
    // Affiche la liste des notes
    void NavigateToAllNotes();

    // Affiche l'écran d'une note : une note existante, ou une note neuve
    void NavigateToNote(Note note);
}
