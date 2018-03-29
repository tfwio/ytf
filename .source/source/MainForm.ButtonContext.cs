using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace YouTubeDownloadUtil
{
  partial class MainForm
  {
    ContextMenuStrip cm;
    ToolStripMenuItem mExplore, mOptions, mAbortOnDuplicate, mAddMetadata, mContinue, mEmbedSubs, mEmbedThumb, mGetPlaylist, mIgnoreErrors, mVerbose, mWriteAutoSubs, mWriteSubs, mDownloadTargets;
    
    string DragDropButtonText = string.Empty; // used for temporary storage on drag-enter/drop.
    
    const ToolStripDropDownDirection DropDownDirection = ToolStripDropDownDirection.BelowLeft;
    
    void ShowButtonMenu(Control target) { cm.Show(target, new Point(target.Width,target.Height), DropDownDirection); }
    
    void Button1MouseDown(object sender, MouseEventArgs e) { cm.Show(button1,new Point(button1.Width,button1.Height), DropDownDirection); cm.Focus(); }
    
    void UpdateDownloadTargets()
    {
      mDownloadTargets.DropDownItems.Clear();
      var dt = new List<string>(ConfigModel.Instance.DownloadTargetsList).ToArray();
      Array.Sort(dt);
      foreach (var i in dt)
      {
        var itm = mDownloadTargets.DropDownItems.Add(Path.GetFileName(i)) as ToolStripMenuItem;
        itm.Tag = i;
        itm.ToolTipText = i;
        itm.Checked = (i == ConfigModel.Instance.TargetOutputDirectory);
        itm.Click += DownloadTargetClickHandler;
      }
    }
    
    void CreateToolStrip()
    {
      cm = new ContextMenuStrip();
      mOptions          = cm.Items.Add("youtube-dl flags") as ToolStripMenuItem;
      mExplore          = cm.Items.Add("Explore to Target Directory") as ToolStripMenuItem;
      mExplore.Click   += (object sender,EventArgs e)=>Actions.ExploreTo();
      lbLast.Text       = $"[{NextTargetType}]"; // initial target-type is m4a (itunes audio)
      // Flags
      mAbortOnDuplicate = mOptions.DropDownItems.Add("Abort on Duplicate (File Exists)") as ToolStripMenuItem;
      mAddMetadata      = mOptions.DropDownItems.Add("Add MetaData") as ToolStripMenuItem;
      mContinue         = mOptions.DropDownItems.Add("Continue Unfinished Downloads") as ToolStripMenuItem;
      mEmbedSubs        = mOptions.DropDownItems.Add("Embed Subtitles") as ToolStripMenuItem;
      mEmbedThumb       = mOptions.DropDownItems.Add("Embed Thumbnail") as ToolStripMenuItem;
      mGetPlaylist      = mOptions.DropDownItems.Add("Get Playlist") as ToolStripMenuItem;
      mOptions.DropDownItems.Add("-");
      mIgnoreErrors     = mOptions.DropDownItems.Add("Ignore Errors") as ToolStripMenuItem;
      mVerbose          = mOptions.DropDownItems.Add("Verbose") as ToolStripMenuItem;
      mWriteAutoSubs    = mOptions.DropDownItems.Add("Write Auto Subtitles (yt: if present)") as ToolStripMenuItem;
      mWriteSubs        = mOptions.DropDownItems.Add("Write Subtitles (yt: if present") as ToolStripMenuItem;
      // Targets
      mDownloadTargets  = cm.Items.Add("Download Targets") as ToolStripMenuItem;
      foreach (var m in new ToolStripMenuItem[] {mAbortOnDuplicate,mAddMetadata,mContinue,mEmbedSubs,mEmbedThumb,mGetPlaylist,mIgnoreErrors,mVerbose,mWriteAutoSubs,mWriteSubs}){
        m.CheckOnClick = true;
        m.Click += (e,x)=>SetAppFlags();
      }
      // load defaults
      GetAppFlags();
      UpdateDownloadTargets();
    }
    
    void GetAppFlags(){
      mAbortOnDuplicate.Checked   = ConfigModel.Instance.YoutubeDlFlags.HasFlag(YoutubeDlFlags.AbortOnDuplicate);
      mAddMetadata.Checked        = ConfigModel.Instance.YoutubeDlFlags.HasFlag(YoutubeDlFlags.AddMetadata);
      mContinue.Checked           = ConfigModel.Instance.YoutubeDlFlags.HasFlag(YoutubeDlFlags.Continue);
      mEmbedSubs.Checked          = ConfigModel.Instance.YoutubeDlFlags.HasFlag(YoutubeDlFlags.EmbedSubs);
      mEmbedThumb.Checked         = ConfigModel.Instance.YoutubeDlFlags.HasFlag(YoutubeDlFlags.EmbedThumb);
      mGetPlaylist.Checked        = ConfigModel.Instance.YoutubeDlFlags.HasFlag(YoutubeDlFlags.GetPlaylist);
      mIgnoreErrors.Checked       = ConfigModel.Instance.YoutubeDlFlags.HasFlag(YoutubeDlFlags.IgnoreErrors);
      mVerbose.Checked            = ConfigModel.Instance.YoutubeDlFlags.HasFlag(YoutubeDlFlags.Verbose);
      mWriteAutoSubs.Checked      = ConfigModel.Instance.YoutubeDlFlags.HasFlag(YoutubeDlFlags.WriteAutoSubs);
      mWriteSubs.Checked          = ConfigModel.Instance.YoutubeDlFlags.HasFlag(YoutubeDlFlags.WriteSubs);
    }
    void SetAppFlags()
    {
      YoutubeDlFlags F=0;
      if (mAbortOnDuplicate.Checked) F |= YoutubeDlFlags.AbortOnDuplicate;
      if (mAddMetadata.Checked)      F |= YoutubeDlFlags.AddMetadata;
      if (mContinue.Checked)         F |= YoutubeDlFlags.Continue;
      if (mEmbedSubs.Checked)        F |= YoutubeDlFlags.EmbedSubs;
      if (mEmbedThumb.Checked)       F |= YoutubeDlFlags.EmbedThumb;
      if (mGetPlaylist.Checked)      F |= YoutubeDlFlags.GetPlaylist;
      if (mIgnoreErrors.Checked)     F |= YoutubeDlFlags.IgnoreErrors;
      if (mVerbose.Checked)          F |= YoutubeDlFlags.Verbose;
      if (mWriteAutoSubs.Checked)    F |= YoutubeDlFlags.WriteAutoSubs;
      if (mWriteSubs.Checked)        F |= YoutubeDlFlags.WriteSubs;
      ConfigModel.Instance.YoutubeDlFlagsStr = F.ToString();
      ConfigModel.Instance.Save();
    }
  
  }
}
