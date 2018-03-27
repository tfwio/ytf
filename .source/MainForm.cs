using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace YouTubeDownloadUI
{
  public partial class MainForm : Form
  {
    static readonly string textformat = DataFormats.Text;
    static readonly string fileformat = DataFormats.FileDrop;
    
    BackgroundWorker worker;
    YoutubeDownloader downloader;
    Thread thread;
    
    string DragDropButtonText = string.Empty;
    
    string NextTargetType { get; set; }
    
    ContextMenuStrip cm;
    
    const string msgAllreadyDownloaded = "has already been downloaded";
    const string msgDownloadHeading = "[download] ";
    const string msgDownloadDestination = "[download] Destination: ";
    void UI_WorkerThread_DataFilter(string text, YoutubeDownloader obj)
    {
      if (!string.IsNullOrEmpty(text) && text.Contains(msgDownloadDestination))
      {
        var filen = text.Replace(msgDownloadDestination, "").Trim();
        if (File.Exists(Path.Combine(obj.TargetPath,filen)) && obj.AbortOnDuplicate)
        {
          obj.Abort();
          richTextBox1.AppendText($"[abort] due to EXISITING FILE: {filen}\n");
          Text=$"[EXISTS] {filen}";
        }
        else Text = filen;
      }
      else if (!string.IsNullOrEmpty(text) && text.Contains(msgAllreadyDownloaded))
      {
        obj.Abort();
        var filen = text
          .Replace(msgDownloadHeading, "")
          .Replace(msgAllreadyDownloaded, "")
          .Trim();
        richTextBox1.AppendText($"[abort] due to EXISITING FILE: {filen}\n");
        Text=$"[EXISTS] {filen}";
      }
    }
    
    void UI_WorkerThread_DataHandler(string data, bool isError, YoutubeDownloader obj)
    {
      if (!isError) UI_WorkerThread_DataFilter(data, obj);
      richTextBox1.AppendText($"{data}\n");
    }
    
    void UI_WorkerProcess_Pre(YoutubeDownloader obj)
    {
      richTextBox1.BackColor = Color.FromArgb(64,64,64);
      richTextBox1.ForeColor = SystemColors.ControlLight;
      lbM4a.Enabled = false;
      lbMp4.Enabled = false;
      lbBest.Enabled = false;
      lbMp3.Enabled = false;
      lbLast.Enabled = false;
      richTextBox1.Clear();
      richTextBox1.AppendText($"[to youtube-dl]: {obj.CommandText}\n");
      richTextBox1.Focus();
    }
    
    void UI_WorkerProcess_Post(YoutubeDownloader obj)
    {
      richTextBox1.BackColor = SystemColors.ControlLight;
      richTextBox1.ForeColor = Color.FromArgb(64,64,64);
      richTextBox1.AppendText($"[exit-code]: {obj.ExitCode}\n");
      lbM4a.Enabled = true;
      lbMp3.Enabled = true;
      lbMp4.Enabled = true;
      lbBest.Enabled = true;
      lbLast.Enabled = true;
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
      if (worker!=null && worker.IsBusy) {
        return;
      }
      worker = new BackgroundWorker();
      worker.DoWork += WorkerEvent_DoWork;
      worker.Disposed += WorkerEvent_Disposed;
      worker.RunWorkerCompleted += WorkerEvent_Complete;;
      worker.WorkerSupportsCancellation = true;
      worker.WorkerReportsProgress = false;
      worker.RunWorkerAsync();
    }

    void Worker_PrepareThread()
    {
      var downloads = DirectoryHelper.EnsureLocalDirectory("downloads");
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
    
    void WorkerEvent_DoWork(object sender, DoWorkEventArgs e)
    {
      thread = new Thread(Worker_PrepareThread);
      thread.Start();
      while (thread.IsAlive)
        Thread.Sleep(500);
      
    }

    void WorkerEvent_Complete(object sender, RunWorkerCompletedEventArgs e)
    {
      if (InvokeRequired) Invoke(new Action(()=>UI_WorkerProcess_Post(downloader)));
      else UI_WorkerProcess_Post(downloader);
      worker.Dispose();
    }
    
    void WorkerEvent_Disposed(object sender, EventArgs e) { worker = null; }
    
    ToolStripMenuItem mOptions, mAbortOnDuplicate, mAddMetadata, mContinue, mEmbedSubs, mEmbedThumb, mGetPlaylist, mSep, mIgnoreErrors, mVerbose, mWriteAutoSubs, mWriteSubs;

    void CreateToolStrip()
    {
      cm = new ContextMenuStrip();
      mOptions = cm.Items.Add("Options: Flags") as ToolStripMenuItem;
      cm.Items.Add("[browse] Download Path");
      cm.Items.Add("[browse] FFmpeg");
      cm.Items.Add("[browse] youtube-dl");
      
      mAbortOnDuplicate = mOptions.DropDownItems.Add("Abort on Duplicate (File Exists)") as ToolStripMenuItem;
      mAddMetadata = mOptions.DropDownItems.Add("Add MetaData") as ToolStripMenuItem;
      mContinue = mOptions.DropDownItems.Add("Continue Unfinished Downloads") as ToolStripMenuItem;
      mEmbedSubs = mOptions.DropDownItems.Add("Embed Subtitles") as ToolStripMenuItem;
      mEmbedThumb = mOptions.DropDownItems.Add("Embed Thumbnail") as ToolStripMenuItem;
      mGetPlaylist = mOptions.DropDownItems.Add("Get Playlist") as ToolStripMenuItem;
      mSep = mOptions.DropDownItems.Add("-") as ToolStripMenuItem;
      mIgnoreErrors = mOptions.DropDownItems.Add("Ignore Errors") as ToolStripMenuItem;
      mVerbose = mOptions.DropDownItems.Add("Verbose") as ToolStripMenuItem;
      mWriteAutoSubs = mOptions.DropDownItems.Add("Write Auto Subtitles (yt: if present)") as ToolStripMenuItem;
      mWriteSubs = mOptions.DropDownItems.Add("Write Subtitles (yt: if present") as ToolStripMenuItem;
      
      mAbortOnDuplicate.CheckOnClick = true;
      mAbortOnDuplicate.ToolTipText =
        "If this is checked, and you have\n" +
        "Continue Downloads checked also,\n" +
        "provides you a conflict of interest.\n" +
        "note:\n" +
        "When CONTINUE-DOWNLOADS is enabled\n" +
        "and you attempt to re-download something\n" +
        "the file will always be over-written\n" +
        "particularly if you updated the file\n" +
        "such as by way of EMBEDDED-METADATA, \n" +
        "or COVER-IMAGE.";
      mAddMetadata.CheckOnClick = true;
      mContinue.CheckOnClick = true;
      mEmbedSubs.CheckOnClick = true;
      mEmbedThumb.CheckOnClick = true;
      mGetPlaylist.CheckOnClick = true;
      mIgnoreErrors.CheckOnClick = true;
      mVerbose.CheckOnClick = true;
      mWriteAutoSubs.CheckOnClick = true;
      mWriteSubs.CheckOnClick = true;
      
      mAbortOnDuplicate.Checked = DownloadTarget.Default.AbortOnDuplicate;
      mAddMetadata.Checked = DownloadTarget.Default.AddMetaData;
      mContinue.Checked = DownloadTarget.Default.Continue;
      mEmbedSubs.Checked = DownloadTarget.Default.EmbedSubs;
      mEmbedThumb.Checked = DownloadTarget.Default.EmbedThumbnail;
      mGetPlaylist.Checked = DownloadTarget.Default.GetPlaylist;
      mIgnoreErrors.Checked = DownloadTarget.Default.IgnoreErrors;
      mVerbose.Checked = DownloadTarget.Default.Verbose;
      mWriteAutoSubs.Checked = DownloadTarget.Default.WriteAutoSub;
      mWriteSubs.Checked = DownloadTarget.Default.WriteSub;
      
    }
    
    void ShowButtonMenu(Control target) { cm.Show(target, new Point(target.Width,target.Height), ToolStripDropDownDirection.BelowLeft); }
    
    public MainForm()
    {
      InitializeComponent();
      
      CreateToolStrip();
      
      button1.ApplyDragDropMethod(
        (sender,e)=>{
          if (e.Data.GetDataPresent(textformat) |
              e.Data.GetDataPresent(fileformat)) e.Effect = DragDropEffects.Copy;
        },
        (sender,e)=>{
          if (e.Data.GetDataPresent(textformat))
          {
            DragDropButtonText = (string)e.Data.GetData(textformat);
            if (DragDropButtonText.Contains("https://youtu.be")) ShowButtonMenu(button1);
            else if (DragDropButtonText.Contains("https://youtube.com")) ShowButtonMenu(button1);
            else if (DragDropButtonText.Contains("https://soundcloud.com")) ShowButtonMenu(button1);
            else if (DragDropButtonText.Contains("https://mixcloud.com")) ShowButtonMenu(button1);
          }
          else if (e.Data.GetDataPresent(fileformat))
          {
            DragDropButtonText = (string)e.Data.GetData(fileformat);
            if (Directory.Exists(DragDropButtonText))
              cm.Show(button1,new Point(button1.Width,button1.Height), ToolStripDropDownDirection.BelowLeft);
          }
        });
      
      textBox1.ApplyDragDropMethod(
        (sender,e)=>{ if (e.Data.GetDataPresent(textformat)) e.Effect = DragDropEffects.Copy; },
        (sender,e)=>{
          if (e.Data.GetDataPresent(textformat))
          {
            textBox1.Text = (string)e.Data.GetData(textformat);
          }
        });
    }
    
    void Event_BeginDownloadType(object sender, LinkLabelLinkClickedEventArgs e) { var l = sender as LinkLabel; NextTargetType = l.Text; lbLast.Text = $"[{NextTargetType}]"; Worker_Begin(); }
    void Event_BeginDownload(object sender, LinkLabelLinkClickedEventArgs e) { Worker_Begin(); }
    
    void TextBox1TextChanged(object sender, EventArgs e) { ckHasPlaylist.Checked = textBox1.Text.Contains("&list="); }
    
    void Button1MouseDown(object sender, MouseEventArgs e)
    {
      cm.Show(button1,new Point(button1.Width,button1.Height), ToolStripDropDownDirection.BelowLeft);
      cm.Focus();
    }
    
  }
  
}
