using System;
using System.Diagnostics;
using YtFlag=YouTubeDownloadUtil.YoutubeDlFlags;
namespace YouTubeDownloadUtil
{
  class YoutubeDownloader : DownloadTarget
  {
    Process shellProcess;
    
    public bool Aborted { get; private set; }
    
    //short
    string StrIgnoreErrors { get { return Flags.HasFlag(YtFlag.IgnoreErrors) ? $"-i" : string.Empty; } }
    string StrContinue { get { return Flags.HasFlag(YtFlag.Continue) ? $"-c" : string.Empty; } }
    //common
    string StrAddMetaData { get { return Flags.HasFlag(YtFlag.AddMetadata) ? $"--add-metadata" : string.Empty; } }
    string StrEmbedThumbnail { get { return Flags.HasFlag(YtFlag.EmbedThumb) ? $"--embed-thumbnail" : string.Empty; } }
    string StrPlaylist { get { return Flags.HasFlag(YtFlag.GetPlaylist) ? "--yes-playlist" : "--no-playlist"; } }
    string StrTargetType { get { return HasTargetType ? $"-f {TargetType}" : string.Empty; } }
    string StrVerbose { get { return Flags.HasFlag(YtFlag.Verbose) ? $"--verbose" : string.Empty; } }
    // subs
    string StrEmbedSubs { get { return Flags.HasFlag(YtFlag.EmbedSubs) ? "--embed-subs" : string.Empty; } }
    string StrSubLang { get { return !string.IsNullOrEmpty(SubLang) ? " --sub-lang {SubLang}" : string.Empty; } }
    string StrWriteAutoSub { get { return Flags.HasFlag(YtFlag.WriteAutoSubs) ? "--write-auto-sub" : string.Empty; } }
    string StrWriteSub { get { return Flags.HasFlag(YtFlag.WriteSubs) ? "--write-sub" : string.Empty; } }
    
    public string CommandText { get { return $"{StrIgnoreErrors} {StrContinue} {StrTargetType} {StrWriteSub} {StrPlaylist} {StrSubLang} {StrWriteAutoSub} {StrEmbedSubs} {StrEmbedThumbnail} {StrAddMetaData} {StrVerbose} \"{TargetUri}\""; } }
    
    ProcessStartInfo NewStartInfo {
      get {
        // only reason we're not using this is the RTF box
        // std-out is directed to.
        // StandardOutputEncoding = Encoding.UTF8,
        // StandardErrorEncoding = Encoding.UTF8,
        var si = new ProcessStartInfo("youtube-dl",CommandText){
          UseShellExecute = false,
          RedirectStandardError = true,
          RedirectStandardOutput = true,
          CreateNoWindow = true,
          WorkingDirectory = TargetPath.EnvironmentPathFilter()
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
    
    const int Win32_ErrorCode = -2147467259;
    const int Win32Native_NoExecutable = 2;
    const string Win32Native_NoExecutable_Msg = "youtube-dl (or target executable) wasn't found.\n<to-fix> Drag-Drop youtube-dl.exe back onto the app for it to be configured properly.";
    const int Win32Native_NoWorkingDirectory = 267;
    const string Win32Native_NoWorkingDirectory_Msg = "The target-directory (WorkPath) wasn't found.\n<to-fix> Select another target (output) path.";
    const string msgLauchError = "<app> There was a Win32Exception (error)...\n";
    
    public void Go(){
      shellProcess.StartInfo = NewStartInfo;
      shellProcess.Exited += WeDone;
      shellProcess.Disposed += WeDisposed;
      try {
        shellProcess.Start();
      }
      catch (System.ComponentModel.Win32Exception w32err)
      {
        switch (w32err.NativeErrorCode)
        {
          case Win32Native_NoExecutable:
            Aborted = true;
            AbortMessage = $"{msgLauchError}<ErrorCode> {w32err.ErrorCode}, <NativeErrorCode> {w32err.NativeErrorCode}\n<DOTNET/Exception>{w32err.Message}\n{Win32Native_NoExecutable_Msg}\n";
            return;
          case Win32Native_NoWorkingDirectory:
            Aborted = true;
            AbortMessage = $"{msgLauchError}<ErrorCode> {w32err.ErrorCode}, <NativeErrorCode> {w32err.NativeErrorCode}\n<DOTNET/Exception>{w32err.Message}\n{Win32Native_NoWorkingDirectory_Msg}\n";
            return;
          default:
            throw w32err;
        }
      }
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


