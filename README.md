#To run the C++ version in powershell:

.\run.ps1
.\run.ps1

.\build\Release\scg_proto_reader.exe
<absolute path to repo>\binary\scg_test.pb

#To run the C# version:
cd csharp\ScgProtoTest
dotnet restore
dotnet build
dotnet run -- <absolute path to repo>\binary\scg_test.pb