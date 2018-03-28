using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace YouTubeDownloadUtil
{
  public partial class MainForm : Form
  {
    BackgroundWorker worker;
    YoutubeDownloader downloader;
    Thread thread;
    
    string DragDropButtonText = string.Empty;
    string NextTargetType { get; set; } = "m4a";
    
    ContextMenuStrip cm;
    
    const string msgAllreadyDownloaded = "has already been downloaded";
    const string msgDownloadHeading = "[download] ";
    const string msgDownloadDestination = "[download] Destination: ";
    
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
      richTextBox1.BackColor = Color.FromArgb(64,64,64);
      richTextBox1.ForeColor = SystemColors.ControlLight;
      foreach (var c in TogglableControls) c.Enabled = false;
      richTextBox1.Clear();
      richTextBox1.AppendText($"<APP>: {obj.CommandText}\n");
      richTextBox1.Focus();
    }
    
    Control[] TogglableControls { get { return new Control[]{lbM4a, lbMp3, lbMp4, lbBest, lbLast}; } }
    
    void UI_WorkerProcess_Post(YoutubeDownloader obj)
    {
      if (obj.Aborted)
      {
        var fi = new FileInfo(Path.Combine(obj.TargetPath, obj.KnownTargetFile));
        var jpg = fi.FullName.Replace(fi.Extension,".jpg");
        var temp = fi.FullName.Replace(fi.Extension,$".temp.{fi.Extension}");
        if (File.Exists(jpg)) File.Delete(jpg);
        if (File.Exists(temp)) File.Delete(temp);
      }
      richTextBox1.BackColor = SystemColors.ControlLight;
      richTextBox1.ForeColor = Color.FromArgb(64,64,64);
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
    
    ToolStripMenuItem mOptions, mAbortOnDuplicate, mAddMetadata, mContinue, mEmbedSubs, mEmbedThumb, mGetPlaylist, mSep, mIgnoreErrors, mVerbose, mWriteAutoSubs, mWriteSubs, mDownloadTargets;
    
    void CreateToolStrip()
    {
      cm = new ContextMenuStrip();
      mOptions = cm.Items.Add("Options: Flags") as ToolStripMenuItem;
      cm.Items.Add("[add] Download Directory");
      lbLast.Text = $"[{NextTargetType}]"; // initial target-type is m4a (itunes audio)
      mAbortOnDuplicate = mOptions.DropDownItems.Add("Abort on Duplicate (File Exists)") as ToolStripMenuItem;
      mAddMetadata      = mOptions.DropDownItems.Add("Add MetaData") as ToolStripMenuItem;
      mContinue         = mOptions.DropDownItems.Add("Continue Unfinished Downloads") as ToolStripMenuItem;
      mEmbedSubs        = mOptions.DropDownItems.Add("Embed Subtitles") as ToolStripMenuItem;
      mEmbedThumb       = mOptions.DropDownItems.Add("Embed Thumbnail") as ToolStripMenuItem;
      mGetPlaylist      = mOptions.DropDownItems.Add("Get Playlist") as ToolStripMenuItem;
      mSep              = mOptions.DropDownItems.Add("-") as ToolStripMenuItem;
      mIgnoreErrors     = mOptions.DropDownItems.Add("Ignore Errors") as ToolStripMenuItem;
      mVerbose          = mOptions.DropDownItems.Add("Verbose") as ToolStripMenuItem;
      mWriteAutoSubs    = mOptions.DropDownItems.Add("Write Auto Subtitles (yt: if present)") as ToolStripMenuItem;
      mWriteSubs        = mOptions.DropDownItems.Add("Write Subtitles (yt: if present") as ToolStripMenuItem;
      mDownloadTargets  = cm.Items.Add("Download Targets") as ToolStripMenuItem;
      foreach (var m in new ToolStripMenuItem[]{ mAbortOnDuplicate,mAddMetadata,mContinue,mEmbedSubs,mEmbedThumb,mGetPlaylist,mIgnoreErrors,mVerbose,mWriteAutoSubs,mWriteSubs}) m.CheckOnClick = true;
      // load defaults
      mAbortOnDuplicate.Checked  = DownloadTarget.Default.AbortOnDuplicate;
      mAddMetadata.Checked       = DownloadTarget.Default.AddMetaData;
      mContinue.Checked          = DownloadTarget.Default.Continue;
      mEmbedSubs.Checked         = DownloadTarget.Default.EmbedSubs;
      mEmbedThumb.Checked        = DownloadTarget.Default.EmbedThumbnail;
      mGetPlaylist.Checked       = DownloadTarget.Default.GetPlaylist;
      mIgnoreErrors.Checked      = DownloadTarget.Default.IgnoreErrors;
      mVerbose.Checked           = DownloadTarget.Default.Verbose;
      mWriteAutoSubs.Checked     = DownloadTarget.Default.WriteAutoSub;
      mWriteSubs.Checked         = DownloadTarget.Default.WriteSub;
      UpdateDownloadTargets();
    }
    
    void DownloadTargetClickHandler(object sender, EventArgs e)
    {
      var value =  (sender as ToolStripMenuItem).Tag as string;
      var n = Path.GetFileName(DownloadTarget.Default.TargetPath = value);
      DownloadTarget.Default.TargetPath = (ConfigModel.Instance.TargetOutputDirectory = value);
      Text = $"Dir: {n}";
      UpdateDownloadTargets();
      ConfigModel.Instance.Save();
    }
    
    void UpdateDownloadTargets()
    {
      mDownloadTargets.DropDownItems.Clear();
      var dt = new List<string>(ConfigModel.Instance.DownloadTargetsList).ToArray();
      Array.Sort(dt);
      foreach (var i in dt)
      {
        var itm = mDownloadTargets.DropDownItems.Add(Path.GetFileName(i)) as ToolStripMenuItem;
        itm.Tag = i;
        itm.ToolTipText = i;
        itm.Checked = (i == ConfigModel.Instance.TargetOutputDirectory);
        itm.Click += DownloadTargetClickHandler;
      }
    }
    
    void ShowButtonMenu(Control target) { cm.Show(target, new Point(target.Width,target.Height), ToolStripDropDownDirection.BelowLeft); }
    
    public MainForm()
    {
      InitializeComponent();
      
      System.Environment.SetEnvironmentVariable("PATH",$"{ConfigModel.Instance.PathFFmpeg};{ConfigModel.Instance.PathYoutubeDL};{ConfigModel.OriginalPath}");
      
      FormClosing += (object sender, FormClosingEventArgs e) => ConfigModel.Instance.Save();
      
      CreateToolStrip();
      
      this.ApplyDragDropMethod(
        (sender,e)=>{
          if (e.Data.GetDataPresent(DataFormats.Text) ||
              e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        },
        (sender,e)=>{
          if (e.Data.GetDataPresent(DataFormats.Text))
          {
            DragDropButtonText = (string)e.Data.GetData(DataFormats.Text);
            textBox1.Text = (string)e.Data.GetData(DataFormats.Text);
          }
          else if (e.Data.GetDataPresent(DataFormats.FileDrop))
          {
            DragDropButtonText = (e.Data.GetData(DataFormats.FileDrop) as string[]).FirstOrDefault();
            if (Directory.Exists(DragDropButtonText))
            {
              cm.Show(button1,new Point(button1.Width,button1.Height), ToolStripDropDownDirection.BelowLeft);
              Text = "is directory";
              ConfigModel.Instance.AddDirectory(DragDropButtonText);
              UpdateDownloadTargets();
              DownloadTarget.Default.TargetPath = DragDropButtonText;
            }
            else if (File.Exists(DragDropButtonText))
            {
              cm.Show(button1,new Point(button1.Width,button1.Height), ToolStripDropDownDirection.BelowLeft);
              Text = "is file";
            }
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
