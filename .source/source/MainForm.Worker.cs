using System;
using System.Drawing;
using System.IO;

namespace YouTubeDownloadUtil
{
  /// <summary>
  /// Description of MainForm_Worker.
  /// </summary>
  partial class MainForm
  {
    readonly Color colorDark  = Color.FromArgb(64,64,64);
    readonly Color colorLight = SystemColors.ControlLight;
    
    System.ComponentModel.BackgroundWorker worker;
    YoutubeDownloader downloader;
    System.Threading.Thread thread;
    
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
      richTextBox1.AppendText($"{data}\n");
    }
    
    void UI_WorkerProcess_Pre(YoutubeDownloader obj)
    {
      richTextBox1.BackColor = colorDark;
      richTextBox1.ForeColor = colorLight;
      foreach (var c in TogglableControls) c.Enabled = false;
      richTextBox1.Clear();
      richTextBox1.AppendText($"<APP>: {obj.CommandText}\n");
      richTextBox1.Focus();
    }
    
    void UI_WorkerProcess_Post(YoutubeDownloader obj)
    {
      if (obj.Aborted)
      {
        var fi = new FileInfo(Path.Combine(obj.TargetPath, obj.KnownTargetFile));
        fi.DerivedFile(".jpg").Clean();
        fi.DerivedFile($".temp.{fi.Extension}").Clean();
      }
      richTextBox1.BackColor = colorLight;
      richTextBox1.ForeColor = colorDark;
      var abort = !string.IsNullOrEmpty(obj.AbortMessage) ? $"\n{obj.AbortMessage}" : string.Empty;
      richTextBox1.AppendText($"<APP:ERRORSTATUS>: {obj.ExitCode}{abort}\n");
      foreach (var c in TogglableControls) c.Enabled = true;
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

    void Worker_PrepareThread()
    {
      var downloads = "downloads".RelativeToExe();
      downloader = new YoutubeDownloader(
        textBox1.Text,
        downloads,
        WorkerThread_DataReceived,
        WorkerThread_ErrorReceived,
        WorkerThread_Completed
       ){
        TargetType = this.NextTargetType,
        Verbose=mVerbose.Checked,
        AbortOnDuplicate = mAbortOnDuplicate.Checked,
        AddMetaData=mAddMetadata.Checked,
        Continue=mContinue.Checked,
        EmbedSubs= mEmbedSubs.Checked,
        EmbedThumbnail=mEmbedThumb.Checked,
        GetPlaylist=mGetPlaylist.Checked,
        IgnoreErrors=mIgnoreErrors.Checked,
        WriteAutoSub=mWriteAutoSubs.Checked,
        WriteSub=mWriteSubs.Checked,
      };
      
      if (InvokeRequired) Invoke(new Action(()=>UI_WorkerProcess_Pre(downloader)));
      else UI_WorkerProcess_Pre(downloader);
      
      downloader.Go();
    }
    
    void WorkerEvent_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
    {
      thread = new System.Threading.Thread(Worker_PrepareThread);
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
