dotnet ef migrations add Initial -c FREETIMEMigrationsDbContext -s ..\FREETIME.DbMigrator\FREETIME.DbMigrator.csproj -o .\Migrations


# generate sql script 
dotnet ef migrations script -c FREETIMEDbContext -s ..\FREETIME.DbMigrator\FREETIME.DbMigrator.csproj -o script.sql