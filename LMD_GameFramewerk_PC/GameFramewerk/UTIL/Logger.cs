using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LMD_GameFramewerk_PC.GameFramewerk.UTIL
{
	public static class Logger
	{
		public const String PATH_LOG = "Logs/";

		public static void Log(String value)
		{
			String nameFile = GetCurrentName();
			ExistFolderLog(nameFile);

			using (Stream stream = new FileStream(PATH_LOG + nameFile, FileMode.Append))
			{
				StreamWriter writer = new StreamWriter(stream, Encoding.UTF8);
				writer.WriteLine("LOG["+System.DateTime.Now.ToShortTimeString()+"]: " + value);
				writer.Close();
				stream.Close();
			}
		}
		public static void Info(String value)
		{
			String nameFile = GetCurrentName();
			ExistFolderLog(nameFile);

			using (Stream stream = new FileStream(PATH_LOG + nameFile, FileMode.Append))
			{
				StreamWriter writer = new StreamWriter(stream, Encoding.UTF8);
				writer.WriteLine("INFO[" + System.DateTime.Now.ToShortTimeString() + "]: " + value);
				writer.Close();
				stream.Close();
			}
		}
		public static void Error(Exception ex)
		{
			String nameFile = GetCurrentName();
			ExistFolderLog(nameFile);

			using (Stream stream = new FileStream(PATH_LOG + nameFile, FileMode.Append))
			{
				StreamWriter writer = new StreamWriter(stream, Encoding.UTF8);
				writer.WriteLine("ERROR[" + System.DateTime.Now.ToShortTimeString() + "]: " + ex);
				writer.Close();
				stream.Close();
			}
		}
		public static void Debug(String value)
		{
			String nameFile = GetCurrentName();
			ExistFolderLog(nameFile);

			using (Stream stream = new FileStream(PATH_LOG + nameFile, FileMode.Append))
			{
				StreamWriter writer = new StreamWriter(stream, Encoding.UTF8);
				writer.WriteLine("DEBUG[" + System.DateTime.Now.ToShortTimeString() + "]: " + value);
				writer.Close();
				stream.Close();
			}
		}
		private static void ExistFolderLog(String nameFile)
		{
			if (!Directory.Exists(PATH_LOG))
			{
				Directory.CreateDirectory(PATH_LOG);
			}
			if (!File.Exists(PATH_LOG + nameFile))
			{
				File.Create(PATH_LOG + nameFile).Close();
			}
		}
		public static String GetCurrentName()
		{
			return System.DateTime.Now.ToShortDateString().Replace(".", "_") + ".log";
        }
	}
}
