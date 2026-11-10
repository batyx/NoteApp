# NoteApp – version MVVM

Une petite application de notes en **WPF + MaterialDesignInXAML (Material Design 3)**, sur **.NET 10**,
qui sert de support à un tutoriel sur l'architecture MVVM et les tests unitaires.

## Les deux branches

| Branche | Sprints | Contenu |
| --- | --- | --- |
| `codeBehind` | 1 à 3 | La version de départ : toute la logique dans le code-behind, impossible à tester |
| `main` | 4 à 8 | La version MVVM : logique séparée, services derrière des interfaces, injection de dépendances, tests |

Pour comparer les deux versions sans changer de branche : `git diff codeBehind main`,
ou un second dossier de travail avec `git worktree add ../NoteApp-codeBehind codeBehind`.

## Les guides

Le dossier [`guides/`](guides/) contient le tutoriel MVVM en cinq sprints, avec un guide HTML et un quiz Moodle par sprint.
Chaque sprint se termine sur un tag :

| Tag | État de l'application |
| --- | --- |
| `sprint-04` | Deux projets ; la liste des notes vient d'un ViewModel, par Binding |
| `sprint-05` | Éditer une note : commandes, navigation par ViewModel, notifications |
| `sprint-06` | Supprimer avec confirmation ; les ViewModels communiquent par messages |
| `sprint-07` | Les objets sont assemblés par un conteneur d'injection de dépendances |
| `sprint-08` | Les tests unitaires : fakes écrits à la main, puis NSubstitute |

## Fonctionnalités

- afficher toutes les notes sous forme de cartes ;
- créer une note, l'éditer, l'enregistrer (retour à la liste) ou annuler ;
- supprimer une note après confirmation (bouton grisé pour une note neuve) ;
- afficher une notification après un enregistrement ou une suppression.

Les notes sont des fichiers `.txt` dans `%LocalAppData%\WpfNotes\Notes`, partagés avec la version code-behind.

## Lancer et tester

Prérequis : Windows, SDK .NET 10 (Visual Studio 2026 ou `dotnet` en ligne de commande).

```
dotnet run --project WpfNotes   # l'application
dotnet test                     # les tests unitaires
```

Les tests ne dépendent pas de WPF : `dotnet test` fonctionne aussi sous Linux ou macOS.

## Structure de la solution

| Projet | Cible | Contenu | Références |
| --- | --- | --- | --- |
| `WpfNotes` | `net10.0-windows` | Vues XAML, MaterialDesign, `DialogService`, `NotificationService`, injection de dépendances | `WpfNotes.Core` |
| `WpfNotes.Core` | `net10.0` | Modèle, ViewModels, interfaces de services, messages | aucune |
| `WpfNotes.Tests` | `net10.0` | Tests xUnit, fakes, tests NSubstitute | `WpfNotes.Core` |

**La règle d'or :** `WpfNotes.Core` cible `net10.0` et non `net10.0-windows`.
Il ne peut donc utiliser aucun type WPF ni MaterialDesign : le compilateur le refuse.

**Un compromis assumé :** `MainViewModel` joue aussi le rôle du service de navigation (`INavigationService`)
et crée lui-même les `NoteViewModel`. On évite ainsi une classe et des fabriques de plus.

## Les tests

| # | Test | Ce qu'il enseigne |
| --- | --- | --- |
| 1 | `Text_WhenChanged_RaisesPropertyChanged` | Le premier test |
| 2 | `DeleteCommand_ForNewNote_CannotExecute` | Tester `CanExecute` (bouton grisé) |
| 3 | `SaveCommand_WritesNoteToFileService` | Premier fake, vérification de l'état |
| 4 | `SaveCommand_SendsNoteSavedMessage` | Tester la messagerie |
| 5 | `SaveCommand_NavigatesBackToList` | Tester la navigation |
| 6 | `CancelCommand_NavigatesBackWithoutSaving` | Plusieurs vérifications |
| 7 | `DeleteCommand_WhenUserSaysNo_DeletesNothing` | Le test impossible du sprint 3 |
| 8 | `DeleteCommand_WhenUserSaysYes_DeletesNotifiesAndNavigates` | Test asynchrone |
| 9 | `LoadNotes_FillsNotesFromFileService` | Test paramétré avec `[Theory]` |
| 10 | `ReceiveNoteDeletedMessage_RemovesNoteFromList` | Un ViewModel destinataire de messages |
| 11 | `NewNoteCommand_NavigatesToNewNote` | Commande et navigation |

Le dossier `WithMocks` réécrit les tests 3, 7 et 8 avec NSubstitute, pour comparer fakes et mocks.
