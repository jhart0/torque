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
            Assert.AreEqual("torque Database Comparison & Synchronization Tool\r\n\r\n\r\nAvailable Commands:\r\ncompare (compare 2 db schemas):\r\ncompare <dbms> <from connection string> <to connection string>\r\nsync (sync 2 db schemas):\r\nsync <dbms> <from connection string> <to connection string>\r\ndeploy (deploy script to target):\r\ndeploy <dbms> <to connection string> <script to run>\r\n", sb.ToString());
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