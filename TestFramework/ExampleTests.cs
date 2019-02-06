using System;
using NUnit.Framework;

namespace AssignmentTests {
    public static class ExampleProgram {
        public static void Run() {
            var input = Console.ReadLine(); // Not used but works
            Console.WriteLine("Hello World!");
        }
    }
    
    [TestFixture]
    public class ExampleTests {
        [TestCase("example1")]
        [TestCase("example2")]
        public void RunTests(string test) {
            TestHelper.TryTest(test, ExampleProgram.Run);
        }
    }
}