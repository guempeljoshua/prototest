cmake_minimum_required(VERSION 3.16)
project(ScgProtoTestCpp VERSION 1.0 LANGUAGES CXX)

set(CMAKE_CXX_STANDARD 17)
set(CMAKE_CXX_STANDARD_REQUIRED ON)

find_package(protobuf CONFIG REQUIRED)

message(STATUS "Protobuf version: ${protobuf_VERSION}")

include_directories(
    ${CMAKE_CURRENT_SOURCE_DIR}/../generated/cpp
)

set(PROTO_SRCS
    ${CMAKE_CURRENT_SOURCE_DIR}/../generated/cpp/scg_test.pb.cc
)

add_executable(scg_proto_reader
    main.cpp
    ${PROTO_SRCS}
)

target_link_libraries(scg_proto_reader
    PRIVATE
    protobuf::libprotobuf
)

add_custom_command(TARGET scg_proto_reader POST_BUILD
    COMMAND ${CMAKE_COMMAND} -E copy_if_different
        ${CMAKE_CURRENT_SOURCE_DIR}/../binary/scg_test.pb
        $<TARGET_FILE_DIR:scg_proto_reader>/scg_test.pb
    COMMENT "Copying scg_test.pb to build directory"
)