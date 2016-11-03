using System;
using ConcurrentLogger;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConcurrentLoggerTests
{
    [TestClass]
    public class UnitTest1
    {
        public enum LogLevel
        {
            Debug,
            Info,
            Warning,
            Error
        }
        [TestMethod]
        public void TestCreation()
        {
            Target loggerFile = new Target("LoggerFile.txt");
            ILoggerTarget[] loggerTargets = new ILoggerTarget[] { loggerFile };
            int limit = 10;
            Logger logger = new Logger(limit, loggerTargets);
            Assert.AreEqual(limit, logger.bufferLimit);
        }

        [TestMethod]
        public void TestLoggerLog()
        {
            Target loggerFile = new Target("LoggerFile.txt");
            ILoggerTarget[] loggerTargets = new ILoggerTarget[] { loggerFile };
            int limit = 2;
            Logger logger = new Logger(limit, loggerTargets);
            for (int i = 1; i <= 10; i++)
            {
                logger.Log((ConcurrentLogger.Program.LogLevel)LogLevel.Info, DateTime.Now.ToString() + " " + i.ToString());
            }
            Assert.AreEqual(9, logger.threads);
        }

        [TestMethod]
        public void TestLoggerResult()
        {
            Target loggerFile = new Target("LoggerFile.txt");
            ILoggerTarget[] loggerTargets = new ILoggerTarget[] { loggerFile };
            int limit = 2;
            Logger logger = new Logger(limit, loggerTargets);
            for (int i = 1; i <= 10; i++)
            {
                logger.Log((ConcurrentLogger.Program.LogLevel)LogLevel.Info, DateTime.Now.ToString() + " " + i.ToString());
            }
            Assert.AreEqual(9, logger.threads);
        }

    }
}
