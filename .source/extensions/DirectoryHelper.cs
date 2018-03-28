using System;
using System.IO;
namespace System
{
	static class DirectoryHelper
	{
	  
    static public readonly System.IO.FileInfo ExecutableFileInfo = new System.IO.FileInfo(System.Windows.Forms.Application.ExecutablePath);
    static public System.IO.DirectoryInfo ExecutableDirectoryInfo { get { return ExecutableFileInfo.Directory; } }
    static public string ExecutableDirectory { get { return ExecutableDirectoryInfo.FullName; } }
    
    
    /// <summary>
    /// Create directory if not exist relative to the calling application.
    /// </summary>
    /// <param name="dirName">directory-name</param>
    /// <returns>A full path to the target Directory.</returns>
    static public string EnsureLocalDirectory(string dirName)
    {
      var dirTarget = Path.Combine(ExecutableDirectory, dirName);
      if (!Directory.Exists(dirTarget))
        Directory.CreateDirectory(dirTarget);
      return dirTarget;
    }
    
//    static DirectoryHelper()
//    {
//      ExecutableFileInfo = new System.IO.FileInfo(System.Windows.Forms.Application.ExecutablePath);
//    }
	}
}




