# Duende IdentityServer4

This is the inspiration: [eShopOnContainers](https://learn.microsoft.com/en-us/dotnet/architecture/microservices/?WT.mc_id=dotnet-35129-website)

Following tutorials from [Duende](https://docs.duendesoftware.com/identityserver/v6/quickstarts/0_overview/)

In Quickstart4 part, where the database is added, I opted for SqlServer instead of SqlLite.  
Also, I preffer PackageManager over command line.  
Therefore, it's necessary to add <i>Microsoft.EntityFrameworkCode.Tools</i>.  
The correct commands for initializing db contexts are:   
```shell
Add-Migration -c PersistedGrantDbContext InitialMigration -o Migrations/PersistedGrantMigrations
```  
```shell
Add-Migration -c ConfigurationDbContext InitialMigration -o Migrations/ConfigurationMigrations
```
```shell
Update-Database -Context ConfigurationDbContext
```  
Wherere -c is shorthand for Context and -o is shorthand for migration location.  
Basically, since IdentityServer has 2 contexts, it's necessary to explicitly set the context when making any sort of migration work.  

Setting certificate for local development in React app: [Tutorial](https://www.mariokandut.com/how-to-configure-https-ssl-in-angular/)