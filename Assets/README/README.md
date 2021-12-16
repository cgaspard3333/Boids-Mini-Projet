

# Boids-Mini-Projet Group 1

## Intelligence distribuée

[définition](https://fr.wikipedia.org/wiki/Intelligence_distribuée) : Apparition de phénomènes cohérents à l'échelle d'une population dont les individus agissent selon des règles simples.

Basiquement, l'intelligence distribuée est le fait de créer des règles en apparence simples pour des objets. 
Mais lorsque l'on observe une population significative de ces objets dont chacun obéit aux mêmes règles, on remarque des comportements
d'apparence plus complexes. C'est ce genre de comportements bien plus élaborés que les règles initiales que l'on souhaite recréer dans le cas d'essaims de drones, entre autres. Parmi les exemples de formations complexes régis par des règles élémentaires, on peut penser à des flocks d'oiseaux ou encore le Jeu de la vie de Conway.


## Applications et Intérêts pour notre formation

Dans le cas présent des boids, on pourrait utiliser ce type d'algorithme embarqué sur un drone pour permettre à une flotte de drones de se dissimuler parmi des oiseaux ou des poissons.
Une autre application encore serait de faire du mapping avec un poids de séparation réglée haut.
Aussi, c'est en simulant informatiquement les formations en V des oiseaux (régis par des règles individuelles très simples) on est capable
de prouver qu'utiliser de telles formations permet d'économiser du carburant pour les avions et de la batterie pour les drones. 

## Résumé concept Boids

Les boids sont des individus rassemblés en un groupe. Ils peuvent simuler des flocks d'oiseaux, des essaims d'abeilles ou encore des bancs de poissons. En l'occurrence, ils sont régis par trois règles simples mais peuvent engendrer des comportements assez impressionants :

### Cohésion

![cohésion](./img/cohesion.gif)

Une entité d'une flock tend à se diriger vers la position moyenne des boids voisins.

### Alignement

![alignement](./img/alignment.gif)

L'entité tend à s'aligner vers la direction moyenne des boids voisins.

### Séparation

![séparation](./img/separation.gif)

Un boid garde une certaine distance minimale par rapport à ses voisins pour ne pas encombrer la flock.

![neighborhood](./img/neighborhood.gif)

Les boids voisins sont simplement les autres boids qui se trouvent dans la zone considérée par le boid. 

Ces trois règles, en apparence évidentes, engendrent des mouvements complexes rappelant clairement des flock d'oiseaux ou des bancs de poisson. En cela, les boids sont des exemples concrets de l'intelligence distribuée.

## Description du projet

Qu'est ce qu'il se passe dans ce projet :

Un objet boidManager contenant le script boidSpawner,vient générer un prefab de boid le nombre de fois spécifié dans les réglages (boidSettings).
BoidUserInterface modifie directement les réglages de boidSettings.
Les scripts boid conaissent les boid settings.

## Liste des tâches

Toute l'équipe a travaillé d'arrache pied sur ce projet innovant et bien pensé.
Personne n'a été mis de côté. On a apprécié passer tous nos après-midi ensemble sur l'outil discord pour débattre de l'architecture et des noms de variables.

## Liste des scripts

- BoidSpawner
- Boid
- BoidSettings
- BoidUserInterface

## Listes de l'équipe 1

Group 1 branch

## Members:

- `amassonie` : Alexis Massonie
- `tdeporte` : Tom Deporte
- `nrodrigues64` : Nicolas Rodrigues
- `tyrarin` : Lucas Monlezun
- `R3B3-888` : Alexis Hoffmann

## Implémentation du comportement de ses entités

### Cohesion

### alignement

### séparation

### rencontre d'une autre entité

## Liste des "choses amusantes" à réaliser

- Gizmo
- UI
- amélioration de l'algorithmique sur les poids des différents facteurs.

Ce qu'on aimerait valoriser :

## Ressources

https://www.red3d.com/cwr/boids/
https://github.com/lormori/FlockingDemo/tree/f3f00d3619817fa2667381719f174739ae6bfd41
https://unity.com


