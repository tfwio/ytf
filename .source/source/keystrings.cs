using System;
namespace YouTubeDownloadUtil
{
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


