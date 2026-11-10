using WpfNotes.Core.Models;
using WpfNotes.Core.Services;

namespace WpfNotes.Tests.Fakes;

// Un faux service de navigation : il retient les écrans demandés au lieu de les afficher
public class FakeNavigationService : INavigationService
{
    // Le nombre de retours à la liste demandés
    public int NavigatedToAllNotesCount { get; private set; }

    // La dernière note qu'on a demandé d'ouvrir, ou null si aucune
    public Note? LastOpenedNote { get; private set; }

    public void NavigateToAllNotes()
    {
        NavigatedToAllNotesCount = NavigatedToAllNotesCount + 1;
    }

    public void NavigateToNote(Note note)
    {
        LastOpenedNote = note;
    }
}
