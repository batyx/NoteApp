# Guides pas à pas — NoteApp, version MVVM

Cinq sprints pour reconstruire NoteApp en **MVVM**, avec le **MVVM Toolkit**, l'injection de dépendances et des
**tests unitaires** xUnit. Ils font suite aux trois sprints de la version code-behind (branche `codeBehind`),
qui se terminent sur un constat : la logique de l'application, prisonnière des écrans, ne peut pas être testée.

Public visé : des étudiants qui connaissent la programmation objet en PHP (Laravel) mais pas le C#.
Chaque notion nouvelle est expliquée au moment où le code en a besoin, et les encadrés **Depuis PHP**
font le lien avec Laravel (contrats, conteneur de services, événements, PHPUnit…). Le code évite volontairement
les syntaxes raccourcies (`var`, `?.`, `??`, opérateur ternaire, initialiseurs d'objets…). Le MVVM Toolkit est
utilisé directement ; à chaque attribut, un encadré montre une version simplifiée du code qu'il génère.

Chaque dossier de sprint contient :

- `guide-sprint-NN.html` — le guide, page autonome (CSS inclus, thème clair/sombre selon le système).
  À déposer sur Moodle comme ressource **Fichier** (affichage « Intégrer » ou « Ouvrir »).
- `quiz-sprint-NN.gift` — 10 QCM à réponse unique, sans pénalité, avec un retour explicatif sur chaque
  proposition. Import Moodle : *Banque de questions → Importer → format GIFT, encodage UTF-8*.

## Sommaire

| Sprint | Sujet | Dossier | Tag d'arrivée |
|---|---|---|---|
| 4 | Séparer la logique : deux projets, la règle d'or, `IFileService`, premier ViewModel | [`sprint-04/`](sprint-04/) | `sprint-04` |
| 5 | Éditer une note : MVVM Toolkit, commandes, navigation par ViewModel, notifications | [`sprint-05/`](sprint-05/) | `sprint-05` |
| 6 | Supprimer et prévenir la liste : Messenger, `IDialogService`, `CanExecute` | [`sprint-06/`](sprint-06/) | `sprint-06` |
| 7 | L'injection de dépendances : `ServiceCollection` | [`sprint-07/`](sprint-07/) | `sprint-07` |
| 8 | Les tests unitaires : xUnit, fakes, NSubstitute | [`sprint-08/`](sprint-08/) | `sprint-08` |

Le tag `sprint-NN` de la branche `main` est le point d'arrivée du sprint NN et le point de départ du suivant.
Le code des guides est celui de ces tags : compare avec `git show sprint-NN:chemin/Fichier.cs`.
