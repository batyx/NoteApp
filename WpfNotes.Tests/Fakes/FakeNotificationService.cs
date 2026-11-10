using WpfNotes.Core.Services;

namespace WpfNotes.Tests.Fakes;

// Un faux service de notification : il garde les messages au lieu de les afficher
public class FakeNotificationService : INotificationService
{
    public List<string> Messages { get; } = new List<string>();

    public void Show(string message)
    {
        Messages.Add(message);
    }
}
