# NoteApp – version code-behind

Une petite application de notes en **WPF + MaterialDesignInXAML (Material Design 3)**, sur **.NET 10**.
Cette branche contient la version **sans MVVM** : toute la logique est dans le code-behind des écrans.
La branche `main` contient la suite du tutoriel : la version MVVM (sprints 4 à 8), avec ses tests unitaires.

## Les guides

Le dossier [`guides/`](guides/) contient le tutoriel en trois sprints, avec un guide HTML et un quiz Moodle par sprint.
Chaque sprint se termine sur un tag :

| Tag | État de l'application |
| --- | --- |
| `sprint-01` | Le projet, la fenêtre principale, et la navigation entre deux écrans vides |
| `sprint-02` | Créer une note et l'enregistrer dans un fichier, avec un message de confirmation |
| `sprint-03` | Afficher les notes en cartes, en ouvrir une, la supprimer après confirmation |

## Fonctionnalités

- afficher toutes les notes sous forme de cartes ;
- créer une note, l'éditer, l'enregistrer ;
- supprimer une note après confirmation ;
- afficher une notification après un enregistrement ou une suppression.

Les notes sont des fichiers `.txt` dans `%LocalAppData%\WpfNotes\Notes`.

## Lancer l'application

Prérequis : Windows, SDK .NET 10 (Visual Studio 2026 ou `dotnet` en ligne de commande).

```
dotnet run --project WpfNotes.CodeBehind
```

## Structure

```
NoteApp.slnx
WpfNotes.CodeBehind/
  App.xaml(.cs)             thème Material Design 3
  MainWindow.xaml(.cs)      barre d'application, DialogHost, Snackbar, changement d'écran
  Models/
    Note.cs                 le modèle
  Views/
    AllNotesView.xaml(.cs)  la liste : lit le disque, ouvre une note
    NoteView.xaml(.cs)      une note : écrit et supprime sur le disque, ouvre le dialogue
    ConfirmDialog.xaml(.cs) le contenu du dialogue de confirmation
guides/                     le tutoriel (HTML + quiz GIFT)
```

## L'exercice

Essayez d'écrire un test unitaire pour : « Supprimer avec une réponse Annuler ne supprime rien ».
Il faudrait créer une fenêtre, cliquer sur un bouton, répondre à un vrai dialogue et toucher au vrai disque.
Le sprint 3 relève chacun de ces obstacles ; la version MVVM de la branche `main` les lève un par un.
