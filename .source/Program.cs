using System;
using System.Windows.Forms;

namespace YouTubeDownloadUtil
{
  /// <summary>
  /// Class with program entry point.
  /// </summary>
  internal sealed class Program
  {
    
    public const string FFmpeg = @"C:\DEV\avcvt-utils\ffmpeg-20180217-dd8351b-win64-static\bin\";
    public const string YoutubeDL = @"C:\Users\xo\Desktop\youtube-dl-win\bin\";
    /// <summary>
    /// Program entry point.
    /// </summary>
    [STAThread]
    private static void Main(string[] args)
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new MainForm());
    }
    
  }
}
