using WpfNotes.Core.Services;

namespace WpfNotes.Tests.Fakes;

// Une fausse boîte de dialogue : elle répond ce que le test a décidé, sans rien afficher
public class FakeDialogService : IDialogService
{
    // La réponse que donnera « l'utilisateur »
    public bool Answer { get; set; }

    // Le nombre de fois où une confirmation a été demandée
    public int CallCount { get; private set; }

    public Task<bool> ConfirmAsync(string message)
    {
        CallCount = CallCount + 1;
        return Task.FromResult(Answer);
    }
}
