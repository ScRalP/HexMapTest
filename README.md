# G?n?ration proc?durale : dessin d'une carte en 3D

## Cartes de tuiles hexagonales
Par des Ing?nieurs Informatique et Multim?dia (2022, promotion 6) :

Quentin Robard<br>
Lilian  Sananikone
___

### Inspirations
Principale inspiration : Catan

<img src="https://boutiquelydie.com/uploads/products/18868_7160.jpg" alt="Jeu de soci?t? Catan" width="300"/>

Il s'agit d'un jeu de soci?t? dans lequel on se d?place sur des tuiles hexagonales pour r?cup?rer des ressources.
Chaque tuile a sa propre couleur et propose une ressource particuli?re.
Il est possible de tracer des chemins entre ces tuiles et de poser des habitations pour cagnoter des ressources.

Inspiration vid?oludique : Northgard

<img src="https://s1.dmcdn.net/v/To_3Q1YK4gZ-LnuEJ/x1080" alt="Northgard" width="500"/>

Northgard est un jeu de strat?gie temps-r?el. Il implique une gestion de ressources disponibles en interagissant avec une carte.
La carte est compos?e de tuiles hexagonales. C'est plus ou moins ce type de visuel que nous souhaitions reproduire.

### 1ers pas
Nous avons suivi des [tutoriels du site Catlike Coding](https://catlikecoding.com/unity/tutorials/hex-map/) pour tout ce qui concernaient le dessin de la carte.

Dans un 1er temps, nous avons seulement utilis? les ?l?ments du 1er tutotiel pour dessiner des tuiles. Nous souhaitions obtenir un ensemble de routes qui se croisent pour former une ville.
Les routes sont faites ? partir de tuiles hexagonales :

<img src="./ImgOfReadMe/01.png" alt="1re ?tape" width="600"/>

Nous avons abandonn? cette id?e pour suivre au mieux le tutoriel et fournir une grille de tuiles. Nous y avons ajouter des hauteurs ? certaines tuiles de mani?re al?atoire pour cr?er des reliefs :

<img src="./ImgOfReadMe/02.png" alt="Ajout de reliefs" width="400"/>


## Accentuation des reliefs

### Trac? des routes
Pour le trac? des routes, nous avons repris notre id?e de d?part. Il s'agit de donner ? la route une direction principale qu'elle suit dans l'ensemble.
On cr?e la route par petits bouts et ? chaque bout pos?, on tire dans une pile une direction al?atoire pour cr?er des petites d?viations de la route.
A chaque tirage, la pile change de fa?on ? permettre ? la route de suivre sa direction principale et de ne pas piocher tout le temps la m?me direction.

<img src="./ImgOfReadMe/06.png" alt="Routes" width="700"/>

Pour g?n?rer nos routes, nous nous sommes bas?s sur une g?n?ration similaire ? celles des niveaux dans le jeu [Spellunky](https://www.youtube.com/watch?v=Uqk5Zf0tw3o&t=3s).

### G?n?ration des rivi?res

Comme pour la cr?ation des route, la g?n?ration des rivi?res suis un fonctionnement de direction.
Une fois les rivi?res cr??e on vient ajouter des lacs au pieds de celles ci ce qui ajoutes un aspect naturel ? notre monde.

<img src="./ImgOfReadMe/07.png" alt="Rivi?res" width="700"/>

## Placement de mod?les 3D

### Choix des mod?les 3D
Au d?but du projet, nous souhaitions reproduire une ville avec des routes compos?es de tuiles hexagonales.
Pour faire honneur ? l'endroit o? nous ?tudions, nous voulions utiliser des mod?les 3D significatifs de la ville d'Angoul?me.
Pour illuster la ville de la Bande Dessin?e, nous utilisons des mod?les 3D des statues de Herg? (le cr?ateur de Tintin) et de Corto Maltese.

<img src="./ImgOfReadMe/03.png" alt="Herg?" width="300"/>   <img src="./ImgOfReadMe/04.png" alt="Corto Maltese" width="400"/>

Toutefois, la plupart des mod?les 3D de la ville est trop volumineuse et difficile ? retravailler pour les adapter ? notre projet. C'est le cas des b?timents.
Pour peupler notre carte d'habitations, nous avons r?cup?rer des mod?les d'un jeu Pok?mon :

<img src="./ImgOfReadMe/08.png" alt="Maisons Pok?mon" width="700"/>

*"Le projet me rappellait des jouets d'enfance Pok?mon. C'est des petites cr?atures qu'on accroche ? des pi?ces hexagonales qui s'assemble pour former un monde : le monde des pok?mons.*
*Quand j'allais au supermarch?, je d?pensais toujours une pi?ce de 2 euros pour repartir avec un prok?mon au hasard.*
*Pour former le monde le plus grand possible, il me fallait beaucoup de pi?ces hexagonales. Peut importait si j'avais des pok?mons en double !"*

*Lilian*


<img src="./ImgOfReadMe/05.png" alt="Jouets Pok?mon" width="400"/>


### Evolution de la m?thode de placement

Apr?s la r?alisation de la grille hexagonale, nous placions cette grille comme ?tant notre monde. Dans ce monde, on ajoutait sur les tuiles des objets 3D. 
On regardait les voisins de chaque tuile et on pla?ait (ou non, car l'objet avait une probalit? de ne pas ?tre positionn?) sur l'intersection du voisinage un mod?le tir? al?atoirement dans une liste.
L'intersection pouvait ?tre un sommet ou le centre d'une tuile.

<img src="./ImgOfReadMe/09.png" alt="Intersections" width="350"/>

Finalement, nous avons d?cid? de s?parer notre monde en plusieurs unit?s dites "biomes". Notre 1er biome est une ville.
Les objets pos?s sur la carte ne sont pas les m?mes et ne sont pas pos?s de la m?me mani?re en fonction du type de biome.
Par rapport ? notre syst?me de positionnement pr?c?dent, nous avons gard? un principe : On ne pose pas un objet entre des tuiles si elles n'ont pas la m?me hauteur.
Nous avons termin? par poser des objets ? proximit? des routes et des rivi?res.


## Faire fonctionner le programme

Pour lancer ce projet il vous faudra une version de Unity sup?rieure ou ?gale ? la version 2019.4.36f1.
Il vous suffit ensuite de lancer la simulation, la g?n?ration va se faire d'elle m?me et vous aurez un monde proc?duralement cr??.
Chaque fois que vous allez lancer la simulation c'est un nouveau monde qui se forme.

### D?placements de la cam?ra

Z => Avancer  
S => Reculer  
Q => Gauche  
D => Droite  

A => Rotation anti horaire  
E => Rotation horaire  

Molette => Zoom  