dotnet build .\Examples\WebApi\WebApi.csproj
dotnet build .\Examples\WebApi\WebApi.ContractTests.csproj
dotnet build .\FluentRestApi\FluentRestApi.csproj
dotnet build .\src\NContract\NContract.csproj
 nuget push ncontract.nupkg -ApiKey qa3c76wngnspqk6w0hsofy21 -Source https://ci.appveyor.com/nuget/mrsalty-ncn6qtjpeii6/api/v2/package