#!/usr/bin/env bash	

if [ ! -x "$(which docker)" ]; then	
    echo "You need to install docker before running this script."	
    exit 1	
fi	

if [ "$1" == "reset" ]; then	
    echo "Resetting container 'db'."	
    if [ "$(docker ps -q -f name=db)" ]; then	
        if [ "$(docker ps -aq -f status=running -f name=db)" ]; then	
            echo "Stopping container 'db'."	
            docker stop db > /dev/null	
        fi	
        echo "Removing container 'db'."
        docker rm db > /dev/null
    fi
fi

if [ "$1" == "stop" ]; then	
    if [ "$(docker ps -aq -f name=db)" ]; then	
        if [ "$(docker ps -aq -f status=running -f name=db)" ]; then	
            docker stop db	
        fi	
    fi	
fi

if [ "$(docker ps -aq -f name=db)" ]; then	
    if [ "$(docker ps -aq -f status=exited -f name=db)" ]; then	
        docker start db > /dev/null	
        echo "Docker container 'db' started."	
    elif [ "$(docker ps -aq -f status=running -f name=db)" ]; then	
        echo "Docker container 'db' already running."	
    fi	
else	
    docker run --name db -e POSTGRES_USER=root -e POSTGRES_PASSWORD=root -p "5432:5432" -d postgres > /dev/null	
    echo "Created new container named 'db'."	
fi
