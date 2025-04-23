#!/bin/bash

# Apply EF Core migrations
dotnet ef database update --no-build

# Start the app
dotnet RabbitMQLearn.dll
