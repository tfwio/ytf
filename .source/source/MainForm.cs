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
    Control[] TogglableControls { get { return new Control[]{lbM4a, lbMp3, lbMp4, lbBest, lbLast}; } }
    BackgroundWorker worker;
    YoutubeDownloader downloader;
    Thread thread;
    
    internal List<CommandKeyHandler> CommandHandlers { get; private set; }
    
    string NextTargetType { get; set; } = "m4a";
    
    const string msgAllreadyDownloaded  = "has already been downloaded";
    const string msgDownloadHeading     = "[download] ";
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
      if (worker!=null && worker.IsBusy) return;
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
      while (thread.IsAlive) Thread.Sleep(500);
    }

    void WorkerEvent_Complete(object sender, RunWorkerCompletedEventArgs e)
    {
      if (InvokeRequired) Invoke(new Action(()=>UI_WorkerProcess_Post(downloader)));
      else UI_WorkerProcess_Post(downloader);
      worker.Dispose();
    }
    
    void WorkerEvent_Disposed(object sender, EventArgs e) { worker = null; }
    
    void DownloadTargetClickHandler(object sender, EventArgs e)
    {
      var value =  (sender as ToolStripMenuItem).Tag as string;
      var n = Path.GetFileName(DownloadTarget.Default.TargetPath = value);
      DownloadTarget.Default.TargetPath = (ConfigModel.Instance.TargetOutputDirectory = value);
      Text = $"Dir: {n}";
      UpdateDownloadTargets();
      ConfigModel.Instance.Save();
    }
    
    public MainForm()
    {
      CommandHandlers = new List<CommandKeyHandler>(){
        new CommandKeyHandler{Keys=Keys.E|Keys.Control, Action = Actions.ExploreTo}
      };
      
      InitializeComponent();
      
      richTextBox1.Rtf = Actions.RtfHelpText();
      richTextBox1.PreviewKeyDown += (a,e)=> { System.Windows.Forms.Message i = System.Windows.Forms.Message.Create(IntPtr.Zero,0,System.IntPtr.Zero,IntPtr.Zero); this.ProcessCmdKey(ref i,e.KeyData); };
      
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
    
    readonly object L= new object();
    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
      lock (L) foreach (var k in CommandHandlers) if (keyData.IsMatch(k.Keys)) k.Action();
      return base.ProcessCmdKey(ref msg, keyData);
    }
    
    void Event_BeginDownloadType(object sender, LinkLabelLinkClickedEventArgs e) { var l = sender as LinkLabel; NextTargetType = l.Text; lbLast.Text = $"[{NextTargetType}]"; Worker_Begin(); }
    void Event_BeginDownload(object sender, LinkLabelLinkClickedEventArgs e) { Worker_Begin(); }
    
    void TextBox1TextChanged(object sender, EventArgs e) { ckHasPlaylist.Checked = textBox1.Text.Contains("&list="); }
    
  }
  
}
