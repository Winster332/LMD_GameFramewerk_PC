using System;

namespace LMD_GameFramewerk_PC.GameFramewerk
{
	public interface IFileIO
	{
		void SetDataXMLFile(string path, object obj);
		object GetDataXMLFile(string path, Type type);
	}
}