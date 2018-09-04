using System;
using System.Windows.Forms;

namespace YouTubeDownloadUtil
{
  internal sealed class Program
  {
    //[System.Runtime.InteropServices.DllImport("user32")]
    //private static extern bool ShowWindow(IntPtr hWnd , int nCmdShow);
    [System.Runtime.InteropServices.DllImport("user32", SetLastError = true)]
    private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
    [System.Runtime.InteropServices.DllImport("user32")]
    private static extern bool ShowWindow(IntPtr hwnd, int nCmdShow);
    [System.Runtime.InteropServices.DllImport("user32")]
    private static extern bool EnableWindow(IntPtr hwnd, bool enable);
    [System.Runtime.InteropServices.DllImport("user32")]
    private static extern bool MoveWindow(IntPtr handle, int x, int y, int width, int height, bool redraw);
    //https://stackoverflow.com/questions/10387586/how-to-hide-unhide-a-process-in-c
    //https://stackoverflow.com/questions/21434644/how-to-show-console-app-window-hidden-by-createprocess-function#21485632
    //Well, this didn't work.
    const int SW_SHOW = 5;
    [Flags]
    enum STARTF : uint
    {
      STARTF_USESHOWWINDOW = 0x00000001,
      STARTF_USESIZE       = 0x00000002,
      STARTF_USEPOSITION   = 0x00000004,
      STARTF_USECOUNTCHARS = 0x00000008,
      STARTF_USEFILLATTRIBUTE = 0x00000010,
      STARTF_RUNFULLSCREEN = 0x00000020,  // ignored for non-x86 platforms
      STARTF_FORCEONFEEDBACK = 0x00000040,
      STARTF_FORCEOFFFEEDBACK = 0x00000080,
      STARTF_USESTDHANDLES = 0x00000100,
    }

    static IntPtr FindMe(string className=null, string programName="ffmpeg.exe", int msWait=1000)
    {
      IntPtr HWND = FindWindow(className, programName);
      System.Threading.Thread.Sleep(msWait);
      ShowWindow(HWND, SW_SHOW);
      EnableWindow(HWND, true);
      return HWND;
    }
    /// <summary>
    /// This program might terminate unexpectedly if you attempt to download a frigging
    /// stream and leave behind a windowless command 'ffmpeg.exe'.
    /// 
    /// In that scenario, we can not do anything to show the window.
    /// 
    /// Though its not been implemented, we may add some flags to the process before its
    /// creation which can perhaps allow us to assign a window handle to it???
    /// 
    /// Since we are not using CreateProcess (win32 api) we don't get to do any of what
    /// is mentioned above, so it could otherwise be possible if we find a solution for
    /// this perhaps on SO, but I would much rather see if there is a way to do this
    /// by using the existing youtube-dl process that we have going here.
    /// 
    /// Unfortunately I have not as of yet figured a way to stop downloading a RTP stream
    /// for windows.
    /// </summary>
    /// <param name="PID"></param>
    static void FindAppHelper(int PID)
    {
      IntPtr Handle = FindMe();
      MessageBox.Show($"Handle: {Handle}... Attempted to show...", "Success?");
    }

    [STAThread]
    private static void Main(string[] args)
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new MainForm());
    }
  }
}
