using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace YouTubeDownloadUtil
{
  public partial class MainForm : Form
  {
    Control[] TogglableControls { get { return new Control[]{lbM4a, lbMp3, lbMp4, lbBest, lbLast}; } }
    
    internal List<CommandKeyHandler> CommandHandlers { get; private set; }
    
    string NextTargetType { get; set; } = "m4a";
    
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
    
    public MainForm()
    {
      CommandHandlers = new List<CommandKeyHandler>(){
        new CommandKeyHandler{Keys=Keys.E|Keys.Control, Action = Actions.ExploreTo}
      };
      
      InitializeComponent();
      
      richTextBox1.Rtf = Actions.RtfHelpText();
      richTextBox1.PreviewKeyDown += (a,e)=> { System.Windows.Forms.Message i = System.Windows.Forms.Message.Create(IntPtr.Zero,0,System.IntPtr.Zero,IntPtr.Zero); this.ProcessCmdKey(ref i,e.KeyData); };
      
      System.Environment.SetEnvironmentVariable("PATH",$"{ConfigModel.Instance.PathFFmpeg};{ConfigModel.Instance.PathYoutubeDL};{ConfigModel.OriginalPath}");
      
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
              cm.Show(button1,new Point(button1.Width,button1.Height), ToolStripDropDownDirection.BelowLeft);
              Text = "is file";
            }
          }
        });
    }
    
    readonly object L= new object();
    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
      lock (L) foreach (var k in CommandHandlers) if (keyData.IsMatch(k.Keys)) k.Action();
      return base.ProcessCmdKey(ref msg, keyData);
    }
    
    void Event_BeginDownloadType(object sender, LinkLabelLinkClickedEventArgs e) { var l = sender as LinkLabel; NextTargetType = l.Text; lbLast.Text = $"[{NextTargetType}]"; Worker_Begin(); }
    void Event_BeginDownload(object sender, LinkLabelLinkClickedEventArgs e) { Worker_Begin(); }
    
    void TextBox1TextChanged(object sender, EventArgs e) { ckHasPlaylist.Checked = textBox1.Text.Contains("&list="); }
    
  }
  
}
