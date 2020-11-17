FROM mcr.microsoft.com/dotnet/core/sdk:3.1 as server

ENV ASPNETCORE_Environment=Production

WORKDIR /server
COPY . ./

RUN dotnet publish -c Release -o publish


CMD ["dotnet","publish/LeaderBoardService.dll"]