using NLog;
using System;
using TBT.Components.Interfaces.Logger;

namespace TBT.Components.Implementations.Logger
{
    public class LogManager : NLog.Logger, ILogManager
    {
        private ILogger _logger;

        public LogManager(string loggerName)
        {
            _logger = NLog.LogManager.GetLogger(loggerName);
        }

        public new void Debug(object value)
        {
            _logger.Debug(value);
        }

        public new void Debug(string message)
        {
            _logger.Debug(message);
        }

        public new void Debug(string message, object argument)
        {
            _logger.Debug(message, argument);
        }

        public new void Debug(string message, params object[] args)
        {
            _logger.Debug(message, args);
        }

        public new void Debug(Exception exception, string message, params object[] args)
        {
            _logger.Debug(exception, message, args);
        }

        public new void Error(object value)
        {
            _logger.Error(value);
        }

        public new void Error(string message)
        {
            _logger.Error(message);
        }

        public new void Error(string message, object argument)
        {
            _logger.Error(message, argument);
        }

        public new void Error(string message, params object[] args)
        {
            _logger.Error(message, args);
        }

        public new void Error(Exception exception, string message, params object[] args)
        {
            _logger.Error(exception, message, args);
        }

        public new void Fatal(object value)
        {
            _logger.Fatal(value);
        }

        public new void Fatal(string message)
        {
            _logger.Fatal(message);
        }

        public new void Fatal(string message, object argument)
        {
            _logger.Fatal(message, argument);
        }

        public new void Fatal(string message, params object[] args)
        {
            _logger.Fatal(message, args);
        }

        public new void Fatal(Exception exception, string message, params object[] args)
        {
            _logger.Fatal(exception, message, args);
        }

        public new void Info(object value)
        {
            _logger.Info(value);
        }

        public new void Info(string message)
        {
            _logger.Info(message);
        }

        public new void Info(string message, object argument)
        {
            _logger.Info(message, argument);
        }

        public new void Info(string message, params object[] args)
        {
            _logger.Info(message, args);
        }

        public new void Info(Exception exception, string message, params object[] args)
        {
            _logger.Info(exception, message, args);
        }

        public new void Trace(object value)
        {
            _logger.Trace(value);
        }

        public new void Trace(string message)
        {
            _logger.Trace(message);
        }

        public new void Trace(string message, object argument)
        {
            _logger.Trace(message, argument);
        }

        public new void Trace(string message, params object[] args)
        {
            _logger.Trace(message, args);
        }

        public new void Trace(Exception exception, string message, params object[] args)
        {
            _logger.Trace(exception, message, args);
        }

        public new void Warn(object value)
        {
            _logger.Warn(value);
        }

        public new void Warn(string message)
        {
            _logger.Warn(message);
        }

        public new void Warn(string message, object argument)
        {
            _logger.Warn(message, argument);
        }

        public new void Warn(string message, params object[] args)
        {
            _logger.Warn(message, args);
        }

        public new void Warn(Exception exception, string message, params object[] args)
        {
            _logger.Warn(exception, message, args);
        }
    }
}
