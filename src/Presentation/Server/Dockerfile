#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Directory.Build.props", "."]
COPY ["Directory.Build.targets", "."]
COPY ["src/Presentation/Server/Mc2.CrudTest.Presentation.Server.csproj", "src/Presentation/Server/"]
COPY ["SharedKernel/Shared/Mc2.CrudTest.Presentation.Shared.csproj", "SharedKernel/Shared/"]
COPY ["src/Application/Mc2.CrudTest.Application/Mc2.CrudTest.Application.csproj", "src/Application/Mc2.CrudTest.Application/"]
COPY ["src/Domain/Mc2.CrudTest.Domain/Mc2.CrudTest.Domain.csproj", "src/Domain/Mc2.CrudTest.Domain/"]
COPY ["src/Domain/Mc2.CrudTest.Domain.Contract/Mc2.CrudTest.Domain.Contract.csproj", "src/Domain/Mc2.CrudTest.Domain.Contract/"]
COPY ["src/Infrastructure/Persistence/Mc2.CrudTest.Persistence.EntityFramework/Mc2.CrudTest.Persistence.EntityFramework.csproj", "src/Infrastructure/Persistence/Mc2.CrudTest.Persistence.EntityFramework/"]
COPY ["src/Application/Mc2.CrudTest.Application.Contract/Mc2.CrudTest.Application.Contract.csproj", "src/Application/Mc2.CrudTest.Application.Contract/"]
COPY ["src/Presentation/Client/Mc2.CrudTest.Presentation.Client.csproj", "src/Presentation/Client/"]
RUN dotnet restore "src/Presentation/Server/Mc2.CrudTest.Presentation.Server.csproj"
COPY . .
WORKDIR "/src/src/Presentation/Server"
RUN dotnet build "Mc2.CrudTest.Presentation.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Mc2.CrudTest.Presentation.Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mc2.CrudTest.Presentation.Server.dll"]