# API

To start the run the project:

```bash
docker-compose up -d
```

To attach to postgresql on the container, first get the container name:

```bash
docker ps
```

The name should be `<folder_name>_db_1`, i.e.: `backend_db_1`. Now we can connect to the database `EffortlessApi` using the `psql` command in the container:

```bash
docker exec -it backend_db_1 psql EffortlessApi
```

## Only start database container

To start a docker container with database only:

```bash
./start-docker.sh
```

To stop it again:

```bash
./start-docker.sh
```

To reset the container (stop, remove and create a new one):

```bash
./start-docker.sh reset
```

## To start a custom docker-compose file

```bash
docker-compose --file docker-compose.production.yml up -d --build
```

Example for custom docker-comopse file:

```yaml
version: '3'
services:
  restapi:
    build:
      context: ./EffortlessApi
      dockerfile: Dockerfile
    depends_on:
      - db
    ports:
      - "5000:80"
    restart: always
    environment:
      - DB_HOST=db
      - AUTH_SIGNING_KEY=secretSigningKey
      - ASPNETCORE_ENVIRONMENT=Production
      - VIRTUAL_HOST=api.example.com
      - LETSENCRYPT_HOST=api.example.com
      - LETSENCRYPT_EMAIL=stuff@example.com
  db:
   image: postgres
   ports:
     - "5432:5432"
   restart: always
   environment:
     POSTGRES_USER: "root"
     POSTGRES_PASSWORD: "root"
networks:
  default:
    external:
      name: nginx-proxy
```

## Setup automatic deployment with git

<https://medium.com/@francoisromain/vps-deploy-with-git-fea605f1303b>
