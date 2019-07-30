# Rutracker Blazor

The application was built on the basis of the Blazor-Hosted template, which includes **Server (RESTful API)** and **Client (Blazor-client)**.


## Demo and Documentation

You can see an example of a ready-made application on **Github Pages** - [Demo](https://rutracker.azurewebsites.net/). Also, you can see the api endpoints documentation here - [Documentation](https://osipukr.github.io/Rutracker-Blazor/).


## Running the sample using Docker

You can run the application sample by running these commands from the root folder (where the **.sln** file is located):

```
    docker-compose build
    docker-compose up
```

You should be able to make requests to **localhost:5106** once these commands complete.

You can also run the application by using the instructions located in its `Dockerfile` file in the root of the project. Again, run these commands from the root of the solution (where the **.sln** file is located).