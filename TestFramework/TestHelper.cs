using System;
using System.IO;
using NUnit.Framework;

namespace AssignmentTests {
    public static class TestHelper {
        private static string GetTestRoot() {
            return Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(TestContext.CurrentContext.TestDirectory)),
                "Inputs");
        }

        public delegate void IOProgram();

        public static void TryTest(string test, IOProgram program) {
            TestContext.Out.WriteLine($"Testing using: {test}");
            // Read input & output
            var testFile = Path.Combine(GetTestRoot(), test);
            string input, output;
            try {
                input = File.ReadAllText($"{testFile}.in");
                output = File.ReadAllText($"{testFile}.out");
            } catch (Exception e) {
                throw new Exception("Could not read test input files. Make sure you have both *.in and *.out");
            }

            // Pipe the outputs
            Console.SetIn(new StringReader(input));
            using (var sw = new StringWriter()) {
                Console.SetOut(sw);

                // Run program
                program.Invoke();

                // Trim outputs
                var programOutput = sw.ToString().TrimEnd();
                output = output.TrimEnd();

                if (output != programOutput) {
                    TestContext.Out.WriteLine("Wrong output!");
                    TestContext.Out.WriteLine($"Expected: \n{output}\n\nGot: \n{programOutput}");
                    Assert.Fail("Your program is shit! See below");
                }
            }
        }
    }
}