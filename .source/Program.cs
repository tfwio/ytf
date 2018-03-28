using System;
using System.Windows.Forms;

namespace YouTubeDownloadUtil
{
  /// <summary>
  /// Class with program entry point.
  /// </summary>
  internal sealed class Program
  {
    
    /// <summary>
    /// Program entry point.
    /// </summary>
    [STAThread]
    private static void Main(string[] args)
    {
      var penv = System.Environment.GetEnvironmentVariable("PATH");
      System.Environment.SetEnvironmentVariable("PATH",$"{KeyStrings.FFmpeg};{KeyStrings.YoutubeDL};{penv}");
      
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new MainForm());
    }
    
  }
}
