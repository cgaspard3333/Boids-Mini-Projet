# Modélisation Essaim de drône.
***
Ce projet vise à créer/simuler un essaim de drône sur Unity.
Ces essaims de drône peuvent avoir différente mise en situation tels que la défense, le civile, le secourisme et est un domaine de plus en plus répandu.


## Table of Contents
1. [Introduction : Intelligence distribuée]
2. [Exemple : Boid]
3. [Projet]


### Introduction : Intelligence distribuée : 
Ulrich : 

Un intelligence distribué est un système d'interaction entre agents/élément dans l'accomplissement d'une tâche ou d'un déplacement 
.Ces agents sont souvent de même nature (mais pas nécéssairement, ils peuvent être hétérogène) et constitue une population de n'importe quelle échelle (3, 10 , 100, 10000 ,.....).
Il est important de ne pas la confondre avec une "flotte" qui elle ,possède un capitaine et des vaisseaux qui suivent ce capitaine. Contrairement à l'intelligence distribué qui elle est "robuste". 
Chaque drône considère son voisinage direct pour "comprendre" la situation et s'adapter.ainsi aucune station au sol n'est requise afin de donner des commandes au drône, celui-ci improvise (comme un musicien qui rejoindrait un groupe de musique n,il communique/ecoute juste le BPM et la clé du morceau envirronnante et il peut s'intégrér ^_^).


Alexis L:
L'intelligence distribuée est un principe permettant de résoudre ou calculer des éléments complexes en divisant la charge de travail entre chaque agent. L'intelligence distribuée notamment lors de son utilisation dans un système multi-agent de drone permet à un essaim d'effectuer des missions complexes. On peut notamment attribuer des rôles à chaque agent :
- chef d'équipe : coordonne l'équipe, prend les actions et choisit comment diviser le travail entre les agents.
- Observateur : Produit les images et les envoie à l'équipe calcule pour traiter les données.
- Calculateur : Suis le chef d'équipe et effectuent les calculs (traitement d'images, isolation de point-clé) et renvoie les données au chef d'équipe.Les boids dans notre exemple, permettent de gérer les déplacements de l'équipe chef d'équipe + calculateur en évitant qu'il n'y ait des collisions mais en restant à une distance optimale et dans la même direction.


Adrien R :
- L'intelligence distibuée en essaim est le comportement collectif de systèmes décentralisés et auto-organisés (naturels ou artificiels) qui peuvent manœuvrer rapidement de manière coordonnée. Dans la nature, ce comportement collaboratif en boucle fermée est unique au sein de chaque espèce. Les fourmis déposent des phéromones en se dirigeant mutuellement vers les ressources, les abeilles utilisent des vibrations, les poissons ressentent des tremblements dans l'eau et les oiseaux détectent des mouvements se propageant dans le troupeau.
- Pour moi cette intelligence est une accumulation de comportements semblables à tous les individus de l'essaim permettant le fonctionnement et la survie de ce même essaim.
- Dans le projet nous pouvons coder ces comportements aux moyens de scripts et les incorporer à nos essaims afin d'obtenir ces boids tant recherchés.



### Exemple : Boid

La méthode des Boids permet de créer une intelligence distribué pour contrôler les mouvements d'un essaim. Celle-ci repose sur 3 principes :
	- Cohésion : permet de faire converger les agents vers une position moyenne une position moyenne elle même calculée selon les agents voisins.
	
	-Séparation : permet de maintenir une distance entre chaque agent, comme un effet de réplusion les uns entre les autres
	
	
	-Alignement : permet de faire converger l'angle/la direction plus ou moins rapidement vers la direction moyenne de tout les agents alentours.
	

En pratique, ces règles peuvent renvoyer un vecteur consigne à appliquer à l'agents 
la somme de ces 3 vecteur feront converger les agents vers une position "optimale" qui repondra au mieux aux 3 critères. Ces règles peuvent être coefficientés afin d'en augmenter ou diminuer les effets. Ainsi on se retrouve avec :

Vconsigne = a* Règle1 + b* Règle2 + c* Règle3

avec a,b,c des réels poisitifs ou nul.



### Projet
Nous avons ici essayer d'appliquer l'intelligence distribué avec un essaim en 3 dimension sous Unity afin de le rentre plus interactif et nottament développer derrière plusieur type de scénarion.
#### Structure

Au niveau de la structure de base :

Les principaux élément dans la scène sont

	-L'UI

	-L'Essaim

	-L'Agent

L'UI permet simplement d'avoir un début d'interface qui permettra de gerer la scène/la simulation. le script peut être porter par un gameObject vide par unity mais de préférence nous aurons une hiérarchie ou l'UI sera au dessus de tout les autre gameObject ou bien référencer le gameObject depuis l'éditeur l'objet "Swarm" possèdant tout les agents pour y avoir accès et changer leur paramètres si nécéssaire car si l'on demande à Unity un gameObject qui n'est pas dans sa hiérarchie celui-ci éxécute une recherche avec un algorithmme de graphe (recherche opération, qui peut être très vite être gourmande en ressource si jamais on a beaucouo de gameObject (par exemple 1000000 agents ...).
Dans notre cas il gère le nombre d'agents  à créer, la vitesse des agents, et les coefficient a,b,c des 3 règles

Ainsi Le gameObject Essaim est aussi est encore un gameObject vide mais permettra, de regrouper les agents et gérer leurs physiques ensemble.
Elle est l'équivalente de la station au sol sauf que celle-ci ne commande en rien les drône, elle est juste interatif avec eux dans le cadre de notre  simulation afin de pouvoir changer leur vitesse et coefficient.
Ils reçoit ainsi tout les agents lors du spawn par la fonction "Resources.Load" qui permet d'instancier des prefabs dans la scène depuis un script, il est tout de même nécéssaire de préciser si il aura un parent un non sans quoi, il sera instancier dans la scene sans aucune hiérarchie particulière

Enfin les Agents, le gameObject "réel" avec un mesh (on peut sélectionner n'importe quels agent/forme en changeant le mesh).
Celui-ci possède deux script, un script qui va SEULEMENT calculer le vecteur consigne et  un autre qui va s'occuper de réaliser/mettre à jour les mouvements selon le vecteur consigne et aussi choisir le comportement/la consigne qu'il veut executer.En effet, il faut prendre en compte la possibilité que l'agent puisse se retrouver tout seul ainsi impossible de calculer une consigne par les regles de Boid.


À partir de là, plusieurs problématique sont apparut (propre à Unity et pas forcément à celui d'un système réel, même si celle-ci peuvent être similaires)

-La recherche de voisinage, qui si l'on demande à tout les robots :"es-tu dans mon voisinage ?" on obtient un complexité en (O)n²

-L'esquive d'obstacle, qui est nécéssaire pour ne pas foner dans le murs et pas forcement simple à implmenter si on veut considérer le chemin optimal pour esquiver un objet.

-L'utilisation excessive de la physique dans la scène, celle doit être utilisé le moins possible car plus gourmande contrairement à juste changer la transform d'un gameObject.
#### Ulrich : Collider

J'ai nottament travaillé sur la parti recherche de voisinage afin de réduire la complexité, une approche connu (sur d'autre moteur 3d, ou simulation), est la discrétisation de l'espace et se réduire à l'étude du voisinage seulement dans les cases à proximité de celle échantilloné. ceci à une compléxité en (O)log(n)
Ma solution implémentable seulement via un moteur tels que Unity est la suivante : Utilsier les Colliders de Unity afin de mettre à jour le nombre de voisin autour de l'agent.
Les Colliders sont tout d'abord un des élément utilisé pour la physique de Unity celui-ci. Il permet de créer des collisions entre des objects.

/ ! \ si on veut une "vrai" collision un component "Rigidbody" est nécéssaire.
Il y a tout de même quelque subtilités nottament le mode Trigger que nous utiliserons afin de detecter un autre gameObject/Collider avec la fonction OnTriggerEnter(), qui s'active lorsque l'on a une collision (une fonction intégrer par Unity au même titre que Start(), Update(),....
OnTriggerExit() est aussi utilsié pour les sortie de chaque GameObject. 
Ainsi a chaque agent aura une liste de voisin qui sera déterminé par la taille du collider de "detection" et ainsi avoir un complixité surement proche du (O)log(n). bien sur dans cette hypothèse je néglige la complexité induite par OnTriggerEnter/Exit(), cependant elle reste une fonction très bien optimisé ! 


#### Alexis : Comportement + Boids + Structure du projet
Pour ce projet, je me suis concentré sur le travail des boids et notamment sur le comportement.
Pour la gestion du comportement (BoidBehavior), je l'ai implémenté comme une bibliothèque.
Le fichier BoidBehavior est juste initialisé par chaque boid à la création et les fonctions publiques sont ensuite appelées lors de l'update de chaque boid.
Pour la gestion des objets boids (BoidEntity), j'ai rajouté des coroutines par rapport à la version d'Ulrich même si ça n'a pas pu être fini et testé.
Pour la fin, j'ai réagencé la structure du projet (même si incomplet) pour que cela soit plus lisible et également plus logique en séparant les fichiers en BoidUI(gestion de l'UI et de la récupération des données), BoidBehavior, BoidEntity, BoidController(Gestion générale du projet et interaction entre les classes) et BoidSpawner(Spawn de chaque boid avec initialisation).


#### Clement : RA

Etant malade, je n'ai pas pu terminer l'implémentation avec mes collègues de la partie Réalitée augmentée. Cependant, de mon côté j'ai reussi à implémenter une application Android utilisant le module ARCore de Google permettant grace à du SLAM de créer un environnement virtuel à partir des surface réelles de l'environnement. 
La partie configuration de l'espace de travail Unity pour utiliser Android ainsi que la partie réalité augmentée a été la partie la plus chronophage. En effet, il n'a pas été facile de configurer tout les composants pour qu'ils discutent entre eux.
Dans mon application finale, j'ai donc réussi a afficher toutes les surfaces détectées par le module ARCore et permettre à l'utilisateur en touchant sont écran de créer un objet capsule à l'endroit précis ou se trouve le curseur sur l'écran. La capsule se dépose ensuite à l'endroit sélectionné en prenant en compte les surface virtuelles détectées. 
En effet, si on ajoute la gravité et qu'on pose une capsule contre un mur, elle glisse bien le long du mur et tombe sur le sol, tout ça en réalité augmentée. Cela sera utile lors d'une future implémentation des boids, pour qu'ils puissent aussi s'adapter aux frontières du monde réel.
Il manque donc à implémenter le fait que toucher l'écran crée des boids et que de plus, grâce à des canevas, que l'on puisse directement depuis l'écran du téléphone indiquer la quantité et les parametres d'attractions répulsion.
Le code nécessaire à la réalisation de la partie RA est aussi disponible dans git.


#### Adrien

- Lors de ce TP, j'ai travaillé sur une approche plus centrée autour des comportements que l'on pouvait apporter à l'essaim et voici un exemple de ce que l'on peut obtenir.
- J'ai eu pour objectif de passer ce projet en 3D et j'en ai fait la présentation en cours.
- Objectif projet perso en janvier.


