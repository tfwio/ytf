using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
namespace YouTubeDownloadUI
{
  
  
  class ThreadedDownloaderWorker
  {
    BackgroundWorker worker;
    YoutubeDownloader downloader;
    Thread thread;
    
    public DownloadTarget Target { get; set; }
    
    public event EventHandler DownloadPost;
    protected virtual void OnDownloadPost() { var handler = DownloadPost; if (handler != null) handler(this, EventArgs.Empty); }

    public event EventHandler DownloadPre;
    protected virtual void OnDownloadPre() { var handler = DownloadPre; if (handler != null) handler(this, EventArgs.Empty); }
    
//    void PrepareDownload()
//    {
//      if (InvokeRequired) Invoke(new Action(richTextBox1.Clear));
//      else richTextBox1.Clear();
//      
//      downloader = new YoutubeDownloader(DownloadTarget, downloads, DataReceived, ErrorReceived, downloader_Completed) {
//        TargetType = this.nextType,
//        IsVerbose = ckVerbose.Checked,
//        //        IsEmbedSubs=ckEmbedSubs.Checked,
//        //        IsGetPlaylist=ckUsePlaylist.Checked
//      };
//      richTextBox1.AppendText();
//      downloader.Go();
//    }
  }
}


