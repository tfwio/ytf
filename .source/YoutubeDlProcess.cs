using System;
using System.Diagnostics;
namespace YouTubeDownloadUI
{
  
  class YoutubeDownloader
  {
    #region Private
    

    string HasPL { get { return GetPlaylist ? "--yes-playlist" : ""; } }
    
    Process yt_process;
    
    #endregion
    
    public bool IsVerbose { get; set; } = true;
    public bool UrlHasPlaylist { get { return this.UriInput.Contains("&list"); } }
    public bool GetPlaylist { get; set; } = false;
    
    public string OutputPath { get; set; }
    
    /// <summary>
    /// Set to m4a, any or mp4 among others.
    /// </summary>
    public string TargetType { get; set; }
    /// <summary>
    /// the youtube url.
    /// </summary>
    public string UriInput { get; set; }
    
    public string UriInputFiltered {
      get
      {
        return UriInput
          .Replace("&","^&")
          ;
      }
    }
    
    public string CommandText {
      get {
        return $"-c -f {TargetType}" +
          " --write-sub" +
          $" {HasPL}" +
          " --sub-lang en" +// skip
          " --write-auto-sub" +// skip
          " --embed-subs" +// skip
          " --embed-thumbnail" +
          " --add-metadata" +
          " --verbose" +
          $" \"{UriInputFiltered}\"";
      }
    }
    
    public bool HasDownloader { get { return yt_process != null; } }
    
    ProcessStartInfo NewStartInfo {
      get {
        var si = new ProcessStartInfo("youtube-dl",CommandText){
          UseShellExecute = false,
//          StandardOutputEncoding = Encoding.UTF8,
//          StandardErrorEncoding = Encoding.UTF8,
          RedirectStandardError = true,
          RedirectStandardOutput = true,
          CreateNoWindow = true,
          WorkingDirectory = OutputPath
        };
        // si.RedirectStandardInput = false;
        return si;
      }
    }
    public YoutubeDownloader(
      string url,
      string output,
      DataReceivedEventHandler onOutput,
      DataReceivedEventHandler onError,
      EventHandler onCompleted)
    {
      OutputPath = output;
      UriInput = url;
      yt_process = new Process(){
        StartInfo = NewStartInfo,
        EnableRaisingEvents = true
      };
      yt_process.OutputDataReceived += onOutput;
      yt_process.ErrorDataReceived += onError;
      yt_process.Exited += WeDone;
      yt_process.Disposed += WeDisposed;
      Completed += onCompleted;
    }
    public void Go(){
      yt_process.StartInfo = NewStartInfo;
      yt_process.Start();
      yt_process.BeginOutputReadLine();
      yt_process.BeginErrorReadLine();
      yt_process.WaitForExit();
    }
    
    public void Abort()
    {
      yt_process.Close();
    }
    
    public event EventHandler Completed;
    protected virtual void OnCompleted()
    {
      var handler = Completed;
      if (handler != null) handler(this, EventArgs.Empty);
    }
    
    public int ExitCode { get; set; } = 0;
    
    void WeDone(object sender, EventArgs e) {
      try {
        ExitCode = yt_process.ExitCode;
        Console.Write("[app]: {0}\n", ExitCode);
        yt_process.Dispose();
      }
      catch {}
    }
    
    void WeDisposed(object sender, EventArgs e) {
//      yt_process = null;
      ExitCode = yt_process.ExitCode;
      OnCompleted();
    }
    
  }
}


