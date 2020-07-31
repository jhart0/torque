using NUnit.Framework;
using System.Text;
using System.Threading.Tasks;
using torque.Common.Contracts.Services;

namespace torque.IntegrationTests.Postgres
{
    public class ArgumentParsingTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GivenHelpIsSpecifiedAsArg0_WhenProgramEntryPointIsCalled_HelpTextIsShown()
        {
            var sb = new StringBuilder();
            Program.loggingService = new TestLoggingService(sb);
            await Program.Main(new string[] { "help" });
            Assert.AreEqual($"torque Database Comparison & Synchronization Tool{Environment.NewLine}{Environment.NewLine}{Environment.NewLine}Available Commands:{Environment.NewLine}compare (compare 2 db schemas):{Environment.NewLine}compare <dbms> <from connection string> <to connection string>{Environment.NewLine}sync (sync 2 db schemas):{Environment.NewLine}sync <dbms> <from connection string> <to connection string>{Environment.NewLine}deploy (deploy script to target):{Environment.NewLine}deploy <dbms> <to connection string> <script to run>{Environment.NewLine}", sb.ToString());
        }
    }

    internal class TestLoggingService : ILoggingService
    {
        public StringBuilder _sb;

        public TestLoggingService(StringBuilder sb)
        {
            this._sb = sb;
        }

        public void Log(string line)
        {
            this._sb.AppendLine(line);
        }
    }
}