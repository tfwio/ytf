using System;
namespace YouTubeDownloadUtil
{
  static class Actions
  {
    static readonly object L = new object();
    
    internal static Action ExploreToPath { get; } =()=> {
      lock (L) {
        if (ConfigModel.Instance.TargetOutputDirectory.Contains("start:")) System.Diagnostics.Process.Start("start",KeyStrings.ExploreToPath.Replace( "$path$", ConfigModel.Instance.TargetOutputDirectory.Replace("start:","") ));
        else System.Diagnostics.Process.Start("explorer.exe",KeyStrings.ExploreToPath.Replace( "$path$", ConfigModel.Instance.TargetOutputDirectory.EnvironmentPathFilter() ));
      };
    };
//    internal static Action ExploreTo { get; } =()=> { lock (L) { System.Diagnostics.Process.Start("explorer.exe",KeyStrings.ExploreToPath.Replace( "$path$", ConfigModel.Instance.TargetOutputDirectory.EnvironmentPathFilter() )); }; };
    internal static Func<string> RtfHelpText { get; } =()=> { using (var rs= System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("YouTubeDownloadUtil.readme.rtf")) using (var sr=new System.IO.StreamReader(rs)) return sr.ReadToEnd(); };
  }
  
  static class KeyStrings
  {
    internal const string UserDownloads = "%USERPROFILE%\\Downloads";
    internal const string NotLikely     = "bin";
    internal const string ShowHelp      = @"A helper for running youtube-dl";
    internal const string ConfDefault   = "config.ini";
    internal const string ExploreToPath = @"/e,""$path$"""; // for CommandKeyHandler Ctrl+E
    internal const string ExploreToFile = @"/e,/select,""$path$""";
  }
}


