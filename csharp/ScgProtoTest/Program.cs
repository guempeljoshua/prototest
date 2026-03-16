using System;
using System.IO;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using SCG.Test;

namespace ScgProtoTest
{
    class Program
    {
        static int Main(string[] args)
        {
            string inputFile = "scg_test.pb";
            if (args.Length > 0)
            {
                inputFile = args[0];
            }

            if (!File.Exists(inputFile))
            {
                Console.Error.WriteLine($"Error: Could not find file: {inputFile}");
                Console.Error.WriteLine($"Usage: ScgProtoTest [path/to/scg_test.pb]");
                return 1;
            }

            // Read the binary protobuf file
            byte[] rawBytes;
            try
            {
                rawBytes = File.ReadAllBytes(inputFile);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error reading file '{inputFile}': {ex.Message}");
                return 1;
            }

            // Deserialize the binary data into our TestMessage
            TestMessage message;
            try
            {
                message = TestMessage.Parser.ParseFrom(rawBytes);
            }
            catch (InvalidProtocolBufferException ex)
            {
                Console.Error.WriteLine($"Error: Failed to parse protobuf message: {ex.Message}");
                return 1;
            }

            // Convert to JSON using protobuf's built-in JSON formatter
            // JsonFormatter.Default uses camelCase field names and RFC 3339 timestamps
            var formatter = new JsonFormatter(new JsonFormatter.Settings(true)
                .WithFormatDefaultValues(true)    // include fields with default/zero values
                .WithIndentation());              // pretty-print

            string jsonOutput;
            try
            {
                jsonOutput = formatter.Format(message);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error converting to JSON: {ex.Message}");
                return 1;
            }

            Console.WriteLine("=== SCG Test Protobuf Message (JSON) ===");
            Console.WriteLine(jsonOutput);

            // Also write to a file alongside the binary
            string outputFile = "scg_test_output.json";
            try
            {
                File.WriteAllText(outputFile, jsonOutput);
                Console.WriteLine($"Output also written to: {outputFile}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Warning: Could not write output file: {ex.Message}");
            }

            return 0;
        }
    }
}
