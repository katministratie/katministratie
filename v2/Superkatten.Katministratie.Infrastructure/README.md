We draaien een mysql instantie op de raspberry waar de service is geinstalleerd en draaid.

Initieel zijn de volgende stappen genomen:
> dotnet tool install --global dotnet-ef
> dotnet add package Microsoft.EntityFrameworkCore.Design
(
	of als je in de hoofdmap zit:
    > dotnet ef migrations add InitialCreate --context KatministratieContext --project <Infrastructure project>
)
> dotnet ef migrations add InitialCreate
> dotnet ef database update

Daarna is het bij elke wijziging in de database context een aanroep  op de commandline met
> dotnet ef migrations add <NAAM STAP>

We gebruiken de mysqlconnector en EntityFramework van microsoft:
- https://mysqlconnector.net/tutorials/efcore/
- https://learn.microsoft.com/en-us/ef/core/cli/dbcontext-creation?tabs=dotnet-core-cli
