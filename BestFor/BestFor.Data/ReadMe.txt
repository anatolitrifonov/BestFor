Tools –> NuGet Package Manager –> Package Manager Console

Install-Package  Microsoft.EntityFrameworkCore.SqlServer -Project ConsoleApp1 -Pre
Install-Package  Microsoft.EntityFrameworkCore.SqlServer.Design -Project ConsoleApp1 -Pre

Scaffold existing database.
dotnet ef --configuration Debug --build-base-path .\bin\ dbcontext scaffold "Server=ATRIFONO-FLVFR\AT;Database=zzz;Trusted_Connection=False;MultipleActiveResultSets=true;User Id=bestfor;Password=bestfor;Connection Timeout=30;" Microsoft.EntityFrameworkCore.SqlServer --context MyContext --verbose
dotnet ef --configuration Debug --build-base-path .\bin\ dbcontext scaffold 'Server=ATRIFONO-FLVFR\\AT;Database=zzz;Trusted_Connection=False;MultipleActiveResultSets=true;User Id=bestfor;Password=bestfor;Connection Timeout=30;' Microsoft.EntityFrameworkCore.SqlServer --context MyContext --verbose

"Server=ATRIFONO-FLVFR\\AT;Database=zzz;Trusted_Connection=False;MultipleActiveResultSets=true;User Id=bestfor;Password=bestfor;Connection Timeout=30;"


Make sure that you have PowerShell 5 installed


Script-Migration -Project BestFor.Data 

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20160614032953_MyFirstMigration', N'1.0.0-rc2-20901');

Update-Database -Project BestFor.Data 

This preview of Entity Framework tools does not support targeting class library projects in ASP.NET Core and .NET Core applications. See http://go.mic
rosoft.com/fwlink/?LinkId=798221 for details and workarounds.


http://go.microsoft.com/fwlink/?LinkId=798221 for details and workarounds.

Important details https://docs.efproject.net/en/latest/cli/dotnet.html



C:\Users\atrifono\Documents\Personal\Fork\BestFor\BestFor.Data>
dotnet ef --configuration Debug --build-base-path .\bin\ migrations add M1 --json --verbose

cd C:\Users\atrifono\Documents\Personal\Fork\BestFor\BestFor.Data
dotnet ef --configuration Debug --build-base-path .\bin\ database update --verbose

Add-Migration MyFirstMigration -Project BestFor.Data -Verbose
Add-Migration A03 -Project BestFor.Data -Verbose

update-database -Project BestFor.Data -Verbose
