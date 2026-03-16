Remove-Item -Recurse -Force build -ErrorAction SilentlyContinue

cmake -S . -B build -DCMAKE_TOOLCHAIN_FILE=C:/vcpkg/scripts/buildsystems/vcpkg.cmake -DVCPKG_TARGET_TRIPLET=x64-windows -A x64

cmake --build build --config Release

cd .\build\Release
.\scg_proto_reader.exe