namespace WpfNotes.CodeBehind.Models;

public class Note
{
    // Nom du fichier sur le disque. Vaut null tant que la note n'a jamais été enregistrée.
    public string? Filename { get; set; }

    // Le texte de la note
    public string Text { get; set; } = "";

    // Date du dernier enregistrement
    public DateTime Date { get; set; }
}
