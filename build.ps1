dotnet build .\src\Examples\WebApi\WebApi.csproj
dotnet build .\src\Examples\WebApi\WebApi.ContractTests.csproj
dotnet build .\src\FluentRestApi\FluentRestApi.csproj
dotnet build .\src\NContract\NContract.csproj
nuget pack ".\src\NContract\NContract.csproj"
nuget push *.nupkg -ApiKey qa3c76wngnspqk6w0hsofy21 -Source "https://ci.appveyor.com/nuget/mrsalty-ncn6qtjpeii6/api/v2/package"