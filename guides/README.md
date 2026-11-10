# Guides pas à pas — NoteApp, version code-behind

Trois sprints pour construire NoteApp, un carnet de notes en **C# / WPF / Material Design 3** sur **.NET 10**,
avec toute la logique dans le code-behind des écrans.

Public visé : des étudiants qui connaissent la programmation objet en PHP (Laravel) mais pas le C#.
Chaque notion nouvelle est expliquée au moment où le code en a besoin, et les encadrés **Depuis PHP**
montrent comment on l'écrirait en PHP. Le code évite volontairement les syntaxes raccourcies
(`var`, `?.`, `??`, opérateur ternaire, initialiseurs d'objets…) pour rester lisible.

Chaque dossier de sprint contient :

- `guide-sprint-NN.html` — le guide, page autonome (CSS inclus, thème clair/sombre selon le système).
  À déposer sur Moodle comme ressource **Fichier** (affichage « Intégrer » ou « Ouvrir »).
- `quiz-sprint-NN.gift` — 10 QCM à réponse unique, sans pénalité, avec un retour explicatif sur chaque
  proposition. Import Moodle : *Banque de questions → Importer → format GIFT, encodage UTF-8*.
  Le niveau de chaque question est indiqué dans son titre (`[facile]`, `[moyen]`, `[difficile]`).

## Sommaire

| Sprint | Sujet | Dossier | Tag d'arrivée |
|---|---|---|---|
| 1 | Créer le projet, Material Design, fenêtre principale et navigation entre deux écrans | [`sprint-01/`](sprint-01/) | `sprint-01` |
| 2 | Classe `Note`, enregistrement dans un fichier, message de confirmation (Snackbar) | [`sprint-02/`](sprint-02/) | `sprint-02` |
| 3 | Liste des notes en cartes, ouverture, suppression avec confirmation, et la question des tests | [`sprint-03/`](sprint-03/) | `sprint-03` |

Le tag `sprint-NN` de la branche `codeBehind` est le point d'arrivée du sprint NN et le point de départ du suivant.
Le code des guides est celui de ces tags : compare avec `git show sprint-NN:chemin/Fichier.cs`.

## Et ensuite

Le sprint 3 se termine sur un exercice : essayer d'écrire un test unitaire pour la suppression, et relever
tout ce qui l'empêche. La branche `main` contient la suite du tutoriel, les sprints 4 à 8 (version MVVM),
qui lèvent ces obstacles un par un, jusqu'aux tests unitaires.
