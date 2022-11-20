//-----------------------------------------------------------------------------------
// WRITING MESSAGES TO A LOG FILE

using System;
using System.IO;
using System.Text;

namespace ipcampic
{
	public class Log
	{
		private static object sync = new object();
		public static void WR(string message)
		{
			try
			{
				// Creating a Path to Write a Log File
				string pathToLog = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, gVar.Dlog);
				// Create directory if it doesn't exist
				if (!Directory.Exists(pathToLog)) Directory.CreateDirectory(pathToLog);
                // Create a log file name
                string filename = Path.Combine(pathToLog, string.Format("{0}.log","ipcampic"));
                if (gVar.Elog > 0)
                {
                    filename = Path.Combine(pathToLog, string.Format("{0}_{1:yyyMMdd}.log", gVar.Plog, DateTime.Now));
                }
				// Create a message to write to the log file
				string fullText = string.Format("[{0:yyy.MM.dd HH:mm:ss.fff}] {1}\r\n", DateTime.Now, message);
				// Write message to log file
				lock(sync)
				{
				//	File.AppendAllText(filename, fullText, Encoding.GetEncoding("Windows-1251"));
					File.AppendAllText(filename, fullText, Encoding.GetEncoding("UTF-8"));
				}
			}
			catch { ;; }
		}
	}
}
