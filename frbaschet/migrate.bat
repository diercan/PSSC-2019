cd FrBaschet.Infrastructure
SET ConnectionString=server=server=localhost;database=pssc_diana;user=root;password=parola01
dotnet ef --startup-project ..\FrBaschet.API\  migrations add %1
