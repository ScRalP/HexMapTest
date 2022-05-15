# Génération procédurale : dessin d'une carte en 3D

## Cartes de tuiles hexagonales
Par des Ingénieurs Informatique et Multimédia (2022, promotion 6) :

Quentin Robard<br>
Lilian  Sananikone
___

### Inspirations
Principale inspiration : Catan

<img src="https://boutiquelydie.com/uploads/products/18868_7160.jpg" alt="Jeu de société Catan" width="300"/>

Il s'agit d'un jeu de société dans lequel on se déplace sur des tuiles hexagonales pour récupérer des ressources.
Chaque tuile a sa propre couleur et propose une ressource particulière.
Il est possible de tracer des chemins entre ces tuiles et de poser des habitations pour cagnoter des ressources.

Inspiration vidéoludique : Northgard

<img src="https://s1.dmcdn.net/v/To_3Q1YK4gZ-LnuEJ/x1080" alt="Northgard" width="500"/>

Northgard est un jeu de stratégie temps-réel. Il implique une gestion de ressources disponibles en interagissant avec une carte.
La carte est composée de tuiles hexagonales. C'est plus ou moins ce type de visuel que nous souhaitions reproduire.

### 1ers pas
Nous avons suivi des [tutoriels du site Catlike Coding](https://catlikecoding.com/unity/tutorials/hex-map/) pour tout ce qui concernaient le dessin de la carte.

Dans un 1er temps, nous avons seulement utilisé les éléments du 1er tutotiel pour dessiner des tuiles. Nous souhaitions obtenir un ensemble de routes qui se croisent pour former une ville.
Les routes sont faites à partir de tuiles hexagonales :

<img src="./ImgOfReadMe/01.png" alt="1re étape" width="600"/>

Nous avons abandonné cette idée pour suivre au mieux le tutoriel et fournir une grille de tuiles. Nous y avons ajouter des hauteurs à certaines tuiles de manière aléatoire pour créer des reliefs :

<img src="./ImgOfReadMe/02.png" alt="Ajout de reliefs" width="400"/>


## Accentuation des reliefs

### Tracé des routes
Pour le tracé des routes, nous avons repris notre idée de départ. Il s'agit de donner à la route une direction principale qu'elle suit dans l'ensemble.
On crée la route par petits bouts et à chaque bout posé, on tire dans une pile une direction aléatoire pour créer des petites déviations de la route.
A chaque tirage, la pile change de façon à permettre à la route de suivre sa direction principale et de ne pas piocher tout le temps la même direction.

<img src="./ImgOfReadMe/06.png" alt="Routes" width="700"/>

Pour générer nos routes, nous nous sommes basés sur une génération similaire à celles des niveaux dans le jeu [Spellunky](https://www.youtube.com/watch?v=Uqk5Zf0tw3o&t=3s).

### Génération des rivières

Comme pour la création des route, la génération des rivières suis un fonctionnement de direction.
Une fois les rivières créée on vient ajouter des lacs au pieds de celles ci ce qui ajoutes un aspect naturel à notre monde.

<img src="./ImgOfReadMe/07.png" alt="Rivières" width="700"/>

## Placement de modèles 3D

### Choix des modèles 3D
Au début du projet, nous souhaitions reproduire une ville avec des routes composées de tuiles hexagonales.
Pour faire honneur à l'endroit où nous étudions, nous voulions utiliser des modèles 3D significatifs de la ville d'Angoulême.
Pour illuster la ville de la Bande Dessinée, nous utilisons des modèles 3D des statues de Hergé (le créateur de Tintin) et de Corto Maltese.

<img src="./ImgOfReadMe/03.png" alt="Hergé" width="300"/>   <img src="./ImgOfReadMe/04.png" alt="Corto Maltese" width="400"/>

Toutefois, la plupart des modèles 3D de la ville est trop volumineuse et difficile à retravailler pour les adapter à notre projet. C'est le cas des bâtiments.
Pour peupler notre carte d'habitations, nous avons récupérer des modèles d'un jeu Pokémon :

<img src="./ImgOfReadMe/08.png" alt="Maisons Pokémon" width="700"/>

*"Le projet me rappellait des jouets d'enfance Pokémon. C'est des petites créatures qu'on accroche à des pièces hexagonales qui s'assemble pour former un monde : le monde des pokémons.*
*Quand j'allais au supermarché, je dépensais toujours une pièce de 2 euros pour repartir avec un prokémon au hasard.*
*Pour former le monde le plus grand possible, il me fallait beaucoup de pièces hexagonales. Peut importait si j'avais des pokémons en double !"*

*Lilian*


<img src="./ImgOfReadMe/05.png" alt="Jouets Pokémon" width="400"/>


### Evolution de la méthode de placement

Après la réalisation de la grille hexagonale, nous placions cette grille comme étant notre monde. Dans ce monde, on ajoutait sur les tuiles des objets 3D. 
On regardait les voisins de chaque tuile et on plaçait (ou non, car l'objet avait une probalité de ne pas être positionné) sur l'intersection du voisinage un modèle tiré aléatoirement dans une liste.
L'intersection pouvait être un sommet ou le centre d'une tuile.

<img src="./ImgOfReadMe/09.png" alt="Intersections" width="350"/>

Finalement, nous avons décidé de séparer notre monde en plusieurs unités dites "biomes". Notre 1er biome est une ville.
Les objets posés sur la carte ne sont pas les mêmes et ne sont pas posés de la même manière en fonction du type de biome.
Par rapport à notre système de positionnement précédent, nous avons gardé un principe : On ne pose pas un objet entre des tuiles si elles n'ont pas la même hauteur.
Nous avons terminé par poser des objets à proximité des routes et des rivières.


## Faire fonctionner le programme

Pour lancer ce projet il vous faudra une version de Unity supérieure ou égale à la version 2019.4.36f1.
Il vous suffit ensuite de lancer la simulation, la génération va se faire d'elle même et vous aurez un monde procéduralement créé.
Chaque fois que vous allez lancer la simulation c'est un nouveau monde qui se forme.