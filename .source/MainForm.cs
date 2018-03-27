﻿using System;
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
    
    void UI_WorkerThread_DataFilter(string text)
    {
      if (!string.IsNullOrEmpty(text) && text.Contains("[download] Destination: "))
        Text=text.Replace("[download] Destination: ", "").Trim();
    }
    
    void UI_WorkerThread_DataHandler(string data, bool isError)
    {
      if (!isError) UI_WorkerThread_DataFilter(data);
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
      if (InvokeRequired) Invoke(new Action(()=>UI_WorkerThread_DataHandler(e.Data,false)));
      else UI_WorkerThread_DataHandler(e.Data,false);
    }
    
    void WorkerThread_ErrorReceived(object sender, System.Diagnostics.DataReceivedEventArgs e)
    {
      if (InvokeRequired) Invoke(new Action(()=>UI_WorkerThread_DataHandler(e.Data,true)));
      else UI_WorkerThread_DataHandler(e.Data,true);
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
        AddMetaData=mAddMetadata.Checked,
        Continue=mContinue.Checked,
        EmbedSubs= mEmbedSubs.Checked,
        EmbedThumbnail=mEmbedThumb.Checked,
        GetPlaylist=mGetPlaylist.Checked,
        IgnoreErrors=mIgnoreErrors.Checked,
        WriteAutoSub=mWriteAutoSubs.Checked,
        WriteSub=mWriteSubs.Checked,
      };
      
      if (InvokeRequired) {
        Invoke(new Action(()=>UI_WorkerProcess_Pre(downloader)));
        Invoke(new Action(richTextBox1.Clear));
        Invoke(new Action(()=>richTextBox1.AppendText($"[to youtube-dl]: {downloader.CommandText}\n")));
      } else {
        UI_WorkerProcess_Pre(downloader);
        richTextBox1.Clear();
        richTextBox1.AppendText($"[to youtube-dl]: {downloader.CommandText}\n");
      }
      
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
    
    ToolStripMenuItem mOptions, mAddMetadata, mContinue, mEmbedSubs, mEmbedThumb, mGetPlaylist, mSep, mIgnoreErrors, mVerbose, mWriteAutoSubs, mWriteSubs;

    void CreateToolStrip()
    {
      cm = new ContextMenuStrip();
      mOptions = cm.Items.Add("Options: Flags") as ToolStripMenuItem;
      cm.Items.Add("[browse] Download Path");
      cm.Items.Add("[browse] FFmpeg");
      cm.Items.Add("[browse] youtube-dl");
      
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
      
      mAddMetadata.CheckOnClick = true;
      mContinue.CheckOnClick = true;
      mEmbedSubs.CheckOnClick = true;
      mEmbedThumb.CheckOnClick = true;
      mGetPlaylist.CheckOnClick = true;
      mIgnoreErrors.CheckOnClick = true;
      mVerbose.CheckOnClick = true;
      mWriteAutoSubs.CheckOnClick = true;
      mWriteSubs.CheckOnClick = true;
      
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
