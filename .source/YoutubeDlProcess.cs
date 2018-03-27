using System;
using System.Diagnostics;
namespace YouTubeDownloadUI
{
  class YoutubeDownloader : DownloadTarget
  {
    Process shellProcess;
    
    public bool Aborted { get; private set; }
    
    //short
    string StrIgnoreErrors { get { return IgnoreErrors ? $"-i" : string.Empty; } }
    string StrContinue { get { return Continue ? $"-c" : string.Empty; } }
    //common
    string StrAddMetaData { get { return AddMetaData ? $"--add-metadata" : string.Empty; } }
    string StrEmbedThumbnail { get { return EmbedThumbnail ? $"--embed-thumbnail" : string.Empty; } }
    string StrPlaylist { get { return GetPlaylist ? "--yes-playlist" : string.Empty; } }
    string StrTargetType { get { return HasTargetType ? $"-f {TargetType}" : string.Empty; } }
    string StrVerbose { get { return Verbose ? $"--verbose" : string.Empty; } }
    // subs
    string StrEmbedSubs { get { return EmbedSubs ? "--embed-subs" : string.Empty; } }
    string StrSubLang { get { return !string.IsNullOrEmpty(SubLang) ? " --sub-lang {SubLang}" : string.Empty; } }
    string StrWriteAutoSub { get { return WriteAutoSub ? "--write-auto-sub" : string.Empty; } }
    string StrWriteSub { get { return WriteSub ? "--write-sub" : string.Empty; } }
    
    public string CommandText { get { return $"{StrIgnoreErrors} {StrContinue} {StrTargetType} {StrWriteSub} {StrPlaylist} {StrSubLang} {StrWriteAutoSub} {StrEmbedSubs} {StrEmbedThumbnail} {StrAddMetaData} {StrVerbose} \"{UriFiltered}\""; } }
    
    ProcessStartInfo NewStartInfo {
      get {
        //          StandardOutputEncoding = Encoding.UTF8,
        //          StandardErrorEncoding = Encoding.UTF8,
        var si = new ProcessStartInfo("youtube-dl",CommandText){
          UseShellExecute = false,
          RedirectStandardError = true,
          RedirectStandardOutput = true,
          CreateNoWindow = true,
          WorkingDirectory = TargetPath
        };
        // si.RedirectStandardInput = false;
        return si;
      }
    }
    
    void InitEvents(
      DataReceivedEventHandler onOutput,
      DataReceivedEventHandler onError,
      EventHandler onCompleted)
    {
      shellProcess.OutputDataReceived += onOutput;
      shellProcess.ErrorDataReceived += onError;
      Completed += onCompleted;
    }
    
    public YoutubeDownloader(string url,string output)
    {
      TargetUri = url;
      TargetPath = output;
      shellProcess = new Process(){
        StartInfo = NewStartInfo,
        EnableRaisingEvents = true
      };
      ExitCode = 0;
    }
    
    public YoutubeDownloader(string url, string output, DataReceivedEventHandler onOutput, DataReceivedEventHandler onError, EventHandler onCompleted)
      : this(url,output)
    {
      InitEvents(onOutput, onError, onCompleted);
    }
    
    public void Go(){
      shellProcess.StartInfo = NewStartInfo;
      shellProcess.Exited += WeDone;
      shellProcess.Disposed += WeDisposed;
      shellProcess.Start();
      shellProcess.BeginOutputReadLine();
      shellProcess.BeginErrorReadLine();
      shellProcess.WaitForExit();
    }
    
    public void Abort(string msgAbort)
    {
      Aborted = true;
      AbortMessage = msgAbort;
      shellProcess.Kill();
      shellProcess.Close();
    }
    
    public event EventHandler Completed;
    protected virtual void OnCompleted()
    {
      var handler = Completed;
      if (handler != null) handler(this, EventArgs.Empty);
    }
    
    public int ExitCode { get; set; }
    
    void WeDone(object sender, EventArgs e) {
      try {
        ExitCode = shellProcess.ExitCode;
        Console.Write("[app]: {0}\n", ExitCode);
        shellProcess.Dispose();
      }
      catch {}
    }
    
    void WeDisposed(object sender, EventArgs e) {
      //      yt_process = null;
      ExitCode = shellProcess.ExitCode;
      OnCompleted();
    }
    
  }
}


