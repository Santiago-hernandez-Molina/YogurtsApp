from mcr.microsoft.com/dotnet/aspnet:7.0 as base
workdir /app
expose 80

from mcr.microsoft.com/dotnet/sdk:7.0 as build
workdir /src
copy ["*.csproj", "./"]
run dotnet restore "NutryDairyASPApplication.csproj"
copy . .
workdir "/src"
run dotnet build "NutryDairyASPApplication.csproj" -c release -o /app/build

from build as publish
# publish will copy wwwroot files as well
run dotnet publish "NutryDairyASPApplication.csproj" -c release -o /app/publish
workdir /app/publish

from base as final
workdir /app
copy --from=publish /app/publish .
entrypoint ["dotnet", "NutryDairyASPApplication.dll"]
