# Docker compose

## Description 

Compose est un outil permettant de définir et d'exécuter des applications Docker multi-conteneurs. Avec Compose, vous utilisez un fichier YAML pour configurer les services de votre application. Ensuite, avec une seule commande, vous créez et démarrez tous les services à partir de votre configuration. Pour en savoir plus sur toutes les fonctionnalités de Compose, consultez la liste des fonctionnalités .

Compose fonctionne dans tous les environnements: production, préparation, développement, test, ainsi que les flux de travail CI. Vous pouvez en savoir plus sur chaque cas dans les cas d' utilisation courants.

Pour plus de détails : https://docs.docker.com/compose/

## Contenu

Nous avons plusieurs conteneurs :

- Un conteneur de base de données MongoDB
- Un conteneur pour le remplissage de données MongoDB
- Un conteneur hébergeant le front web Angular
- Un conteneur hébergeant le serveur applicatif .NET

De base, nous utilisons deux définitions docker-compose :

- docker-compose.yml
- docker-compose.override.yml

Seul un fichier varie en fonction de si le déploiment docker est en debug ou en production pour le serveur .NET:

- docker-compose.vs.debug.yml
- docker-compose.vs.release.yml
