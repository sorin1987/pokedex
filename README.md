# pokedex api

## Running locally
Method 1. With caching enabled
1. docker-compose -f docker-compose.redis.local.yml up --build -d
   This will get you redis running at port 6379 that I used for caching responses
2. run the PokeDex.Api project from any IDE or command line. This will run it on port 5000. By default the app has redis caching enabled

Method 2. Caching disabled
1. docker-compose -f docker-compose.local.yml up --build -d
  This option starts the api at port 5002 but will not have the caching enabled
  
## How to test:
Once you have the API running you can go to http://localhost:5000/swagger/index.html and check the 2 available endpoints and try them from there or from another tool of your choice (Postman, curl etc)
If you have the caching enabled when the app is started, for the translated endpoint you will not be able to see the circuit break pollicy falling back to the default pokemon description unless you call the endpoint with different pokemon names for multiple calls.
Once the call is made for a pokemon name that response will be cached for 10 minutes and will be brought from the cache and cannot reach the Translations API throttling mechanism. 
The rest is pretty simple and requires no explanation.

# Notes
I spent a limited time on this API so I had to be a little pragmatic with how much I implemented but here are some other things I would have done if this was a real world API or I had more time:
1. I would have implemented some form of security for my API (ApiKey or JWT token for example)
2. Provide logging. 
3. Provide some basic metrics (for example the calls duration to the external APIs etc)
4. Integration testing and better unit test coverage (for now I wrote unit tests for my services to give you an idea of how I write them and the tools I like to use)
If you wish we can discuss all these points during the next phase of the interview provided I will reach it.
