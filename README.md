# Gestionnaire des Articles

## Table des matières
1. [Description](#description)
2. [Fonctionnalités](#fonctionnalités)
3. [Technologies Utilisées](#technologies-utilisées)
4. [Installation](#installation)
5. [Base De Données](#base-de-données)
6. [Exécution](#exécution)
7. [Structure du Projet](#structure-du-projet)
8. [Contribuer](#contribuer)




## Description
L’application Gestion des Articles est un projet développé en ASP.NET Core MVC permettant la gestion complète d’un catalogue d’articles.
Elle offre une interface intuitive pour ajouter, modifier, supprimer et consulter des articles, tout en organisant ces derniers par catégories.

Ce projet illustre les bonnes pratiques du développement web moderne avec Entity Framework Core pour la gestion des données et SQL Server comme système de base de données.


## Fonctionnalités
Espace Administrateur
- Gérer les articles : ajouter, modifier, supprimer, consulter.
- Gérer les catégories : créer, modifier, supprimer.
- Gérer les utilisateurs et rôles : créer des comptes, attribuer les rôles (Admin, User), bloquer ou supprimer un utilisateur.
Espace Client (Utilisateur)
- Consulter la liste des articles et leurs détails.
- Rechercher par nom ou catégorie.
- Ajouter des articles au panier ou aux favoris (si activé).
- Créer un compte utilisateur et gérer ses préférences.





## Technologies Utilisées
- C# (.NET 8 / .NET 7) : Langage principal du projet.
- ASP.NET Core MVC : Framework pour la création de l’application web.
- Entity Framework Core : ORM pour l’accès et la manipulation de la base de données.
- SQL Server : Système de gestion de base de données relationnelle.
- Bootstrap 5 : Framework CSS pour un design moderne et responsive.
- HTML5 / CSS3 / JavaScript : Technologies front-end pour la présentation et l’interactivité.



## Installation
Avant de commencer, assurez-vous d’avoir installé :
- Visual Studio 2022 ou VS Code
- .NET SDK 8.0 ou version compatible
- SQL Server ou SQL Server Express


##Base De Données
- L’application utilise Entity Framework Core avec le mode Code-First.
- La base de données est automatiquement générée à partir des modèles C#.
- Commandes utilisées
Dans Visual Studio, ouvrez :
  Outils → Gestionnaire de package NuGet → Console du Gestionnaire de package
1. Créer la première migration
  ```bash
  Add-Migration InitialCreate
  ```
2. Générer la base de données
  ```bash
  Update-Database
  ```
=> Ces commandes créent toutes les tables nécessaires


##Exécution
Pour lancer le projet, il suffit d’utiliser le bouton :
- Exécution sans débogage (Ctrl + F5): Ce bouton démarre directement l’application sans activer le mode debug.
- Alternative en ligne de commande:
```bash
  dotnet run
```
Une fois le projet lancé, ouvrez votre navigateur et accédez à : "https://localhost:7289"

**Page Accueil** 
<img width="1304" height="609" alt="image" src="https://github.com/user-attachments/assets/9c7ec689-7ef3-4714-b118-6ac86805b5e5" />

**Page Article** 
<img width="1303" height="649" alt="image" src="https://github.com/user-attachments/assets/3c5268f9-1b25-44c4-b225-16fa847429c5" />

**Page Panier**
<img width="1300" height="646" alt="image" src="https://github.com/user-attachments/assets/96a5ccaf-9318-4803-821d-9f336309c68b" />

**Page Login**
<img width="1294" height="638" alt="image" src="https://github.com/user-attachments/assets/5fa56981-7ff9-4c5a-b9c9-7c6cd7e4f995" />

**Page Roles**
<img width="1301" height="646" alt="image" src="https://github.com/user-attachments/assets/cd37d9ce-e204-41cb-863e-7ba625c4dc93" />




## Structure du Projet
Voici un aperçu de l'organisation du projet :
```plaintext
  GestionDesArticles/
│
├── Controllers/               # Contrôleurs (ArticleController, CategoryController, AccountController, etc.)
├── Models/                    # Classes métiers et entités (Article, Category)
├── Data/                      # Contexte de la base de données (ApplicationDbContext)
├── Views/                     # Fichiers Razor (.cshtml) pour l’interface utilisateur
│   ├── Article/
│   ├── Category/
│   └── Shared/
├── wwwroot/                   # Contient les fichiers statiques (CSS, JS, images)
├── appsettings.json           # Configuration de l’application
├── Program.cs / Startup.cs    # Point d’entrée et configuration des services
└── README.md                  # Documentation du projet
```




