using System;
using System.Windows.Forms;

namespace YouTubeDownloadUI
{
  /// <summary>
  /// Class with program entry point.
  /// </summary>
  internal sealed class Program
  {
    static public System.IO.FileInfo ExecutableFileInfo { get; set; }
    static public System.IO.DirectoryInfo ExecutableDirectoryInfo { get { return ExecutableFileInfo.Directory; } }
    static public string ExecutableDirectory { get { return ExecutableDirectoryInfo.FullName; } }
    
    /// <summary>
    /// Program entry point.
    /// </summary>
    [STAThread]
    private static void Main(string[] args)
    {
      var penv = System.Environment.GetEnvironmentVariable("PATH");
      System.Environment.SetEnvironmentVariable("PATH",$"{KeyStrings.FFmpeg};{KeyStrings.YoutubeDL};{penv}");
      ExecutableFileInfo = new System.IO.FileInfo(Application.ExecutablePath);
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new MainForm());
    }
    
  }
}
