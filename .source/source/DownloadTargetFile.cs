using System;

namespace YouTubeDownloadUtil
{
  class DownloadFile
  {
    class XClient : System.Net.WebClient
    {
      public System.Collections.Generic.List<string> LocalHeaders;
      protected override System.Net.WebResponse GetWebResponse(System.Net.WebRequest request)
      {
        var result = base.GetWebResponse(request);
        LocalHeaders = new System.Collections.Generic.List<string>();
        foreach (var header in ResponseHeaders.AllKeys)
        {
          var crh = ResponseHeaders[header];
          Headers.Add($"{header}: {crh}");
        }
        return result;
      }
    }
    static public System.Collections.Generic.IList<string> Go(string targetUri, string destFile)
    {
      var Headers = new System.Collections.Generic.List<string>();
      var client = new XClient
      {
        UseDefaultCredentials = true,
      };
      client.Headers["user-agent"] = @"Mozilla/5.0 (X11; Linux x86_64; rv:59.0) Gecko/20100101 Firefox/59.0 (Chrome)";
      client.DownloadFile(new Uri(targetUri),destFile.FileRelativeToExe());
      // note the lack of error handling.
      client.DownloadFileCompleted += (e, a) => client.Dispose();
      return Headers;
    }
  }
  static class DownloadHelpers
  {
    static public void Extract(this DownloadTargetFile target, string workpath, string outpath)
    {
      var si = new System.Diagnostics.ProcessStartInfo
      {
        FileName = "7za",
        WorkingDirectory = workpath,
        Arguments = @"x -o ""{outpath}"" ""{target.FileName}""",
      };
      System.Diagnostics.Process.Start(si);
    }
  }
  /// <summary>
  /// So what is it that we would like to accomplish here?
  /// 
  /// 1. download the file to a given directory-1. 
  /// 2. extract the file to a given directory-2. 
  /// 3. add whatever in the extracted content is our 'bin'.
  /// </summary>
  class DownloadTargetFile
  {
    public string Title          { get; set; }    // friendly name for printing to UI
    public string FileName       { get; set; } // file is downloaded to.
    public string FileURI        { get; set; }  // http://.....
    public string FileURI_x86_64 { get; set; } // in case we prefer
    public string BinPath        { get; set; } // where (target) binaries are located
    public bool IsArchive        { get; set; } // weather or not we extract the file or just keep as-is.
    public bool Hasx64           { get { return !string.IsNullOrEmpty(FileURI_x86_64); } }


    static string DirectoryTempFiles { get { return "%TEMP%".EnvironmentPathFilter(); } }
    static string TemporaryDownloads { get { return System.IO.Path.Combine(DirectoryTempFiles, "youtube-dl-temp").MkDirIfNotExist(); } }
    static readonly DownloadTargetFile dl7z, dlWGet, dlYtDl, dlAC, dlAP;
    static readonly System.Collections.Generic.List<DownloadTargetFile> KnownDownloads = new System.Collections.Generic.List<DownloadTargetFile>
    {
      (dlAP   = new DownloadTargetFile{Title="AtomicParsley v-0.9.0", FileName=@"AtomicParsley-win32-0.9.0.zip", FileURI=@"https://phoenixnap.dl.sourceforge.net/project/atomicparsley/atomicparsley/AtomicParsley%20v0.9.0/AtomicParsley-win32-0.9.0.zip"}),
      (dlWGet = new DownloadTargetFile{Title="wget v1.11.4-1", FileName=@"wget-1.11.4-1-bin.zip", FileURI=@"https://phoenixnap.dl.sourceforge.net/project/gnuwin32/wget/1.11.4-1/wget-1.11.4-1-bin.zip"}),
      (dlYtDl = new DownloadTargetFile{Title="youtube-dl", FileName=@"youtube-dl.exe", FileURI=@"https://yt-dl.org/latest/youtube-dl.exe", BinPath=""}),
      (dlAC   = new DownloadTargetFile{Title="libav / aconv v11.7 LGPL", FileName=@"libav-x86_64-w64-mingw32-20180108.7z", FileURI=@"http://builds.libav.org/windows/nightly-lgpl/libav-x86_64-w64-mingw32-20180108.7z", BinPath="libav-x86_64-w64-mingw32-11.7", FileURI_x86_64=@"http://builds.libav.org/windows/nightly-lgpl/libav-i686-w64-mingw32-20180108.7z"}),
      (dl7z   = new DownloadTargetFile{Title="7za v9.2.0", FileName=@"7za920.zip", FileURI=@"https://www.7-zip.org/a/7za920.zip", BinPath=""}),
      //new DownloadTargetFile{Title="", FileName=@"", FileURI=@""},
    };
    internal static void TestDownloadAtomicParsley(IUI f)
    {
      var headers = DownloadFile.Go("https://phoenixnap.dl.sourceforge.net/project/atomicparsley/atomicparsley/AtomicParsley%20v0.9.0/AtomicParsley-win32-0.9.0.zip", "wget-1.11.4-1-bin.zip");
      f.OutputRTF.Clear();
      var output = string.Join("\\n", (headers as System.Collections.Generic.List<string>).ToArray());
      f.OutputRTF.AppendText(output);
    }
  }
}
