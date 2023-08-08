#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["LucasNotes.NoteApi/LucasNotes.NoteApi.csproj", "LucasNotes.NoteApi/"]
RUN dotnet restore "LucasNotes.NoteApi/LucasNotes.NoteApi.csproj"
COPY . .
WORKDIR "/src/LucasNotes.NoteApi"
RUN dotnet build "LucasNotes.NoteApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LucasNotes.NoteApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LucasNotes.NoteApi.dll"]