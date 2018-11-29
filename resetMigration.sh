#!/bin/bash

projects=$(find . -name Migrations)
count=$(wc -w <<< $projects)

if [ $count -eq "1" ]; then
    echo "Using project $projects"
    project=$projects
    echo;
else
    echo "More than one projects with migrations exists. Exiting..."
    exit 1
fi

# for project in $projects; do

# echo "Finding projects in $project"
latestFile=$(find $project -type f | sort -n | grep -v "ContextModelSnapshot.cs" | grep -v ".Designer.cs" | tail -2 | cut -f2- -d" ")
fileName=${latestFile#$project}
migrations=$(echo $fileName | sed -e "s/$*.cs$//" -e "s/^\/*//")
m=$(echo "${migrations%% *}")
migration=$(echo $m | sed -e "s/$*.cs$//")

echo "Latest migration is $migration"

read -p "Reset migration $migration (Y/n)? " -n 1 -r
echo;
if [[ $REPLY =~ ^[Yy]$ ]] || [[ -z $REPLY ]]; then
    echo "Reseting database to $migration."
    dotnet ef database update $migration 1
    echo "Removing last migration."
    dotnet ef migrations remove
    echo "Done"
fi

# done
