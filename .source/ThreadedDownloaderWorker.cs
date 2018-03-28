using System;
using System.ComponentModel;
using System.Threading;
namespace YouTubeDownloadUtil
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
    
  }
}


