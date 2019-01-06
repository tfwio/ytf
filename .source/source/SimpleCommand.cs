using System;
using System.Diagnostics;
namespace YouTubeDownloadUtil
{
  class SimpleCommand<T> where T:System.Windows.Forms.Control
  {
    System.ComponentModel.BackgroundWorker Worker { get; set; }
    Process Proc;
    
    public string WorkPath { get; set; }
    public string Arguments { get; set; }
    public string FileName { get; set; }
    public bool SupportsProgress { get; set; }

    virtual protected ProcessStartInfo NewStartInfo {
      get
      {
        return new ProcessStartInfo
        {
          RedirectStandardInput = true,
          RedirectStandardOutput = true,
          UseShellExecute = false,
          FileName = FileName,
          WorkingDirectory = WorkPath.Decode(),
          Arguments = Arguments,
        };
      }
    }
    void InitEvents(
      DataReceivedEventHandler onOutput,
      DataReceivedEventHandler onError,
      EventHandler onCompleted)
    {
      Proc.OutputDataReceived += onOutput;
      Proc.ErrorDataReceived += onError;
      Completed += onCompleted;
    }

    public bool Initialize()
    {
      if (Worker != null) return false;
      Worker = new System.ComponentModel.BackgroundWorker() {
        WorkerSupportsCancellation = true,
        WorkerReportsProgress = SupportsProgress,
      };
      Worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
      Worker.DoWork += Worker_DoWork;
      Worker.Disposed += Worker_Disposed;
      if (Proc != null) return false;
      Proc = new Process()
      {
        StartInfo = NewStartInfo,
        EnableRaisingEvents = true,
      };
      Proc.Exited += Proc_Exited;
      return true;
    }

    private void Proc_Exited(object sender, EventArgs e)
    {
      Worker.CancelAsync();
    }

    private void Worker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
    {
      Proc.Start();
      Proc.WaitForExit();
      while (!Proc.HasExited) System.Threading.Thread.Sleep(500);

    }
    private void Worker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
    {

    }
    private void Worker_Disposed(object sender, EventArgs e)
    {
      throw new NotImplementedException();
    }
    public event EventHandler Completed;
    protected void OnComplete()
    {
      var completer = Completed;
      if (completer != null) completer(this, EventArgs.Empty);
    }
  }
}


