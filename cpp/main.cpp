#include <iostream>
#include <fstream>
#include <sstream>
#include <string>

#include "google/protobuf/util/json_util.h"
#include "scg_test.pb.h"

int main(int argc, char* argv[]) {
    // Verify that the version of the library that we linked against is
    // compatible with the version of the headers we compiled against.
    GOOGLE_PROTOBUF_VERIFY_VERSION;

    std::string input_file = "scg_test.pb";
    if (argc > 1) {
        input_file = argv[1];
    }

    // Read the binary protobuf file
    std::ifstream ifs(input_file, std::ios::binary);
    if (!ifs.is_open()) {
        std::cerr << "Error: Could not open file: " << input_file << std::endl;
        std::cerr << "Usage: " << argv[0] << " [path/to/scg_test.pb]" << std::endl;
        return 1;
    }

    std::ostringstream buf;
    buf << ifs.rdbuf();
    std::string raw_bytes = buf.str();

    // Deserialize the binary data into our TestMessage
    SCG::Test::TestMessage message;
    if (!message.ParseFromString(raw_bytes)) {
        std::cerr << "Error: Failed to parse protobuf message from file: " << input_file << std::endl;
        return 1;
    }

    // Convert the message to JSON using protobuf's JsonPrintOptions
    google::protobuf::util::JsonPrintOptions options;
    options.add_whitespace = true;
    options.always_print_fields_with_no_presence = true;
    options.preserve_proto_field_names = false;  // use camelCase (standard JSON)

    std::string json_output;
    auto status = google::protobuf::util::MessageToJsonString(message, &json_output, options);
    if (!status.ok()) {
        std::cerr << "Error: Failed to convert message to JSON: "
                  << status.message() << std::endl;
        return 1;
    }

    std::cout << "=== SCG Test Protobuf Message (JSON) ===" << std::endl;
    std::cout << json_output << std::endl;

    // Optional: also write to a file
    std::string output_file = "scg_test_output.json";
    std::ofstream ofs(output_file);
    if (ofs.is_open()) {
        ofs << json_output;
        std::cout << "Output also written to: " << output_file << std::endl;
    }

    // Clean up
    google::protobuf::ShutdownProtobufLibrary();

    return 0;
}
