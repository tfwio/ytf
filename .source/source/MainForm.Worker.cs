﻿using System;
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
    void StateName_Show()
    {
      statusControls.RowStyles[1].SizeType = System.Windows.Forms.SizeType.Absolute;
      statusControls.RowStyles[1].Height = 32;
      panelFileName.Visible = true;
      statusControls.Refresh();
    }
    void StateName_Hide()
    {
      statusControls.RowStyles[1].SizeType  = System.Windows.Forms.SizeType.AutoSize;
      panelFileName.Visible = false;
      statusControls.Refresh();
    }
    // hide status controls
    void StateProgress_Hide()
    {
      statCurrent.Visible = false;
      statItemCount.Visible = false;
    }
    // single download
    void StateProgress_OneColumn()
    {
      statCurrent.Visible = true;
      statItemCount.Visible = false;
      statusControls.ColumnStyles[1].SizeType = System.Windows.Forms.SizeType.Percent;
      statusControls.ColumnStyles[0].SizeType = System.Windows.Forms.SizeType.Percent;
      statusControls.ColumnStyles[0].Width = 100F;
      statusControls.ColumnStyles[1].Width = 0F;
      statCurrent.Refresh();
      statusControls.Refresh();
    }
    // multiple downloads
    void StateProgress_TwoColumn()
    {
      statCurrent.Visible = true;
      statItemCount.Visible = true;
      statusControls.ColumnStyles[1].SizeType = System.Windows.Forms.SizeType.Percent;
      statusControls.ColumnStyles[0].SizeType = System.Windows.Forms.SizeType.Percent;
      statusControls.ColumnStyles[0].Width = 80F;
      statusControls.ColumnStyles[1].Width = 100F;
      statCurrent.Refresh();
      statusControls.Refresh();
    }
    void StateDownload_Begin()
    {
      StateProgress_OneColumn();
      btnAbort.Visible = true;
      statCurrent.Value = 0;
      statusControls.Visible = true;
      statCurrent.ShowInTaskbar = true;
    }
    void StateDownload_End()
    {
      foreach (var c in TogglableControls) c.Enabled = true;
      btnAbort.Visible = false;
      statusControls.Visible = false;
      statText.Text = "youtube-dl";
      StateProgress_OneColumn(); // reset to a single column state.
      statCurrent.Value = 0;
      statCurrent.ShowInTaskbar = false;
      statItemCount.Value = 1;
    }

    // See UI_WorkerThread_DataHandler
    // This is the string data filter.
    // It creates values to be placed into the main ProgressBar.
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
      else if  (!string.IsNullOrEmpty(text)
                && text.Contains("[download] Downloading video")
                && text.Contains(" of ")
        )
      {
        // this is a placeholder for downloading a playlist.
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
    
    // Prepares a download job; called before downloading begins.
    void UI_WorkerProcess_Pre(YoutubeDownloader obj)
    {
      // we would like to prepare and/or reset a playlist counter.
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
      StateDownload_Begin();
    }

    // After downloading or Process is complete, we then restore
    // the application to a "ready" state.
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
      StateDownload_End();
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
