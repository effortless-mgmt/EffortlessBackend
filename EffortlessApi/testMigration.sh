#!/bin/bash

# Run: ./testMigration.sh <prevMigrationName> <newMigrationName>

## Update with previous migration to release last migration
# dotnet ef database update 20181121133919_AddedAddressToUserAndCreatedAddressConfiguration

## Update with migration user's choice to release last migration
dotnet ef database update $1

## Remove last migration
dotnet ef migrations remove

## Add new migration
dotnet ef migrations add $2