#!/usr/bin/env bash	

if [ ! -x "$(which docker)" ]; then	
    echo "You need to install docker before running this script."	
    exit 1	
fi	

# Stop and remove the container, don't exit afterwards
if [ "$1" == "reset" ]; then
	if [ "$(docker ps -q -f name=db)" ]; then	
		if [ "$(docker ps -aq -f status=running -f name=db)" ]; then	
			echo "Stopping container 'db'."	
			docker stop db > /dev/null	
		fi	
		echo "Removing container 'db'."
		docker rm db > /dev/null
	fi
fi

# Stop the container and exit, then exit
if [ "$1" == "stop" ]; then	
    if [ "$(docker ps -aq -f name=db)" ]; then	
        if [ "$(docker ps -aq -f status=running -f name=db)" ]; then	
            docker stop db	
        fi	
    fi	

    exit 0
fi

# Stop and remove the container, then exit
if [ "$1" == "remove" ]; then	
    if [ "$(docker ps -q -f name=db)" ]; then	
        if [ "$(docker ps -aq -f status=running -f name=db)" ]; then	
            echo "Stopping container 'db'."	
            docker stop db > /dev/null	
        fi	
        echo "Removing container 'db'."
        docker rm db > /dev/null
    fi

    exit 0
fi

# If the container exists
if [ "$(docker ps -aq -f name=db)" ]; then	
    # If the container exists, but isn't running
    if [ "$(docker ps -aq -f status=exited -f name=db)" ]; then	
        docker start db > /dev/null	
        echo "Docker container 'db' started."	
    # If the container exists, and is already running
    elif [ "$(docker ps -aq -f status=running -f name=db)" ]; then	
        echo "Docker container 'db' already running."	
    fi	
else	
    docker run --name db -e POSTGRES_USER=root -e POSTGRES_PASSWORD=root -p "5432:5432" -d postgres > /dev/null	
    echo "Created new container named 'db'."	
fi

