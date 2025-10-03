# Hello

Web application created using [Ivy](https://github.com/Ivy-Interactive/Ivy).

Ivy is a web framework for building interactive web applications using C# and .NET.

## Prerequisites

- .NET 9 SDK
- Ivy CLI (`dotnet tool install -g Ivy.Console`)

## Run

```
dotnet watch --browse
```

The app will start on `http://localhost:5010`.

## Deploy

```
ivy deploy
```

## Helpful Links

- Docs: `https://docs.ivy.app`
- Samples: `https://samples.ivy.app`
- Framework repo: `https://github.com/Ivy-Interactive/Ivy-Framework`

## Stateless demo

This project includes a demo app using the Stateless state machine (Issue: `https://github.com/Ivy-Interactive/Ivy-Examples/issues/179`).

Open the "Stateless" tab in the running app to interact with a simple traffic light modeled with Stateless triggers and states.