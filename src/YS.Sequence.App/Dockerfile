FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY . .
RUN dotnet build "YS.Sequence.App" -c Release -o /app/build

FROM build AS publish
WORKDIR /src
RUN dotnet publish "YS.Sequence.App" -f netcoreapp3.1 -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN mkdir /tmp/quickcore_tmp && \
	mkdir /var/log/quickcore_log && \
	mkdir /quickcore_data

ENV AppFolder__TempFolder /tmp/quickcore_tmp
ENV AppFolder__LogFolder /var/log/quickcore_log
ENV AppFolder__DataFolder /quickcore_data

ENTRYPOINT ["dotnet", "YS.Sequence.App.dll"]
