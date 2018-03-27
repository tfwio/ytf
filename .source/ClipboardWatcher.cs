using System;
using System.Windows.Forms;
namespace System
{
  /// <summary>
  /// Though the proper way to watch the clipboard is through a windows
  /// hook on the main window handle where we're looking for some
  /// notification CB_SUCHANDSUCH via WINDOWS SDK, but this works
  /// for now.
  /// </summary>
  public class ClipboardWatcher : IDisposable
  {
    #region Image Related
    
    const string mime_start = "data:image";
    const string MimeUndefined = "data:unknown";
    
    public string MimeText { get; set; }
    public string ImageTextStatus { get; set; }
    public bool HasMimeImage { get; set; }
    
    public static bool HasCbImage { get { return Clipboard.ContainsImage(); } }
    public static bool HasCbUniText { get { return Clipboard.ContainsText(TextDataFormat.UnicodeText); } }
    public static bool HasCbText { get { return Clipboard.ContainsText(TextDataFormat.Text); } }
    
    void DefaultImageWatchingAction(object sender, EventArgs e)
    {
      MimeText = MimeUndefined;
      var text = HasCbText ? Clipboard.GetText(TextDataFormat.UnicodeText) : null;
      var strt = HasCbText ? text.IndexOf(';') : -1;
      if (strt > -1)
      {
        MimeText = HasCbText ? text.Substring(0,strt) : MimeUndefined;
        HasMimeImage = HasCbText && MimeText.Contains(mime_start);
      }
    }
    
    #endregion

    readonly Timer fTimer = new Timer { Interval = 1000, };
    public Action<object, EventArgs> TimerInterval { get; set; }
    
    public ClipboardWatcher(Action<object,EventArgs> defaultIntervalAction)
    {
      TimerInterval = defaultIntervalAction ?? DefaultImageWatchingAction;
      fTimer.Tick += DefaultImageWatchingAction;
      fTimer.Start();
    }
    
    #region IDisposable implementation
    
    public void Dispose()
    {
      fTimer.Stop();
      fTimer.Dispose();
    }
    
    #endregion
    
  }
}


