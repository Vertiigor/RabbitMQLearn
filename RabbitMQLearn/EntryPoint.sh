#!/bin/bash
set -e

# Wait for Postgres to be available
until dotnet ef database update --project RabbitMQLearn.csproj; do
  echo "Waiting for the database to be ready..."
  sleep 3
done

# Run the app
exec dotnet RabbitMQLearn.dll
