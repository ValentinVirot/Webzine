# Répartition des tâches
Comme dit précédement, c'est Nathan qui était chef de projet. Dans les premiers jours, nous avons tous défini le projet que nous avons découpé en 3 partie:
- Partie IHM (Interface homme machine) / visuelles
- Partie accés/stockage donnée
- Partie back-end

Ces trois parties sont découpés en plusieurs features qui elles mêmes sont découpés en tâches. ([VOIR LE DASHBOARD](https://dev.azure.com/DiiaZik/Webzine/_dashboards/dashboard/1bee80fa-a9fd-49be-9192-bb48fdea16ee))

Après avoir une base solide pour pouvoir commencer à être efficace dans la gestion du projet, nous avons décidé d'affecter des tâches à chaques membres dans toutes les features ou les parties.
Un exemple: tous le monde à fait le visuel d'une page.
Pendant 2 semaines nous avons suivi ce modèle. Dès qu'une personne terminais son travail, il mettais à jour le repository pour que les autres puissent reprendre son travail et s'en appuyer pour
continuer son travail.
Due à quelques erreurs qui nous ont ralenti et qui vous seront présenté un peu plus tard dans ce rapport, ce modèle n'a pas tenu et nous nous sommes affecté des tâches sans tenir 
compte des tâches que nous avions déjà réalisées mais du temps qu'il nous reste. Par exemple, Nathan à réalisé le CRUD Style la dernière semaine puis a enchainé sur les autres CRUD étant donné qu'il ne restais que quelques tâches et que 
les autres membres était occupés.


# Application architecture

L'application est découpée en plusieurs parties / projets, 10 aux total. Elle est développée sous ASP.NET Core avec plusieurs packages:
- Faker (Insertion de fake data)
- Microsoft.AspNet.Mvc
- Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation (Refresh automatique sans redémarrer la solution)
- Microsoft.EntityFrameworkCore (ORM)
- Microsoft.EntityFrameworkCore.Sqlite (Gestion BDD Sqlite)
- Microsoft.EntityFrameworkCore.SqlServer (Gestion BDD SQLServer)
- Newtonsoft.Json
 Nous avons décider d'utilisé le pattern MVC et le Code First.


## Partie WebApplication (WebApplication):
Ce projet est la partie principale de notre application, elle contient le concept MVC : Modèle View Controllers.
Au démarrage de ce projet, l'application génére un serveur localhost grâce au fichier Program.cs.
Puis instancie le fichier Startup.cs, qui lui s'occupe des injections de dépendances, des middlewares et des gestions des routes.
C'est ce fichier qui va déterminé qu'elle page sera démarrer en premier. Dans notre cas nous avons créer une route par défaut
avec le pattern `{controller=Home}/{action=Index}`. Ce pattern signifie que si dans l'url il n'y a aucune information,
c'est le controller Home qui est instancié et qui va afficher la vue index en éxecutant l'action Index.

Le MVC, comme son nom l'indique permet de diviser son projet en trois parties. Le fonctionnement de ce modèle est simple.
Cependant sa mise en place est régis par plusieurs règles obligatoires. Tous les controllers doivent être placés dans le dossier
"Controllers" qui doivent tous s'appeller "NomDeCeQueTuVeux**Controller**". Pour les views, le dossier racine doit être appelé "Views", puis 
chacunes de ces view doit avoir un dossier avec le nom du controller sans le suffixe controller. Dans ces dossiers, se trouvent des fichiers cshtml propre à la technologie Asp.NetCore Razor qui sont nommer
comme les actions basiques des controllers. Par exemple, action index avec vue index.cshtml.
Pour les modèles, nous avons décider de créer un second projet appelé Entity où se trouvent tous nos modèles (Titre, Artiste, etc ...).
Après avoir ajouté la référence dans le projet WebApplication, nous utilisons des ViewModels pour reprendre uniquement les informations nécessaires
dans l'affichage d'une vue. Avoir l'objet Artiste dans le Titre dans une vue n'est pas nécessaire, seul le nom de l'artiste l'est.
Pour faire le liens entre le modèle, le controller et la vue, la démarche est simple. Lorsque que le controller est instancié et lance la fonction appelée,
à l'interieur de l'action, on instancie un viewmodel avec les informations nécessaire obtenu grâce aux entités.
Dès lors que l'on à le view model, il est possible de la passer en paramètre. Ensuite la vue doit spécifier le type du paramètre reçu grâce à cette ligne de code:
`@model TypeDuParamètre`. Avec ce code, il est possible de reprendre les propriétés de l'objet avec `@Model.NomPropriété`.

Il est possible de diviser son code en Areas, dossier Areas qui contient les dossiers Controllers/Views/ViewsModel.
Cela permet une meilleur visibilité et est bien plus maintenable. Par exemple, si l'on a une partie Administration, il nous faudrait un controller TitreController et un autre ManageTitreController, ce qui 
nous fait des titres très longs. Avec les areas, il est possible de nommer les controllers de la même façon.

### Partie components and layout

Dans la même optique d'avoir une meilleur maintenabilité et une meilleur clareté, nous avons créer des components et un layout.
Les composants doivent avoir des règles de nommages. Les ViewComponents doivent être dans le dossier ViewComponents. Leurs vues doivent être dans un dossier Components lui même
dans le dossier Views / Controller où la vue apelle le composant. Lors de l'appel de ce composant, il est possible de lui passer un paramètre.
Pour le layout, c'est une vue que l'on créer dans le dossier Views/Shared. Pour appeler ce layout dans une vue, il suffit 
de mettre:
`@{
    Layout = "_Layout";
}`
Le nom du layout doit commencer par un underscore.
Dans ce layout, se trouve le header / footer. Nous avons choisi d'utiliser bootstrap donc c'est dans le header et le footer que l'on ajoute les références.
Les librairies externes sont situées dans le dossier `wwwroot/lib/`. 

De plus, la WebApplication contient un fichier de configuration appsettings.json. Ce fichier permet de changer des variables
comme le nombre d'éléments affichés sur une page. 


### Partie API
Le client a souhaité pouvoir accéder aux données de l'extérieur. De ce fait, nous avons créé une WebAPI qui permet de reprendre / rajouter / éditer / supprimer des données.
Pour avoir tous les Titres : `api/Titres`.

## Partie Entity (Bibliothèque de classes):
Comme dit précédemment, ce projet contient toutes les classes:
- Titre
- Style
- Artiste
- Commentaire

## Partie UnitTest (MSTest Core):
Ce projet permet de tester toutes les entités et de savoir si elles répondent à l'attente du client.
Par exemple, le client veux que le nom de l'artiste soit toujours renseigné à sa création et à l'édition.
En Asp.Net Core, il existe les DataAnnotations qui permettre de renseigner plusieurs conditions:
- `Required` (doit être renseigné, peut être suivi de (`(ErrorMessage = "Le nom de l'artiste doit être renseigné !")`)
- `MaxLength(x)` (x correspond à la taille)
- `MinLength(x)`

## Partie Common (Bibliothèque de classes):
C'est un projet qui contient toutes les classes qui peuvent être considérées comme utilisable partout.
Par exemple, nous avons créé la classe StringExtension qui permet de couper une chaine de caractère avec une taille spécifiée puis
de rajouter trois points à la fin.



