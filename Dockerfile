FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src


COPY ["QuizGame/QuizGame/QuizGame.csproj", "QuizGame/QuizGame/"]
RUN dotnet restore "QuizGame/QuizGame/QuizGame.csproj"


COPY . .
WORKDIR "/src/QuizGame/QuizGame"
RUN dotnet publish "QuizGame.csproj" -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .


RUN mkdir -p /root/.aspnet/DataProtection-Keys

ENTRYPOINT ["dotnet", "QuizGame.dll"]