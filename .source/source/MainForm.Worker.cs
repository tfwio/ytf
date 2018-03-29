using System;
using System.Collections.Generic;
using System.IO;

namespace YouTubeDownloadUtil
{
  class Downloader {
    
    public Action PrepareThread { get; set; }
    public Action<YoutubeDownloader> PreProcess { get; set; }
    public Action<YoutubeDownloader> PostProcess { get; set; }
    
    readonly System.Windows.Forms.Form Caller;
    
    System.ComponentModel.BackgroundWorker worker;
    YoutubeDownloader downloader;
    System.Threading.Thread thread;
    List<string> OutputData = new List<string>();

    void ThreadCompleted(object sender, EventArgs e) { worker.CancelAsync(); }
    
    void Begin()
    {
      if (worker!=null && worker.IsBusy) return;
      worker = new System.ComponentModel.BackgroundWorker();
      worker.DoWork += EventDoWork;
      worker.Disposed += WorkerEvent_Disposed;
      worker.RunWorkerCompleted += EventComplete;
      worker.WorkerSupportsCancellation = true;
      worker.WorkerReportsProgress = false;
      worker.RunWorkerAsync();
    }
    
    void EventDoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
    {
      thread = new System.Threading.Thread(PrepareThread.Invoke);
      thread.Start();
      while (thread.IsAlive) System.Threading.Thread.Sleep(500);
    }

    void EventComplete(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
    {
      if (Caller.InvokeRequired) Caller.Invoke(new Action(()=>PostProcess(downloader)));
      else PostProcess(downloader);
      worker.Dispose();
    }
    
    void WorkerEvent_Disposed(object sender, EventArgs e) { worker = null; }
  }
  partial class MainForm {
    
    void UI_WorkerProcess_PrepareThread()
    {
      var downloads = ConfigModel.Instance.TargetOutputDirectory;
      downloader = new YoutubeDownloader(
        textBox1.Text,
        downloads,
        WorkerThread_DataReceived,
        WorkerThread_ErrorReceived,
        WorkerThread_Completed
       ){
        TargetType = ConfigModel.Instance.TargetType,
        Verbose=ConfigModel.Instance.AppFlags.HasFlag(YoutubeDlFlags.Verbose),
        AbortOnDuplicate = ConfigModel.Instance.AppFlags.HasFlag(YoutubeDlFlags.AbortOnDuplicate),
        AddMetaData = ConfigModel.Instance.AppFlags.HasFlag(YoutubeDlFlags.AddMetadata),
        Continue=ConfigModel.Instance.AppFlags.HasFlag(YoutubeDlFlags.Continue),
        EmbedSubs= ConfigModel.Instance.AppFlags.HasFlag(YoutubeDlFlags.EmbedSubs),
        EmbedThumbnail=ConfigModel.Instance.AppFlags.HasFlag(YoutubeDlFlags.EmbedThumb),
        GetPlaylist=ConfigModel.Instance.AppFlags.HasFlag(YoutubeDlFlags.GetPlaylist),
        IgnoreErrors=ConfigModel.Instance.AppFlags.HasFlag(YoutubeDlFlags.IgnoreErrors),
        WriteAutoSub=ConfigModel.Instance.AppFlags.HasFlag(YoutubeDlFlags.WriteAutoSubs),
        WriteSub=ConfigModel.Instance.AppFlags.HasFlag(YoutubeDlFlags.WriteSubs),
      };
      
      if (InvokeRequired) Invoke(new Action(()=>UI_WorkerProcess_Pre(downloader)));
      else UI_WorkerProcess_Pre(downloader);
      
      downloader.Go();
    }
    
    void UI_WorkerThread_DataFilter(string text, YoutubeDownloader obj)
    {
      if (!string.IsNullOrEmpty(text) && text.Contains(msgDownloadDestination))
      {
        obj.KnownTargetFile = text.Replace(msgDownloadDestination, "").Trim();
        Text = obj.KnownTargetFile;
      }
      else if (!string.IsNullOrEmpty(text) && text.Contains(msgAllreadyDownloaded))
      {
        obj.KnownTargetFile = text
          .Replace(msgDownloadHeading, "")
          .Replace(msgAllreadyDownloaded, "");
        obj.Abort($"[abort] due to EXISITING FILE: {obj.KnownTargetFile}\n");
        Text=$"[EXISTS] {obj.KnownTargetFile}";
      }
    }
    
    void UI_WorkerThread_DataHandler(string data, bool isError, YoutubeDownloader obj)
    {
      if (!isError) UI_WorkerThread_DataFilter(data, obj);
      OutputData.Add(data);
      richTextBox1.AppendText($"{data}\n");
    }
    
    void UI_WorkerProcess_Pre(YoutubeDownloader obj)
    {
      richTextBox1.BackColor = colorDark;
      richTextBox1.ForeColor = colorLight;
      foreach (var c in TogglableControls) c.Enabled = false;
      richTextBox1.Clear();
      OutputData.Clear();
      var content = $"<APP>: {obj.CommandText}";
      OutputData.Add(content);
      richTextBox1.AppendText($"{content}\n");
      richTextBox1.Focus();
    }
    
    void UI_WorkerProcess_Post(YoutubeDownloader obj)
    {
      if (obj.Aborted && obj.KnownTargetFile!=null)
      {
        var fi = new FileInfo(Path.Combine(obj.TargetPath, obj.KnownTargetFile));
        fi.DerivedFile(".jpg").Clean();
        fi.DerivedFile($".temp.{fi.Extension}").Clean();
      }
      richTextBox1.BackColor = colorLight;
      richTextBox1.ForeColor = colorDark;
      var abort = !string.IsNullOrEmpty(obj.AbortMessage) ? $"\n{obj.AbortMessage}" : string.Empty;
      var content = $"<APP:ERRORSTATUS>: {obj.ExitCode}{abort}";
      OutputData.Add($"{content}\n");
      richTextBox1.AppendText($"{content}\n");
      foreach (var c in TogglableControls) c.Enabled = true;
    }

  }
  /// <summary>
  /// Description of MainForm_Worker.
  /// </summary>
  partial class MainForm
  {
    System.ComponentModel.BackgroundWorker worker;
    YoutubeDownloader downloader;
    System.Threading.Thread thread;
    List<string> OutputData = new List<string>();

    void WorkerThread_Completed(object sender, EventArgs e) { worker.CancelAsync(); }
    
    void WorkerThread_DataReceived(object sender, System.Diagnostics.DataReceivedEventArgs e)
    {
      if (InvokeRequired) Invoke(new Action(()=>UI_WorkerThread_DataHandler(e.Data, false, downloader)));
      else UI_WorkerThread_DataHandler(e.Data, false, downloader);
    }
    
    void WorkerThread_ErrorReceived(object sender, System.Diagnostics.DataReceivedEventArgs e)
    {
      if (InvokeRequired) Invoke(new Action(()=>UI_WorkerThread_DataHandler(e.Data, true, downloader)));
      else UI_WorkerThread_DataHandler(e.Data, true, downloader);
    }
    
    void Worker_Begin()
    {
      if (worker!=null && worker.IsBusy) return;
      worker = new System.ComponentModel.BackgroundWorker();
      worker.DoWork += WorkerEvent_DoWork;
      worker.Disposed += WorkerEvent_Disposed;
      worker.RunWorkerCompleted += WorkerEvent_Complete;;
      worker.WorkerSupportsCancellation = true;
      worker.WorkerReportsProgress = false;
      worker.RunWorkerAsync();
    }

    void WorkerEvent_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
    {
      thread = new System.Threading.Thread(UI_WorkerProcess_PrepareThread);
      thread.Start();
      while (thread.IsAlive) System.Threading.Thread.Sleep(500);
    }

    void WorkerEvent_Complete(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
    {
      if (InvokeRequired) Invoke(new Action(()=>UI_WorkerProcess_Post(downloader)));
      else UI_WorkerProcess_Post(downloader);
      worker.Dispose();
    }
    
    void WorkerEvent_Disposed(object sender, EventArgs e) { worker = null; }
  }
}
