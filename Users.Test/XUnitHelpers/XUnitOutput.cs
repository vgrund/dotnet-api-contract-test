using System;
using System.IO;
using PactNet.Infrastructure.Outputters;
using Xunit.Abstractions;

namespace Users.Test.XUnitHelpers
{
    public class XUnitOutput : IOutput
    {
        private readonly ITestOutputHelper _output;

        public XUnitOutput(ITestOutputHelper output)
        {
            _output = output;
        }

        public void WriteLine(string line)
        {
            _output.WriteLine(line);
            Console.WriteLine(line);
            using StreamWriter file = new("WriteLines1.txt", append: true);
            file.WriteLine(line);
        }
    }
}