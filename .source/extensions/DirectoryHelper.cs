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
    /// Replace environment variables (e.g. "%PATH%") with
    /// their corresponding value.
    /// </summary>
    /// <param name="input">string to filter.</param>
    /// <returns>string result</returns>
    static public string EnvironmentPathFilter(this string input) {
      string result = input;
      var vars = Environment.GetEnvironmentVariables();
      foreach (var v in vars.Keys) {
        var ukey = v.ToString().ToUpper();
        var lkey = v.ToString().ToLower();
        var value = vars[v];
        result = result
          .Replace($"%{ukey}%", value.ToString())
          .Replace($"%{lkey}%", value.ToString())
          .Replace($"%{v}%", value.ToString())
          ;
        if (!result.Contains("%")) break;
      }
      return result;
    }
    
    /// <summary>
    /// Create directory if not exist relative to the calling application.
    /// </summary>
    /// <param name="dirName">directory-name</param>
    /// <returns>A full path to the target Directory.</returns>
    static public string RelativeToExe(this string dirName) { var dirTarget = Path.Combine(ExecutableDirectory, dirName); if (!Directory.Exists(dirTarget)) Directory.CreateDirectory(dirTarget); return dirTarget; }
    
  }
}




