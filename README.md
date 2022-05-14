# G�n�ration proc�durale : dessin d'une carte en 3D

## Cartes de tuiles hexagonales

### Inspirations
Principale inspiration : Catan

<img src="https://boutiquelydie.com/uploads/products/18868_7160.jpg" alt="Jeu de soci�t� Catan" width="300"/>

Il s'agit d'un jeu de soci�t� dans lequel on se d�place sur des tuiles hexagonales pour r�cup�rer des ressources.
Chaque tuile a sa propre couleur et propose une ressource particuli�re.
Il est possible de tracer des chemins entre ces tuiles et de poser des habitations pour cagnoter des ressources.

Inspiration vid�oludique : Northgard

<img src="https://s1.dmcdn.net/v/To_3Q1YK4gZ-LnuEJ/x1080" alt="Northgard" width="500"/>

Northgard est un jeu de strat�gie temps-r�el. Il implique une gestion de ressources disponibles en interagissant avec une carte.
La carte est compos�e de tuiles hexagonales. C'est plus ou moins ce type de visuel que nous souhaitions reproduire.

### 1ers pas
Nous avons suivi des [tutoriels du site Catlike Coding](https://catlikecoding.com/unity/tutorials/hex-map/) pour tout ce qui concernaient le dessin de la carte.

Dans un 1er temps, nous avons seulement utilis� les �l�ments du 1er tutotiel pour dessiner des tuiles. Nous souhaitions obtenir un ensemble de routes qui se croisent pour former des villes.
Les routes sont faites � partir de tuiles hexagonales :

<img src="./ImgOfReadMe/01.png" alt="1re �tape" width="600"/>

Nous avons abandonn� cette id�e pour suivre au mieux le tutoriel et fournir une grille de tuiles. Nous y avons ajouter des hauteurs � certaines tuiles de mani�re al�atoire pour cr�er des reliefs :

<img src="./ImgOfReadMe/02.png" alt="Ajout de reliefs" width="400"/>


## Accentuation des reliefs

### Trac� des routes
Pour le trac� des routes, nous avons repris notre id�e de d�part. Il s'agit de donner � la route une direction princpale qu'elle suit dans l'ensemble.
On cr�e la route par petits bouts et � chaque bout pos�, on tire dans une pile une direction al�taoire pour cr�er des petites d�viations de la route.
A chaque tirage, la pile change de fa�on � permettre � la route de suivre sa direction principale et de ne pas piocher tout le temps la m�me direction.

<img src="./ImgOfReadMe/06.png" alt="Routes" width="700"/>

Pour g�n�rer nos routes, nous nous sommes bas�s sur une g�n�ration similaire � celles des niveaux dans le jeu [Spellunky](https://www.youtube.com/watch?v=Uqk5Zf0tw3o&t=3s).

### G�n�ration des rivi�res

<img src="./ImgOfReadMe/07.png" alt="Rivi�res" width="700"/>

## Placement de mod�les 3D

### Choix des mod�les 3D
Au d�but du projet, nous souhaitions reproduire une ville avec des routes compos�es de tuiles hexagonales.
Pour faire honneur � l'endroit o� nous �tudions, nous voulions utiliser des mod�les 3D significatifs de la ville d'Angoul�me.
Pour illuster la ville de la Bande Dessin�e, nous utilisons des mod�les 3D des statues de Herg� (le cr�ateur de Tintin) et de Corto Maltese.

<img src="./ImgOfReadMe/03.png" alt="Herg�" width="300"/>   <img src="./ImgOfReadMe/04.png" alt="Corto Maltese" width="400"/>

Toutefois, la plupart des mod�les 3D de la ville est trop volumineuse et difficile � retravailler pour les adapter � notre projet. C'est le cas des b�timents.
Pour peupler notre carte d'habitations, nous avons r�cup�rer des mod�les d'un jeu Pok�mon.

*" Le projet me rappellait des jouets d'enfances Pok�mon. C'est des petites cr�atures qu'on accroche � des pi�ces hexagonales qui s'assemble pour former un monde : le monde de pok�mon.*
*Quand j'allais au supermarch�, je d�pensais toujours une pi�ce de 2 euros pour repartir avec un prok�mon au hasard.*
*Pour former le monde le plus grand possible, il me fallait beaucoup de pi�ces hexagonales. Peut importait si j'avais des pok�mons en double !"*

*Lilian*


<img src="./ImgOfReadMe/05.png" alt="Jouets Pok�mon" width="400"/>


### Evolution de la m�thode de placement