using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace AspClaysysPrjct
{
    public class LogHelper
    {
        // Path to store log file
        private static readonly string logFilePath = HttpContext.Current.Server.MapPath("~/App_Data/ErrorLog.txt");

        public static void LogError(string message, Exception ex)
        {
            try
            {
                // Ensure the directory exists
                var logDirectory = Path.GetDirectoryName(logFilePath);
                if (!Directory.Exists(logDirectory))
                {
                    Directory.CreateDirectory(logDirectory);
                }

                // Log format
                string logEntry = $"[{DateTime.Now}] ERROR: {message}{Environment.NewLine}Exception: {ex}{Environment.NewLine}{Environment.NewLine}";

                // Append error to the log file
                File.AppendAllText(logFilePath, logEntry);
            }
            catch (Exception logEx)
            {
                // In case logging fails, handle it appropriately (e.g., alert admin via email)
                Console.WriteLine($"Logging error failed: {logEx.Message}");
            }
        }
    }
}