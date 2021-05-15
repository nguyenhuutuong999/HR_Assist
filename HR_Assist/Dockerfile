#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["HR_Assist/HR_Assist.csproj", "HR_Assist/"]
RUN dotnet restore "HR_Assist/HR_Assist.csproj"
COPY . .
WORKDIR "/src/HR_Assist"
RUN dotnet build "HR_Assist.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HR_Assist.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN dotnet dev-certs https
ENTRYPOINT ["dotnet", "HR_Assist.dll"]