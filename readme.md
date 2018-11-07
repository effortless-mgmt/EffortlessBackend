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

