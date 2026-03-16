#To run the C++ version:

cd cpp

cmake -B build ^
  -DCMAKE_TOOLCHAIN_FILE=C:/vcpkg/scripts/buildsystems/vcpkg.cmake ^
  -DVCPKG_TARGET_TRIPLET=x64-windows ^
  -A x64

cmake --build build --config Release

.\build\Release\scg_proto_reader.exe
<absolute path to repo>\binary\scg_test.pb

#To run the C# version:
cd csharp\ScgProtoTest
dotnet restore
dotnet build
dotnet run -- <absolute path to repo>\binary\scg_test.pb