# .NET 6.0 and Angular 13.x in Dev Container

A base template for __.NET 6 WebApi__ backend __Angular 13__ SPA frontend.

Here is what I have so far:

- __Serilog__ for logging.  
- __Dapper__ for ORM.
- TODO: Identity Server for authentication.
- TODO: Build deployment container template

There is a sample data load script called `sql-dataload.sql` that can populate a sample DB.  A few simple scripts for setting up a SQL Server docker container make it fairly straightforward.

## HTTPS
Initial setup involves sharing and trusting the development TLS cert so that it is trusted on both the host machine (by the browser), and also by the underlying proxy that runs in the docker environment and integrates with the SPA frameworks.

__dotnet dev certs__ is handy for getting all this setup.

1. __Create the dev certificate.__ dotnet 6.0 SDK is required on the host machine in order to run the following command:

```
dotnet dev-certs https -ep ${HOME}/Workspace/certs/dotnetcert.pfx -p { password here }
```

> By including the password both the public and private keys will be embedded in the pfx.

__Note:__ the certificate is exported to `${HOME}/Workspace/certs/` as `dotnetcert.pfx`.  Subsequent configurations expect this location and naming, and would need to be updated accordingly if changed.

2. __Trust the certificate on the host machine__.

```
dotnet dev-certs https --trust
```

The command works it magic installing and trusting the certificate, and many sudo passwords will be entered.

The docker container will mount the host folder `~/Workspcae/certs`, which is then specified in the `.devcontainer.json` via the ENV variable for the ASPNET runtime:

```
	"remoteEnv": {
	 	"ASPNETCORE_Kestrel__Certificates__Default__Password": "",
	   	"ASPNETCORE_Kestrel__Certificates__Default__Path": "/home/vscode/.aspnet/https/dotnetcert.pfx"
    },

```

In this way both the containerized ASPNET server and the host browser use the same locally trusted dev certificate (which is bound to the name `localhost`).

