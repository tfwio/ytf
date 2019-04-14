using System;
namespace YouTubeDownloadUtil
{
	interface IUI {

		void Worker_Begin();
		void Print(string heading, params string[] lines);
		void PrintMore(string heading, params string[] lines);
		void SetStatus(string text);

    System.Drawing.Text.PrivateFontCollection PrivateFonts { get; }
		System.Windows.Forms.RichTextBox OutputRTF { get; }
		System.Windows.Forms.TextBox TextInput { get; }

	}
	static class Actions
	{
		static readonly object L = new object();

		internal static Action ExploreToPath { get; } = () => {
			lock (L) {
				if (ConfigModel.Instance.TargetOutputDirectory.Contains("start:")) System.Diagnostics.Process.Start("start", ResourceStrings.ExploreToPath.Replace("$path$", ConfigModel.Instance.TargetOutputDirectory.Replace("start:", "")));
				else System.Diagnostics.Process.Start("explorer.exe", ResourceStrings.ExploreToPath.Replace("$path$", ConfigModel.Instance.TargetOutputDirectory));
			};
		};
		internal static void COutputEmbededFonts(IUI f)
		{
			using (var defaultFont = new System.Drawing.Font(f.OutputRTF.Font.FontFamily, f.OutputRTF.Font.Size, f.OutputRTF.Font.Style))
				using (var headerFont = new System.Drawing.Font(f.OutputRTF.Font.FontFamily, 14f, System.Drawing.FontStyle.Bold))
			{
				f.OutputRTF.SelectionTabs = null;
				f.OutputRTF.Clear();
				f.OutputRTF.SelectionFont = headerFont;
				f.OutputRTF.AppendText("Embedded Font Families\n");
				f.OutputRTF.SelectionFont = defaultFont;
				foreach (var fam in f.PrivateFonts.Families)
				{
					f.OutputRTF.AppendText($"{fam.Name}\n");
				}
				f.OutputRTF.AppendText("\n");
			}
			
		}
		// internal static Action ExploreTo { get; } =()=> { lock (L) { System.Diagnostics.Process.Start("explorer.exe",KeyStrings.ExploreToPath.Replace( "$path$", ConfigModel.Instance.TargetOutputDirectory.EnvironmentPathFilter() )); }; };
		internal static Func<string> RtfHelpText { get; } = () => { using (var rs = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("YouTubeDownloadUtil.readme.rtf")) using (var sr = new System.IO.StreamReader(rs)) return sr.ReadToEnd(); };
		//internal static Func<string> RtfHelpText { get; } =()=> { using (var rs= System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("YouTubeDownloadUtil.splash.rtf")) using (var sr=new System.IO.StreamReader(rs)) return sr.ReadToEnd(); };
		internal static void CRunLastType(IUI f)     { f.Worker_Begin(); }
		internal static void COutputClear(IUI f)     { f.OutputRTF.Clear(); f.OutputRTF.SelectionTabs = null; }
		internal static void COutputSplash(IUI f)    { f.OutputRTF.Rtf = Actions.RtfHelpText(); f.OutputRTF.WordWrap = true; }
		internal static void COutputWordWrap(IUI f)  { f.OutputRTF.WordWrap = !f.OutputRTF.WordWrap; }
		internal static void COutputZoomReset(IUI f) { f.OutputRTF.ZoomFactor = 1; }
		internal static void COutputShortcuts(IUI f)
		{
			f.OutputRTF.SelectAll();
			f.OutputRTF.SelectionTabs = null;
			f.OutputRTF.Clear();
			using (var reguFont = new System.Drawing.Font(f.OutputRTF.Font.FontFamily, 14, System.Drawing.FontStyle.Regular))
				using (var boldFont = new System.Drawing.Font(f.OutputRTF.Font.FontFamily, 14, System.Drawing.FontStyle.Bold))
			{
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
		internal static void COutputCurrentTargetPath(IUI f)
		{
			var pathvalue = System.IO.Path.GetFullPath(ConfigModel.Instance.TargetOutputDirectory.Decode());
			
			f.Print(
				"Selected Target Output Directory…",
				$"Directory Name: “{System.IO.Path.GetFileName(pathvalue)}”\n",
				$"Full Path: “{pathvalue}”\n",
				$"Coded Path: “{pathvalue.Encode()}”"
			);
			
			var targets = new System.Collections.Generic.List<string>();
			
			foreach (var item in ConfigModel.Instance.DownloadTargetsList) targets.Add($"{item.Encode()}\n");
			f.OutputRTF.AppendText("\n");
			f.PrintMore("All Download Targets", targets.ToArray());
		}
		// internal static void COutputAppChecklist(IUI f)
		// {
		// 	
		// }
	}
}




