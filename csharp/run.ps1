$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
Set-Location $scriptDir

$pbFile = Resolve-Path "$scriptDir\..\binary\scg_test.pb"

dotnet restore
dotnet build
dotnet run -- "$pbFile"