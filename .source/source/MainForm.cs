﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace YouTubeDownloadUtil
{
  public partial class MainForm : Form, IUI, IRestoreBounds
  {
    readonly Color colorDark = Color.FromArgb(64, 64, 64);
    readonly Color colorLight = SystemColors.ControlLight;

    ToolStripItem[] TogglableControls { get { return new ToolStripItem[] { lbLast }; } }

    void DownloadTargetClickHandler(object sender, EventArgs e)
    {
      var value = (sender as ToolStripMenuItem).Tag as string;
      var n = Path.GetFileName(DownloadTarget.Default.TargetPath = value);
      DownloadTarget.Default.TargetPath = (ConfigModel.Instance.TargetOutputDirectory = value);
      Text = $"Dir: {n}";
      UpdateDownloadTargets();
      ConfigModel.Instance.Save();
    }

    void UpdateEnvironmentPath()
    {
      var pathVars = new List<string>();
      if (ConfigModel.Instance.PathAVConv.DirectoryExistsAndNonempty()) pathVars.Add(ConfigModel.Instance.PathAVConv);
      if (ConfigModel.Instance.PathFFmpeg.DirectoryExistsAndNonempty()) pathVars.Add(ConfigModel.Instance.PathFFmpeg);
      if (ConfigModel.Instance.PathYoutubeDL.DirectoryExistsAndNonempty()) pathVars.Add(ConfigModel.Instance.PathYoutubeDL);
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

    public MainForm()
    {
      InitializeComponent();

      statusTimer = new Timer { Interval = 3000, Enabled = false };
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
      CreateToolStrip();

      // events
      btnAbortProcess.MouseDown += Event_ButtonShowContext;
      textBox1.TextChanged += TextBox1TextChanged;

      ConfigModel.Instance.BeforeSaved += (s,e) => (this as IRestoreBounds).WindowStateToConfig();
      ConfigModel.Instance.Saved += (o, a) => SetStatus("Saved Configuration");
      ConfigModel.Instance.FlagsChanged += (o, a) => ConfigModel.Instance.Save();
      textMaxDownloads.TextChanged += (a, b) => ConfigModel.Instance.MaxDownloads = textMaxDownloads.Text;
      FormClosing += (s, e) => ConfigModel.Instance.Save(true);

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
              Text = "is directory";
              ConfigModel.Instance.AddDirectory(DragDropButtonText);
              UpdateDownloadTargets();
              DownloadTarget.Default.TargetPath = DragDropButtonText;
            }
            else if (File.Exists(DragDropButtonText))
            {
              var fn = DragDropButtonText.GetFileInfo();
              if (fn.Name.ToLower() == "youtube-dl.exe")
              {
                ConfigModel.Instance.PathYoutubeDL = fn.Directory.FullName;
                UpdateEnvironmentPath(); ConfigModel.Instance.Save();
              }
              else if (fn.Name.ToLower() == "avconv.exe")
              {
                ConfigModel.Instance.PathAVConv = fn.Directory.FullName;
                UpdateEnvironmentPath(); ConfigModel.Instance.Save();
              }
              else if (fn.Name.ToLower() == "ffmpeg.exe")
              {
                ConfigModel.Instance.PathFFmpeg = fn.Directory.FullName;
                UpdateEnvironmentPath(); ConfigModel.Instance.Save();
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

    void Event_BeginDownloadType(object sender, EventArgs e) { var l = sender as ToolStripMenuItem; ConfigModel.Instance.TargetType = l.Text; lbLast.Text = $"[{ConfigModel.Instance.TargetType}]"; Worker_Begin(); }
    void Event_BeginDownload(object sender, EventArgs e) { Worker_Begin(); }
    void TextBox1TextChanged(object sender, EventArgs e)
    {
      ckHasPlaylist.Visible = textBox1.Text.Contains("&list=") || textBox1.Text.Contains("?list=");
      ConfigModel.TargetURI = textBox1.Text;

      if (ConfigModel.Instance.AppFlags.HasFlag(YoutubeDlFlags.NameFromURL))
        SetStatus(ConfigModel.TargetURI.GetBasenameFromURL());
    }

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
      new CommandKeyHandler<IUI>{Name="Output: Show Target WorkPath (Output-Dir)",Keys=Keys.D|Keys.Control, Action = Actions.COutputWorkPath },
      new CommandKeyHandler<IUI>{Name="Output: Focus", Keys=Keys.Alt|Keys.D, Action = (f)=> f.TextInput.Focus() },
      new CommandKeyHandler<IUI>{Name="Outout: Short-Cut Keys", Keys=Keys.F5, Action = Actions.COutputShortcuts },
      new CommandKeyHandler<IUI>{Name="Outout: Reset Zoom Factor", Keys=Keys.NumPad0|Keys.Control, Action = Actions.COutputZoomReset },
      new CommandKeyHandler<IUI>{Name="Shel: Explore to Path",Keys=Keys.E|Keys.Control, Action =(IUI f)=>Actions.ExploreToPath()},
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

  }

  interface IRestoreBounds
  {
    void WindowStateToConfig();
    Rectangle WindowStateToRect();
    void WindowStateToForm();
  }

}
