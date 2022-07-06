# CVE Webscraper

This program sends search requests to cve.mitre.org for a given software and returns all CVEs found for that software. 
The software is written in .NET 6.0

Replace all <software> in the commands with the software you are searching for.

## .NET 6.0
### Windows

For Windows please install the SDK from Microsoft https://dotnet.microsoft.com/en-us/download/dotnet

To compile the project for the first time run the commands

`dotnet restore`
`dotnet build`

To execute the program:

`C:\PATH\TO\EXECUTEABLE.exe\ <software>`

### Linux

For Linux please install the SDK as described on the official Microsoft webpage https://docs.microsoft.com/de-de/dotnet/core/install/linux?WT.mc_id=dotnet-35129-website for your Linux OS

To compile the project for the first time run the commands

`dotnet restore`
`dotnet build /PATH/TO/wpf1.csproj --runtime linux-x64`

To execute the program: 

`/PATH/TO/EXECUTEABLE <software>`

## dailycheck.sh

This is a simple bash script that can be used to mail the results of a CVE search.

This script uses the mail command from mailultils. To get this package execute

`sudo apt-get install -y mailutils`

### Editing the script

Replace /PATH/TO/EXECUTEABLE with the path to the compiled program as well as recipient@example.com with your own mail address.

## crontab

After compiling the software you can setup a crontab with the included bashscript dailycheck.sh

### crontab setup

Execute the command

`crontab -l`

and the following line to the crontab for each software you want to have checked:

`0 0 * * * /PATH/TO/SCRIPT/dailycheck.sh <software>`

This setup a daily check at midnight.

To change the hour of the check change the 2nd 0 to any number from 0-23 representing a 24h format.

After setting up crontab list execute 

`crontab -e`

to save and apply changes made in the crontab list.


