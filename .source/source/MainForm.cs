using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace YouTubeDownloadUtil
{
  public partial class MainForm : Form, IUI, IRestoreBounds
  {
		static public string AppRootPath
		{
			get
			{
				var fileinfo = new FileInfo(Path.GetFullPath(System.Reflection.Assembly.GetExecutingAssembly().Location));
				return fileinfo.Directory.FullName;
			}
		}
    
  	readonly Font OpenSans_SB13, OpenSans_R10_5, Roboto_M12_Bold, Roboto_M9;
    readonly System.Drawing.Text.PrivateFontCollection pfc = new System.Drawing.Text.PrivateFontCollection();
    System.Drawing.Text.PrivateFontCollection IUI.PrivateFonts { get { return pfc; } }
    readonly Color colorDark = Color.FromArgb(64, 64, 64);
    readonly Color colorLight = SystemColors.ControlLight;

    ToolStripItem[] TogglableControls { get { return new ToolStripItem[] { lbLast }; } }

    void DownloadTargetClickHandler(object sender, EventArgs e)
    {
      var value = (sender as ToolStripMenuItem).Tag as string;
      var n = Path.GetFileName(DownloadTarget.Default.TargetPath = value);
      DownloadTarget.Default.TargetPath = (ConfigModel.Instance.TargetOutputDirectory = value);
      // SetStatus($"Dir: {n}");
      UpdateDownloadTargets();
      ConfigModel.Instance.Save();
			
			var encodedpath = Path.GetFullPath(value).Encode();
			WriteSomeStuff(
				"Selected Target Output Directory…",
				$"Directory Name: “{Path.GetFileName(value)}”\n",
				$"Full Path: “{value}”\n",
				$"Coded Path: “{encodedpath}”"
			);
    }

    void UpdateEnvironmentPath()
    {
      var pathVars = new List<string>();
      if (ConfigModel.Instance.PathAVConv.Decode().DirectoryExistsAndNonempty()) pathVars.Add(ConfigModel.Instance.PathAVConv.Decode());
      if (ConfigModel.Instance.PathFFmpeg.Decode().DirectoryExistsAndNonempty()) pathVars.Add(ConfigModel.Instance.PathFFmpeg.Decode());
      if (ConfigModel.Instance.PathYoutubeDL.Decode().DirectoryExistsAndNonempty()) pathVars.Add(ConfigModel.Instance.PathYoutubeDL.Decode());
      var newPath = string.Join(";", pathVars.ToArray());
      System.Environment.SetEnvironmentVariable("PATH", $"{newPath};{ConfigModel.OriginalPath}");
    }

    private void UI_WorkerProcess_Abort(object sender, EventArgs e)
    {
      downloader.Abort(ResourceStrings.msgUserAbort);
    }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      (this as IRestoreBounds).WindowStateToForm();
      mTopLevel.Checked = (TopMost = ConfigModel.Instance.KeepOnTop);
    }

    Timer statusTimer;

		#region Font Helpers
    /// <summary>Use this wisely.  EG: don't add the same font twice, of course.</summary>
    /// <param name="fontResource">Embedded byte[] resource</param>
    /// <returns>The Family</returns>
    FontFamily CreateResFont(byte[] fontResource)
    {
      var pinnedArray = System.Runtime.InteropServices.GCHandle.Alloc(fontResource, System.Runtime.InteropServices.GCHandleType.Pinned);
      IntPtr pointer = pinnedArray.AddrOfPinnedObject();
			
      pfc.AddMemoryFont(pointer, fontResource.Length);
      pinnedArray.Free();

      return pfc.Families.First();
    }
		/// <summary>
		/// Combines CreateResFont with a method which directly creates a
		/// font of a particular style and size.
		/// 
		/// Be careful not to add the same font.
		/// </summary>
		/// <param name="res">Binary byte[] resource.</param>
		/// <param name="emSize">font's float size.</param>
		/// <param name="fontStyle">you know.</param>
		/// <returns></returns>
    Font CreateFont(byte[] res, float emSize, FontStyle fontStyle)
    {
    	var fam_temp = CreateResFont(res);
    	return new Font(fam_temp, emSize, fontStyle);
    }
    Font GetEmbeddedFont(string name, float emSize, FontStyle fontStyle)
    {
    	// 1. Check if the family name exists
    	bool hasFamily = false;
      for (int i = 0; i < this.pfc.Families.Length; i++)
				hasFamily |= pfc.Families[i].Name == name;
    	// 2. return Null if we havent found the target font family.
      if (!hasFamily) return null;
      // 3. Get pointer to the fam.
      var fam = pfc.Families.First(fnt => fnt.Name == name);
      // 4. create/return desired Font.
      return new Font(fam, emSize, fontStyle);
    }
		#endregion
    
    public MainForm()
    {
      InitializeComponent();
      
      CreateResFont(ResImage.Roboto_Regular);
      CreateResFont(ResImage.Roboto_Medium);
      CreateResFont(ResImage.OpenSans_Semibold);
      CreateResFont(ResImage.OpenSans_Regular);
      
      Roboto_M9 = GetEmbeddedFont("Roboto Medium", 9F, FontStyle.Regular);
      Roboto_M12_Bold = GetEmbeddedFont("Roboto Medium", 12F, FontStyle.Bold);

      textBox1.Font = Roboto_M12_Bold;
      textMaxDownloads.Font = Roboto_M9;
      ckHasPlaylist.Font = Roboto_M9;
      toolStripLabel1.Font = Roboto_M9;
      toolStripLabel2.Font = Roboto_M9;
      toolStripTextBox1.Font = Roboto_M9;
      toolStripTextBox2.Font = Roboto_M9;
      
      OpenSans_SB13 = CreateFont(ResImage.OpenSans_Semibold, 13.0f, FontStyle.Regular);
      OpenSans_R10_5 = CreateFont(ResImage.OpenSans_Regular, 10.5f, FontStyle.Regular);
      cm.Font = OpenSans_SB13;
      textBox1.Font = OpenSans_R10_5;
			
      statusTimer = new Timer { Interval = 4500, Enabled = false };
      statusTimer.Tick += (n, a) => {
        statText.Text = "youtube-dl";
        statusTimer.Stop();
      };

      // additional properties
      statusControls.Visible = false;
      textMaxDownloads.Text = ConfigModel.Instance.MaxDownloads;

      // more initializers
      UpdateEnvironmentPath();
      Actions.COutputSplash(this);

      // events
      btnAbortProcess.MouseDown += Event_ButtonShowContext;
      textBox1.TextChanged += TextBox1TextChanged;

      ConfigModel.Instance.BeforeSaved += (s,e) => (this as IRestoreBounds).WindowStateToConfig();
      ConfigModel.Instance.Saved += (o, a) => SetStatus("Saved Configuration");
      ConfigModel.Instance.FlagsChanged += (o, a) => ConfigModel.Instance.Save();
      textMaxDownloads.TextChanged += (a, b) => ConfigModel.Instance.MaxDownloads = textMaxDownloads.Text;
      FormClosing += (s, e) => ConfigModel.Instance.Save(true);

      CreateToolStrip();
      StateProgress_OneColumn();

      // drag-drop
      this.ApplyDragDropMethod(
        (sender, e) =>
        {
          if (e.Data.GetDataPresent(DataFormats.Text) ||
              e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        },
        (sender, e) =>
        {
          if (e.Data.GetDataPresent(DataFormats.Text))
          {
            DragDropButtonText = (string)e.Data.GetData(DataFormats.Text);
            textBox1.Text = string.Empty;
            textBox1.Text = DragDropButtonText;
          }
          else if (e.Data.GetDataPresent(DataFormats.FileDrop))
          {
            DragDropButtonText = (e.Data.GetData(DataFormats.FileDrop) as string[]).FirstOrDefault();
            if (Directory.Exists(DragDropButtonText))
            {
							cm.Show(btnAbortProcess, new Point(btnAbortProcess.Width, btnAbortProcess.Height), ToolStripDropDownDirection.BelowLeft);
							// Text = "is directory";
							ConfigModel.Instance.AddDirectory(DragDropButtonText);
							UpdateDownloadTargets();
							DownloadTarget.Default.TargetPath = DragDropButtonText;
							var encodedpath = Path.GetFullPath(DragDropButtonText).Encode();
							WriteSomeStuff(
								"Added a Target Directory",
								$"Directory Name: “{Path.GetFileName(DragDropButtonText)}”\n",
								$"Full Path: “{DragDropButtonText}”\n",
								$"Coded Path: “{encodedpath}”"
							);
            }
            else if (File.Exists(DragDropButtonText))
            {
							var fn = DragDropButtonText.GetFileInfo();
							if (fn.Name.ToLower() == "youtube-dl.exe")
							{
								ConfigModel.Instance.PathYoutubeDL = fn.Directory.FullName;
								UpdateEnvironmentPath(); ConfigModel.Instance.Save();
								var encodedpath = Path.GetFullPath(DragDropButtonText).Encode();
								WriteSomeStuff(
									"Updated youtube-dl.exe executable path...",
									$"Directory Name: “{Path.GetFileName(DragDropButtonText)}”\n",
									$"Full Path: “{DragDropButtonText}”\n",
									$"Coded Path: “{encodedpath}”"
								);
							}
							else if (fn.Name.ToLower() == "avconv.exe")
							{
								ConfigModel.Instance.PathAVConv = fn.Directory.FullName;
								UpdateEnvironmentPath(); ConfigModel.Instance.Save();
								var encodedpath = Path.GetFullPath(DragDropButtonText).Encode();
								WriteSomeStuff(
									"Updated avconv.exe executable path...",
									$"Directory Name: “{Path.GetFileName(DragDropButtonText)}”\n",
									$"Full Path: “{DragDropButtonText}”\n",
									$"Coded Path: “{encodedpath}”"
								);
							}
							else if (fn.Name.ToLower() == "ffmpeg.exe")
							{
								ConfigModel.Instance.PathFFmpeg = fn.Directory.FullName;
								UpdateEnvironmentPath(); ConfigModel.Instance.Save();
								var encodedpath = Path.GetFullPath(DragDropButtonText).Encode();
								WriteSomeStuff(
									"Updated ffmpeg.exe executable path...",
									$"Directory Name: “{Path.GetFileName(DragDropButtonText)}”\n",
									$"Full Path: “{DragDropButtonText}”\n",
									$"Coded Path: “{encodedpath}”"
								);
							}
            }
          }
        });
    }

    readonly object L = new object();
    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
      lock (L) foreach (var k in CommandHandlers) if (keyData.IsMatch(k.Keys)) k.Action(this);
      return base.ProcessCmdKey(ref msg, keyData);
    }

    void Event_BeginDownloadType(object sender, EventArgs e) {
      var l = sender as ToolStripMenuItem;
      ConfigModel.Instance.TargetType = l.Text;
      lbLast.Text = $"[{ConfigModel.Instance.TargetType}]";
      Worker_Begin();
    }
    void Event_BeginDownload(object sender, EventArgs e) { Worker_Begin(); }

    void TextBox1TextChanged(object sender, EventArgs e)
    {
      ckHasPlaylist.Visible = textBox1.Text.Contains("&list=") || textBox1.Text.Contains("?list=");
      ConfigModel.TargetURI = textBox1.Text;

      if (ConfigModel.Instance.AppFlags.HasFlag(YoutubeDlFlags.NameFromURL))
        SetStatus(ConfigModel.TargetURI.GetBasenameFromURL());
    }

    void IUI.SetStatus(string text) { SetStatus(text); }

    void SetStatus(string text)
    {
      statText.Text = text;
      statusTimer.Start();
    }

    // interface: IUI

    TextBox IUI.TextInput { get { return textBox1; } }
    RichTextBox IUI.OutputRTF { get { return richTextBox1; } }
    void IUI.Worker_Begin() => Worker_Begin();

    static internal List<CommandKeyHandler<IUI>> CommandHandlers /* { get; private set; }*/ = new List<CommandKeyHandler<IUI>>(){
      new CommandKeyHandler<IUI>{Name="Output: Clear Output Text",Keys=Keys.C|Keys.Alt, Action = Actions.COutputClear },
      new CommandKeyHandler<IUI>{Name="Output: Show Splash Document",Keys=Keys.R|Keys.Alt, Action = Actions.COutputSplash },
      new CommandKeyHandler<IUI>{Name="Output: Toggle Word-Wrap",Keys=Keys.Z|Keys.Alt, Action = Actions.COutputWordWrap },
      new CommandKeyHandler<IUI>{Name="Output: Focus", Keys=Keys.Alt|Keys.D, Action = (f)=> f.TextInput.Focus() },
      new CommandKeyHandler<IUI>{Name="Outout: Short-Cut Keys", Keys=Keys.F5, Action = Actions.COutputShortcuts },
      new CommandKeyHandler<IUI>{Name="Outout: Reset Zoom Factor", Keys=Keys.NumPad0|Keys.Control, Action = Actions.COutputZoomReset },
      new CommandKeyHandler<IUI>{Name="Output: Show Embedded Font Families", Keys=Keys.Control|Keys.Shift|Keys.F, Action=Actions.COutputEmbededFonts},
      new CommandKeyHandler<IUI>{Name="Output: Show Current Download Target Path", Keys=Keys.Control|Keys.T, Action=Actions.COutputCurrentTargetPath},
      new CommandKeyHandler<IUI>{Name="Shell: Explore to Path",Keys=Keys.E|Keys.Control, Action =(IUI f)=>Actions.ExploreToPath()},
      new CommandKeyHandler<IUI>{Name="Run Using Last Taret-Type", Keys=Keys.Control|Keys.Enter, Action=Actions.CRunLastType},
      new CommandKeyHandler<IUI>{Name="Test Download (Atomic Parsley)", Keys=Keys.Control|Keys.Shift|Keys.D, Action=DownloadTargetFile.TestDownloadAtomicParsley},
    };

    void IRestoreBounds.WindowStateToConfig()
    {
      if (WindowState == FormWindowState.Minimized || WindowState == FormWindowState.Maximized)
        ConfigModel.Instance.RestoreBounds = $"{RestoreBounds.X}, {RestoreBounds.Y}, {RestoreBounds.Width}, {RestoreBounds.Height}";
      else
        ConfigModel.Instance.RestoreBounds = $"{Left}, {Top}, {Width}, {Height}";
    }
    Rectangle IRestoreBounds.WindowStateToRect()
    {
      var arect = ConfigModel.Instance.RestoreBounds.SplitComma();
      if (arect.Length < 4) return System.Drawing.Rectangle.Empty;
      var rect = System.Drawing.Rectangle.Empty;
      int temp = 0;
      if (int.TryParse(arect[0], out temp)) rect.X = temp;
      if (int.TryParse(arect[1], out temp)) rect.Y = temp;
      if (int.TryParse(arect[2], out temp)) rect.Width = temp;
      if (int.TryParse(arect[3], out temp)) rect.Height = temp;
      return rect;
    }
    void IRestoreBounds.WindowStateToForm()
    {
      if (!string.IsNullOrEmpty(ConfigModel.Instance.RestoreBounds))
      {
        var bounds = (this as IRestoreBounds).WindowStateToRect();
        Left = bounds.X;
        Top = bounds.Y;
        Width = bounds.Width;
        Height = bounds.Height;
      }
      else
      {
      }
    }

		void WriteSomeStuff(string heading, params string[] lines) { WriteSomeStuff(true, heading, lines); }
		void WriteSomeStuff(bool clear, string heading, params string[] lines)
		{
			using (var boldFont = new System.Drawing.Font(richTextBox1.Font.FontFamily, 14.0f, System.Drawing.FontStyle.Bold))
				using (var reguFont = new System.Drawing.Font(richTextBox1.Font.FontFamily, 12.0f, System.Drawing.FontStyle.Regular))
			{
        if (clear)
        {
          richTextBox1.SelectAll();
          richTextBox1.SelectionTabs = null;
          richTextBox1.Clear();
        }
        if (!string.IsNullOrEmpty(heading))
        {
          richTextBox1.SelectionIndent = 10;
          richTextBox1.SelectionFont = boldFont;
          richTextBox1.AppendText($"{heading}\n\n");
        }
				richTextBox1.SelectionFont = reguFont;
				richTextBox1.SelectionIndent = 20;
				foreach (var line in lines) richTextBox1.AppendText($"{line}\n");
			}
		}
    void IUI.Print(string heading, params string[] lines) { WriteSomeStuff(heading, lines); }
    void IUI.PrintMore(string heading, params string[] lines) { WriteSomeStuff(false, heading, lines); }
  }

  interface IRestoreBounds
  {
    void WindowStateToConfig();
    Rectangle WindowStateToRect();
    void WindowStateToForm();
  }

}
