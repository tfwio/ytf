using System;
using System.Collections.Generic;
using System.IO;

namespace YouTubeDownloadUtil
{
  // mvc component (not implemented yet)
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
  // a mvc view
  partial class MainForm
  {

    void UI_WorkerThread_DataFilter(string text, YoutubeDownloader obj)
    {
      if (!string.IsNullOrEmpty(text) && text.Contains(ResourceStrings.msgDownloadDestination))
      {
        obj.KnownTargetFile = text.Replace(ResourceStrings.msgDownloadDestination, "").Trim();
        Text = obj.KnownTargetFile;
      }
      else if (!string.IsNullOrEmpty(text)
        && text.Contains("[download]")
        && text.Contains("% of"))
      {
        var dat = new DownloaderStatus(text);
        statCurrent.Value = dat.IntPercent;
        statText.Text = dat.Percent;
      }
      else if (!string.IsNullOrEmpty(text)
               && text.Contains(ResourceStrings.msgAllreadyDownloaded)
               && obj.Flags.HasFlag(YoutubeDlFlags.AbortOnDuplicate))
      {
        obj.KnownTargetFile = text
          .Replace(ResourceStrings.msgDownloadHeading, "")
          .Replace(ResourceStrings.msgAllreadyDownloaded, "");
        obj.Abort($"[abort] due to EXISITING FILE: {obj.KnownTargetFile}\n");
        Text = $"[EXISTS] {obj.KnownTargetFile}";
      }
    }
    
    void UI_WorkerThread_DataHandler(string data, bool isError, YoutubeDownloader obj)
    {
      var iserrorstr = isError ? "2>" : "1>";
      if (!isError) UI_WorkerThread_DataFilter(data, obj);
      OutputData.Add(data);
      richTextBox1.AppendText($"{iserrorstr} {data}\n");
    }
    
    void UI_WorkerProcess_Pre(YoutubeDownloader obj)
    {
      richTextBox1.BackColor = colorDark;
      richTextBox1.ForeColor = colorLight;
      foreach (var c in TogglableControls) c.Enabled = false;
      richTextBox1.Clear();
      richTextBox1.WordWrap = false;
      OutputData.Clear();
      var content = $"<APP>: {obj.CommandText}";
      OutputData.Add(content);
      richTextBox1.AppendText($"{content}\n");
      richTextBox1.Focus();
      btnAbort.Visible = true;
      statCurrent.Value = 0;
      statusControls.Visible = true;
      statCurrent.ShowInTaskbar = true;
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
      btnAbort.Visible = false;
      statusControls.Visible = false;
      statText.Text = "youtube-dl";
      statCurrent.Value = 0;
      statCurrent.ShowInTaskbar = false;
    }

  }
  
  // a mvc component - containing the worker->thread->downloader.
  partial class MainForm
  {
    System.ComponentModel.BackgroundWorker worker;
    YoutubeDownloader downloader;
    System.Threading.Thread thread;
    List<string> OutputData = new List<string>();
    
    void UI_WorkerProcess_PrepareThread()
    {
      var downloads = ConfigModel.Instance.TargetOutputDirectory.EnvironmentPathFilter();
      downloader = new YoutubeDownloader(
        ConfigModel.TargetURI,
        downloads,
        WorkerThread_DataReceived,
        WorkerThread_ErrorReceived,
        WorkerThread_Completed
       ){
        Flags = ConfigModel.Instance.AppFlags,
        TargetType = ConfigModel.Instance.TargetType,
      };
      
      if (InvokeRequired) Invoke(new Action(()=>UI_WorkerProcess_Pre(downloader)));
      else UI_WorkerProcess_Pre(downloader);
      
      downloader.Go();
    }
    
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
