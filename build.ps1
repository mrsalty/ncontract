dotnet build ".\src\Examples\WebApi\WebApi.csproj"
dotnet build ".\src\Examples\WebApi.ContractTests\WebApi.ContractTests.csproj"
dotnet build ".\src\NContract.Nunit\NContract.Nunit.csproj"
dotnet build ".\src\NContract\NContract.csproj"
nuget pack ".\NContract.nuspec\" -version {version}
nuget push *.nupkg -ApiKey qa3c76wngnspqk6w0hsofy21 -Source "https://ci.appveyor.com/nuget/mrsalty-ncn6qtjpeii6/api/v2/package"