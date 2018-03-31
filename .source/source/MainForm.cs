using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace YouTubeDownloadUtil
{
  public partial class MainForm : Form, IUI
  {
    readonly Color colorDark  = Color.FromArgb(64,64,64);
    readonly Color colorLight = SystemColors.ControlLight;
    
    ToolStripMenuItem[] TogglableControls { get { return new ToolStripMenuItem[]{lbM4a, lbMp3, lbMp4, lbLast, lbBest}; } }
    
    const string msgAllreadyDownloaded  = "has already been downloaded";
    const string msgDownloadHeading     = "[download] ";
    const string msgDownloadDestination = "[download] Destination: ";
    
    void DownloadTargetClickHandler(object sender, EventArgs e)
    {
      var value =  (sender as ToolStripMenuItem).Tag as string;
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
      System.Environment.SetEnvironmentVariable("PATH",$"{newPath};{ConfigModel.OriginalPath}");
    }
    
    public MainForm()
    {
      InitializeComponent();
      
      textBox1.TextChanged += TextBox1TextChanged;
      
      UpdateEnvironmentPath();

      Actions.COutputSplash(this);
      
      FormClosing += (object sender, FormClosingEventArgs e) => ConfigModel.Instance.Save();
      
      CreateToolStrip();
      
      this.ApplyDragDropMethod(
        (sender,e)=>{
          if (e.Data.GetDataPresent(DataFormats.Text) ||
              e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        },
        (sender,e)=>{
          if (e.Data.GetDataPresent(DataFormats.Text))
          {
            DragDropButtonText = (string)e.Data.GetData(DataFormats.Text);
            textBox1.Text = (string)e.Data.GetData(DataFormats.Text);
          }
          else if (e.Data.GetDataPresent(DataFormats.FileDrop))
          {
            DragDropButtonText = (e.Data.GetData(DataFormats.FileDrop) as string[]).FirstOrDefault();
            if (Directory.Exists(DragDropButtonText))
            {
              cm.Show(button1,new Point(button1.Width,button1.Height), ToolStripDropDownDirection.BelowLeft);
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
    
    readonly object L= new object();
    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
      lock (L) foreach (var k in CommandHandlers) if (keyData.IsMatch(k.Keys)) k.Action(this);
      return base.ProcessCmdKey(ref msg, keyData);
    }
    
    void Event_BeginDownloadType(object sender, EventArgs e) { var l = sender as ToolStripMenuItem; ConfigModel.Instance.TargetType = l.Text; lbLast.Text = $"[{ConfigModel.Instance.TargetType}]"; Worker_Begin(); }
    void Event_BeginDownload(object sender, EventArgs e) { Worker_Begin(); }
    void TextBox1TextChanged(object sender, EventArgs e) {
      ckHasPlaylist.Checked = textBox1.Text.Contains("&list=") || textBox1.Text.Contains("?list=");
      ConfigModel.TargetURI = textBox1.Text;
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
      new CommandKeyHandler<IUI>{Name="Output: Focus", Keys=Keys.F1, Action = (f)=> f.TextInput.Focus() },
      new CommandKeyHandler<IUI>{Name="Outout: Short-Cut Keys", Keys=Keys.F5, Action = Actions.COutputShortcuts },
      new CommandKeyHandler<IUI>{Name="Outout: Reset Zoom Factor", Keys=Keys.NumPad0|Keys.Control, Action = Actions.COutputZoomReset },
      new CommandKeyHandler<IUI>{Name="Shel: Explore to Path",Keys=Keys.E|Keys.Control, Action =(IUI f)=>Actions.ExploreToPath()},
      new CommandKeyHandler<IUI>{Name="Run Using Last Taret-Type", Keys=Keys.Control|Keys.Enter, Action=Actions.CRunLastType},
      new CommandKeyHandler<IUI>{Name="Test Download", Keys=Keys.Control|Keys.Shift|Keys.D, Action=DownloadTargetFile.TestDownloadAtomicParsley},
    };
  }
}
