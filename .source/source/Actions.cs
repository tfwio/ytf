using System;
namespace YouTubeDownloadUtil
{
  interface IUI {
    void Worker_Begin();
    System.Windows.Forms.RichTextBox OutputRTF { get; }
    System.Windows.Forms.TextBox TextInput { get; }
  }
  static class Actions
  {
    static readonly object L = new object();
    
    internal static Action ExploreToPath { get; } =()=> {
      lock (L) {
        if (ConfigModel.Instance.TargetOutputDirectory.Contains("start:")) System.Diagnostics.Process.Start("start", ResourceStrings.ExploreToPath.Replace( "$path$", ConfigModel.Instance.TargetOutputDirectory.Replace("start:","") ));
        else System.Diagnostics.Process.Start("explorer.exe",ResourceStrings.ExploreToPath.Replace( "$path$", ConfigModel.Instance.TargetOutputDirectory.EnvironmentPathFilter() ));
      };
    };
    // internal static Action ExploreTo { get; } =()=> { lock (L) { System.Diagnostics.Process.Start("explorer.exe",KeyStrings.ExploreToPath.Replace( "$path$", ConfigModel.Instance.TargetOutputDirectory.EnvironmentPathFilter() )); }; };
    internal static Func<string> RtfHelpText { get; } =()=> { using (var rs= System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("YouTubeDownloadUtil.readme.rtf")) using (var sr=new System.IO.StreamReader(rs)) return sr.ReadToEnd(); };
    //internal static Func<string> RtfHelpText { get; } =()=> { using (var rs= System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("YouTubeDownloadUtil.splash.rtf")) using (var sr=new System.IO.StreamReader(rs)) return sr.ReadToEnd(); };
    
    internal static void CRunLastType(IUI f)     { f.Worker_Begin(); }
    internal static void COutputClear(IUI f)     { f.OutputRTF.Clear(); f.OutputRTF.SelectionTabs = null; }
    internal static void COutputSplash(IUI f)    { f.OutputRTF.Rtf = Actions.RtfHelpText(); f.OutputRTF.WordWrap = true; }
    internal static void COutputWordWrap(IUI f)  { f.OutputRTF.WordWrap = !f.OutputRTF.WordWrap; }
    internal static void COutputWorkPath(IUI f)  { f.OutputRTF.AppendText(ConfigModel.Instance.TargetOutputDirectory.EnvironmentPathFilter()+"\n"); }
    internal static void COutputZoomReset(IUI f) { f.OutputRTF.ZoomFactor = 1; }
    internal static void COutputShortcuts(IUI f)
    {
      f.OutputRTF.SelectionTabs = null;
      f.OutputRTF.Clear();
      var reguFont = new System.Drawing.Font(f.OutputRTF.Font.FontFamily, 14, System.Drawing.FontStyle.Regular);
      var boldFont = new System.Drawing.Font(f.OutputRTF.Font.FontFamily, 14, System.Drawing.FontStyle.Bold);
      f.OutputRTF.SelectionIndent = 20;
      f.OutputRTF.AppendText($"\n");
      foreach (var k in MainForm.CommandHandlers)
      {
        f.OutputRTF.SelectionFont = boldFont;
        f.OutputRTF.AppendText($"{k.Name}\n");
        f.OutputRTF.SelectionFont = reguFont;
        f.OutputRTF.SelectionTabs = new int[]{80};
        var keys = k.Keys.ToString();
        f.OutputRTF.AppendText($"\t[ {keys} ]\n");
      }
    }
  }
}




