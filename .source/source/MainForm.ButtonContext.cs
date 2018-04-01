using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace YouTubeDownloadUtil
{
  static class TSItemHelper
  {
    static public ToolStripMenuItem AddCheckItem(this ToolStripMenuItem parent, string text, string tooltip, Action action)
    {
      var item = parent.DropDownItems.Add(text) as ToolStripMenuItem;
      item.ToolTipText = tooltip;
      if (action != null)
      {
        item.CheckOnClick = true;
        item.Click += (e, x) => action();
      }
      return item;
    }
    static public ToolStripMenuItem AddCheckItem(this ToolStripMenuItem parent, string text, Action action)
    {
      return parent.AddCheckItem(text,null,action);
    }
  }
  partial class MainForm
  {
    ContextMenuStrip cm;
    ToolStripMenuItem mExplore, mRemovePath, mOptions, mAbortOnDuplicate, mAddMetadata, mContinue, mEmbedSubs, mEmbedThumb, mFlatPlaylist, mGetPlaylist, mIgnoreErrors, mVerbose, mWriteAutoSubs, mWriteSubs, mDownloadTargets;
    
    string DragDropButtonText = string.Empty; // used for temporary storage on drag-enter/drop.
    
    const ToolStripDropDownDirection DropDownDirection = ToolStripDropDownDirection.BelowLeft;
    
    void ShowButtonMenu(Control target) { cm.Show(target, new Point(target.Width,target.Height), DropDownDirection); }
    
    void Button1MouseDown(object sender, MouseEventArgs e) { cm.Show(btnAbortProcess,new Point(btnAbortProcess.Width,btnAbortProcess.Height), DropDownDirection); cm.Focus(); }

    void RemoveTargetPath()
    {
      var dir = ConfigModel.Instance.TargetOutputDirectory;
      var msg = $"You are about to remove:\n\"{dir}\"\nfrom your selectable target paths!\nAre your sure?";
      if (MessageBox.Show(msg, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
      {
        if (ConfigModel.Instance.RemoveDirectory(dir))
        {
          MessageBox.Show($"Successully removed {dir} from your collection.", ResourceStrings.msgCreateTsSuccessCaption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk); UpdateDownloadTargets();
        }
        else MessageBox.Show(ResourceStrings.msgCreateTsFail, ResourceStrings.msgCreateTsFailCaption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      }
    }

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
      mOptions          = cm.Items.Add(ResourceStrings.mOptions) as ToolStripMenuItem;
      cm.Items.Add("-");
      mExplore          = cm.Items.Add(ResourceStrings.mExplore, null, (s,e)=> Actions.ExploreToPath()) as ToolStripMenuItem;
      mRemovePath       = cm.Items.Add(ResourceStrings.mRemovePath, null, (s,e)=> RemoveTargetPath()) as ToolStripMenuItem;
      cm.Items.Add("-");

      // AppFlags

      mAbortOnDuplicate = mOptions.AddCheckItem(ResourceStrings.mAbortOnDuplicate, FlagsFromMenu);
      // YtFlags
      mAddMetadata      = mOptions.AddCheckItem(ResourceStrings.mAddMetadata, FlagsFromMenu);
      mContinue         = mOptions.AddCheckItem(ResourceStrings.mContinue, FlagsFromMenu);
      mEmbedSubs        = mOptions.AddCheckItem(ResourceStrings.mEmbedSubs, FlagsFromMenu);
      mEmbedThumb       = mOptions.AddCheckItem(ResourceStrings.mEmbedThumb, FlagsFromMenu);
      mGetPlaylist      = mOptions.AddCheckItem(ResourceStrings.mGetPlaylist, FlagsFromMenu);
      mFlatPlaylist     = mOptions.AddCheckItem(ResourceStrings.mFlatPlaylist, ResourceStrings.FlatPlaylist, FlagsFromMenu);
      mOptions.DropDownItems.Add("-");
      mIgnoreErrors     = mOptions.AddCheckItem(ResourceStrings.mIgnoreErrors, FlagsFromMenu);
      mVerbose          = mOptions.AddCheckItem(ResourceStrings.mVerbose, FlagsFromMenu);
      mWriteAutoSubs    = mOptions.AddCheckItem(ResourceStrings.mWriteAutoSubs, FlagsFromMenu);
      mWriteSubs        = mOptions.AddCheckItem(ResourceStrings.mWriteSubs, FlagsFromMenu);
      
      // Targets

      mDownloadTargets  = cm.Items.Add(ResourceStrings.mDownloadTargets) as ToolStripMenuItem;
      
      var shf = cm.Items.Add(ResourceStrings.mShellFolders) as ToolStripMenuItem;
      ShellFolderItem(shf, "%USERPROFILE%\\Desktop");
      ShellFolderItem(shf, "%USERPROFILE%\\Documents");
      ShellFolderItem(shf, "%USERPROFILE%\\Downloads");
      ShellFolderItem(shf, "%USERPROFILE%\\Music");
      ShellFolderItem(shf, "%USERPROFILE%\\Videos");
      
      lbLast.Text = $"[{ConfigModel.Instance.TargetType}]"; // initial target-type is m4a (itunes audio)
      
      // load defaults

     UpdateDownloadTargets();
      FlagsToMenu();
    }
    
    ToolStripMenuItem ShellFolderItem(ToolStripMenuItem parent, string key)
    {
      var fi = key.EnvironmentPathFilter().GetDirectoryInfo();
      var shItem = parent.DropDownItems.Add(fi.Name);
      shItem.Tag = fi.FullName;
      shItem.Click += ClickFolderItem;
      Text = fi.Name;
      return shItem as ToolStripMenuItem;
    }
    
    void ClickFolderItem(object sender, EventArgs e){
      DownloadTarget.Default.TargetPath = (sender as ToolStripMenuItem).Tag.ToString();
      ConfigModel.Instance.TargetOutputDirectory = DownloadTarget.Default.TargetPath;
      ConfigModel.Instance.Save();
    }
    
    void FlagsToMenu(){
      mAbortOnDuplicate.Checked   = ConfigModel.Instance.AppFlags.HasFlag(YoutubeDlFlags.AbortOnDuplicate);
      mAddMetadata.Checked        = ConfigModel.Instance.AppFlags.HasFlag(YoutubeDlFlags.AddMetadata);
      mContinue.Checked           = ConfigModel.Instance.AppFlags.HasFlag(YoutubeDlFlags.Continue);
      mEmbedSubs.Checked          = ConfigModel.Instance.AppFlags.HasFlag(YoutubeDlFlags.EmbedSubs);
      mEmbedThumb.Checked         = ConfigModel.Instance.AppFlags.HasFlag(YoutubeDlFlags.EmbedThumb);
      mGetPlaylist.Checked        = ConfigModel.Instance.AppFlags.HasFlag(YoutubeDlFlags.GetPlaylist);
      mFlatPlaylist.Checked       = ConfigModel.Instance.AppFlags.HasFlag(YoutubeDlFlags.FlatPlaylist);
      mIgnoreErrors.Checked       = ConfigModel.Instance.AppFlags.HasFlag(YoutubeDlFlags.IgnoreErrors);
      mVerbose.Checked            = ConfigModel.Instance.AppFlags.HasFlag(YoutubeDlFlags.Verbose);
      mWriteAutoSubs.Checked      = ConfigModel.Instance.AppFlags.HasFlag(YoutubeDlFlags.WriteAutoSubs);
      mWriteSubs.Checked          = ConfigModel.Instance.AppFlags.HasFlag(YoutubeDlFlags.WriteSubs);
    }
    void FlagsFromMenu()
    {
      YoutubeDlFlags F=0;
      if (mAbortOnDuplicate.Checked) F = F | YoutubeDlFlags.AbortOnDuplicate;
      if (mAddMetadata.Checked)      F = F | YoutubeDlFlags.AddMetadata;
      if (mContinue.Checked)         F = F | YoutubeDlFlags.Continue;
      if (mEmbedSubs.Checked)        F = F | YoutubeDlFlags.EmbedSubs;
      if (mEmbedThumb.Checked)       F = F | YoutubeDlFlags.EmbedThumb;
      if (mGetPlaylist.Checked)      F = F | YoutubeDlFlags.GetPlaylist;
      if (mFlatPlaylist.Checked)     F = F | YoutubeDlFlags.FlatPlaylist;
      if (mIgnoreErrors.Checked)     F = F | YoutubeDlFlags.IgnoreErrors;
      if (mVerbose.Checked)          F = F | YoutubeDlFlags.Verbose;
      if (mWriteAutoSubs.Checked)    F = F | YoutubeDlFlags.WriteAutoSubs;
      if (mWriteSubs.Checked)        F = F | YoutubeDlFlags.WriteSubs;
      ConfigModel.Instance.AppFlags = F;
      ConfigModel.Instance.Save();
    }
  
  }
}
