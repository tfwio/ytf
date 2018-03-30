using System;
namespace YouTubeDownloadUtil
{
  interface IUI { System.Windows.Forms.RichTextBox OutputRTF { get; } }
  static class Actions
  {
    static readonly object L = new object();
    
    internal static Action ExploreToPath { get; } =()=> {
      lock (L) {
        if (ConfigModel.Instance.TargetOutputDirectory.Contains("start:")) System.Diagnostics.Process.Start("start",KeyStrings.ExploreToPath.Replace( "$path$", ConfigModel.Instance.TargetOutputDirectory.Replace("start:","") ));
        else System.Diagnostics.Process.Start("explorer.exe",KeyStrings.ExploreToPath.Replace( "$path$", ConfigModel.Instance.TargetOutputDirectory.EnvironmentPathFilter() ));
      };
    };
    // internal static Action ExploreTo { get; } =()=> { lock (L) { System.Diagnostics.Process.Start("explorer.exe",KeyStrings.ExploreToPath.Replace( "$path$", ConfigModel.Instance.TargetOutputDirectory.EnvironmentPathFilter() )); }; };
    internal static Func<string> RtfHelpText { get; } =()=> { using (var rs= System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("YouTubeDownloadUtil.readme.rtf")) using (var sr=new System.IO.StreamReader(rs)) return sr.ReadToEnd(); };
    
    internal static void COutputClear(IUI f)=> f.OutputRTF.Clear();
    internal static void COutputSplash(IUI f)=> f.OutputRTF.Rtf = Actions.RtfHelpText();
    internal static void COutputWordWrap(IUI f) => f.OutputRTF.WordWrap = !f.OutputRTF.WordWrap;
    internal static void COutputWorkPath(IUI f)=> f.OutputRTF.AppendText(ConfigModel.Instance.TargetOutputDirectory.EnvironmentPathFilter()+"\n");
  }
}




