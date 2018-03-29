using System;
namespace YouTubeDownloadUtil
{
  static class Actions
  {
    static readonly object L = new object();
    
    internal static Action ExploreTo { get; } =()=> { lock (L) { System.Diagnostics.Process.Start("explorer.exe",KeyStrings.ExploreTo.Replace( "$path$", ConfigModel.Instance.TargetOutputDirectory )); }; };
    internal static Func<string> RtfHelpText { get; } =()=> { using (var rs= System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("YTdl-util.readme.rtf")) using (var sr=new System.IO.StreamReader(rs)) return sr.ReadToEnd(); };
  }
  
  static class KeyStrings
  {
    internal const string UserDownloads = "%USERPROFILE%\\Downloads";
    internal const string NotLikely     = "bin";
    internal const string ShowHelp      = @"A helper for running youtube-dl";
    internal const string ConfDefault   = "config.ini";
    internal const string ExploreTo     = @"/e,/select,""$path$"""; // for CommandKeyHandler Ctrl+E
  }
}


