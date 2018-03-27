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
    
    void downloader_Completed(object sender, EventArgs e)
    {
      worker.CancelAsync();
    }
    
    void PrepareDownload()
    {
      var downloads = Path.Combine(Program.ExecutableDirectory,"downloads");
      if (!Directory.Exists(downloads))
        Directory.CreateDirectory(downloads);
      
      if (InvokeRequired)
      {
        Invoke(new Action(richTextBox1.Clear));
      }
      else{
        richTextBox1.Clear();
      }
      downloader = new YoutubeDownloader(
        textBox1.Text,
        downloads,
        DataReceived,
        ErrorReceived,
        downloader_Completed
       ){
        TargetType = this.nextType,
        IsVerbose=ckVerbose.Checked,
        //        IsEmbedSubs=ckEmbedSubs.Checked,
        //        IsGetPlaylist=ckUsePlaylist.Checked
      };
      richTextBox1.AppendText($"[to youtube-dl]: {downloader.CommandText}\n");
      downloader.Go();
    }
    
    void DataReceived(object sender, System.Diagnostics.DataReceivedEventArgs e)
    {
      var str = e.Data;
      if (!string.IsNullOrEmpty(str) && str.Contains("[download] Destination: "))
        Text=str.Replace("[download] Destination: ", "").Trim();
      if (richTextBox1.InvokeRequired)
        richTextBox1.Invoke(new Action(()=>richTextBox1.AppendText($"[data]: {e.Data}\n")));
      else
        richTextBox1.AppendText($"[data]: {e.Data}\n");
    }
    string nextType= "m4a";
    
    void ErrorReceived(object sender, System.Diagnostics.DataReceivedEventArgs e)
    {
      if (richTextBox1.InvokeRequired)
        richTextBox1.Invoke(new Action(()=>richTextBox1.AppendText($"[error]: {e.Data}\n")));
      else
        richTextBox1.AppendText($"[error]: {e.Data}\n");
    }
    
    /// <summary>
    /// hi there
    /// </summary>
    void BeginDownload()
    {
      ControlsHide();
      if (worker!=null && worker.IsBusy) {
        return;
      }
      worker = new BackgroundWorker();
      worker.DoWork += worker_DoWork;
      worker.Disposed += worker_Disposed;
      worker.RunWorkerCompleted += worker_RunWorkerCompleted;;
      worker.WorkerSupportsCancellation = true;
      worker.WorkerReportsProgress = false;
      worker.RunWorkerAsync();
    }

    void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      worker.Dispose();
    }
    
    void worker_Disposed(object sender, EventArgs e)
    {
      try {
        Console.Write("ERRORLEVEL={0}",downloader.ExitCode);
      } catch {}
      worker = null;
      if (InvokeRequired) Invoke(new Action(ControlsShow));
      else ControlsShow();
    }
    
    void ControlsShow()
    {
      richTextBox1.BackColor = Color.White;
      richTextBox1.ForeColor = RichTextBox.DefaultForeColor;
      linkLabel1.Enabled = true;
      linkLabel2.Enabled = true;
      linkLabel3.Enabled = true;
    }
    
    void ControlsHide()
    {
      richTextBox1.BackColor = Color.FromArgb(0,0,0);
      richTextBox1.ForeColor = Color.FromArgb(172,172,172);
      linkLabel1.Enabled = false;
      linkLabel2.Enabled = false;
      linkLabel3.Enabled = false;
    }
    
    void worker_DoWork(object sender, DoWorkEventArgs e)
    {
      thread = new Thread(PrepareDownload);
      thread.Start();
      if (InvokeRequired) Invoke(new Action(ControlsHide));
      else ControlsHide();
    }
    ContextMenuStrip cm;
    
    string DragDropButtonText = string.Empty;
    
    public MainForm()
    {
      InitializeComponent();
      cm = new ContextMenuStrip();
      cm.Items.Add("[browse] Download Path");
      cm.Items.Add("[browse] FFmpeg");
      cm.Items.Add("[browse] youtube-dl");
      
      button1.ApplyDragDropMethod(
        (sender,e)=>{
          if (e.Data.GetDataPresent(textformat) ||
              e.Data.GetDataPresent(fileformat)) e.Effect = DragDropEffects.Copy;
        },
        (sender,e)=>{
          if (e.Data.GetDataPresent(textformat))
          {
            DragDropButtonText = (string)e.Data.GetData(textformat);
            cm.Show(button1,new Point(button1.Width,button1.Height), ToolStripDropDownDirection.BelowLeft);
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
    void LinkLabel1LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      nextType = "m4a";
      BeginDownload();
    }
    void LinkLabel2LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      nextType = "mp4";
      BeginDownload();
    }
    void LinkLabel3LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      nextType = "best";
      BeginDownload();
    }
    void LinkLabel4LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      nextType = "mp3";
      BeginDownload();
    }
    void TextBox1TextChanged(object sender, EventArgs e)
    {
      ckHasPlaylist.Checked = textBox1.Text.Contains("&list=");
    }
    
    void Button1MouseDown(object sender, MouseEventArgs e)
    {
      cm.Show(button1,new Point(button1.Width,button1.Height), ToolStripDropDownDirection.BelowLeft);
      cm.Focus();
    }
    
  }
  
}
