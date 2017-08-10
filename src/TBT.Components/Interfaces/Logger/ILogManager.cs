using System;

namespace TBT.Components.Interfaces.Logger
{
    public interface ILogManager
    {
        void Debug(object value);
        void Debug(string message);
        void Debug(string message, object argument);
        void Debug(string message, params object[] args);
        void Debug(Exception exception, string message, params object[] args);
        void Error(object value);
        void Error(string message);
        void Error(string message, object argument);
        void Error(string message, params object[] args);
        void Error(Exception exception, string message, params object[] args);
        void Fatal(object value);
        void Fatal(string message);
        void Fatal(string message, object argument);
        void Fatal(string message, params object[] args);
        void Fatal(Exception exception, string message, params object[] args);
        void Info(object value);
        void Info(string message);
        void Info(string message, object argument);
        void Info(string message, params object[] args);
        void Info(Exception exception, string message, params object[] args);
        void Trace(object value);
        void Trace(string message);
        void Trace(string message, object argument);
        void Trace(string message, params object[] args);
        void Trace(Exception exception, string message, params object[] args);
        void Warn(object value);
        void Warn(string message);
        void Warn(string message, object argument);
        void Warn(string message, params object[] args);
        void Warn(Exception exception, string message, params object[] args);
    }
}
