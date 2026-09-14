using System;
using System.IO;

namespace Calculator.Services
{
    public class FileCalculatorLogger : ICalculatorLogger
    {
        private readonly string _logPath;

        public FileCalculatorLogger(string logPath)
        {
            _logPath = logPath;
        }

        public void Log(string action)
        {
            try
            {
                string line = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {action}";
                File.AppendAllText(_logPath, line + Environment.NewLine);
            }
            catch { }
        }

        public void Clear()
        {
            try
            {
                File.WriteAllText(_logPath, string.Empty);
            }
            catch { }
        }
    }

    public static class CalculatorLogger
    {
        private static readonly ICalculatorLogger _logger = new FileCalculatorLogger(
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log.txt"));

        public static void Log(string action) => _logger.Log(action);

        public static void Clear() => _logger.Clear();
    }
}
