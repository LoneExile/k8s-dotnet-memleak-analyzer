FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

RUN apt-get update && apt-get install -y \
  tar \
  gzip \
  && rm -rf /var/lib/apt/lists/*

RUN dotnet tool install --tool-path /tools dotnet-dump
RUN dotnet tool install --tool-path /tools dotnet-symbol

ENV PATH="/tools:${PATH}"

RUN mkdir /dumps

FROM mcr.microsoft.com/dotnet/runtime:8.0

COPY --from=build /tools /tools
ENV PATH="/tools:${PATH}"

RUN mkdir /dumps

WORKDIR /dumps

CMD ["tail", "-f", "/dev/null"]
