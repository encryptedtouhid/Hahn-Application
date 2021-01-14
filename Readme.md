# Hahn ApplicatonProcess Application

## Frontend Requirements

For development, you will only need Node.js installed on your environement.

### Node
[Node](http://nodejs.org/) is really easy to install & now include [NPM](https://npmjs.org/).
You should be able to run the following command after the installation procedure
below.
		 	$ node --version
  			v0.10.24


## Install

    $ unzip the archive.
    $ cd Hahn-Applicaton/UI
    $ npm install

## Start & watch

    $ npm start

## Simple build for production

    $ npm run build


## Api Requirements

For development, you will only need .net 5.0 SDK installed on your environement.


### Run local with CLI
1. unzip the archive.
2. Install [.NET Core SDK for your platform](https://www.microsoft.com/net/core#windowscmd) if didn't install yet.
3. `cd Hahn-Applicaton/API`
4. `dotnet restore`
5. `dotnet run`


### Run on Visual Studio
1. Install [Visual Studio 2019 for your platform](https://www.visualstudio.com/vs/) if didn't install yet.
2. Open project using this sln file "Hahn.ApplicatonProcess.Application.sln"
5. set "Hahn.ApplicatonProcess.December2020.Web" as Start-up Project
6. Debug -> Start debugging