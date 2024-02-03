# powerplant-coding-challenge

## Instructions on how to run the Web API with Docker
docker build -f Engie.CodeChallenge.WebApi\Dockerfile -t powerplant-coding-challenge-matej-hrlec .
docker run --name Powerplant_coding_challenge_from_Matej_Hrlec -p 8888:8080 powerplant-coding-challenge-matej-hrlec

The Web API is now available on localhost:8888