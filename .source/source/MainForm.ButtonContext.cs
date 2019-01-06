using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using F= YouTubeDownloadUtil.YoutubeDlFlags;
namespace YouTubeDownloadUtil
{
	static class TSItemHelper
	{
		static public ToolStripMenuItem AddCheckItem(this ToolStripMenuItem parent, F flag, string text, string tooltip, Action<ToolStripMenuItem> action)
		{
			var item = parent.DropDownItems.Add(text) as ToolStripMenuItem;
			item.ToolTipText = tooltip;
			item.Tag = flag;
			item.Checked = ConfigModel.Instance.AppFlags.HasFlag(flag);
			ConfigModel.Instance.FlagsChanged += (s, e) => item.Checked = ConfigModel.Instance.AppFlags.HasFlag(flag);
			if (action != null)
			{
				item.CheckOnClick = true;
				item.Click += (s, e) => action(item);
			}
			return item;
		}
		static public ToolStripMenuItem AddCheckItem(this ToolStripMenuItem parent, F flag, string text, Action<ToolStripMenuItem> action)
		{
			return parent.AddCheckItem(flag, text,null,action);
		}
	}
	partial class MainForm
	{
		internal ContextMenuStrip cm = new ContextMenuStrip();
		internal ToolStripMenuItem mExplore, mRemovePath, mTopLevel, mNameFromURL, mOptions, mAbortOnDuplicate, mAddMetadata, mContinue, mEmbedSubs, mEmbedThumb, mFlatPlaylist, mGetPlaylist, mIgnoreErrors, mSimulate, mVerbose, mWriteAutoSub, mWriteSubs, mDownloadTargets, mWriteAnnotations, mPreferFFmpeg, mExtractAudio, mMaxDownloads;
		
		string DragDropButtonText = string.Empty; // used for temporary storage on drag-enter/drop.
		
		const ToolStripDropDownDirection DropDownDirection = ToolStripDropDownDirection.BelowLeft;
		
		void ShowButtonMenu(Control target) { cm.Show(target, new Point(target.Width,target.Height), DropDownDirection); }
		
		internal void Event_ButtonShowContext(object sender, MouseEventArgs e) { cm.Show(btnAbortProcess,new Point(btnAbortProcess.Width,btnAbortProcess.Height), DropDownDirection); cm.Focus(); }

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
			foreach (var itemPath in dt)
			{
				if (string.IsNullOrEmpty(itemPath)) continue;
				var tempPath = Path.GetFullPath(itemPath.Decode());
				if (!Directory.Exists(tempPath)) continue;
				
				var itm = new ToolStripMenuItem(Path.GetFileName(tempPath)){
					Tag = tempPath,
					ToolTipText = tempPath,
					Checked = (tempPath == ConfigModel.Instance.TargetOutputDirectory)
				};
				itm.Click += DownloadTargetClickHandler;
				mDownloadTargets.DropDownItems.Add(itm);
			}
		}

		FormFlagOptions flagOptions = new FormFlagOptions();

		void CreateToolStrip()
		{
			cm.Items.Clear();
			mOptions          = cm.Items.Add(ResourceStrings.mOptions) as ToolStripMenuItem;
			mExplore          = cm.Items.Add(ResourceStrings.mExplore, null, (s,e)=> Actions.ExploreToPath()) as ToolStripMenuItem;
			mTopLevel         = cm.Items.Add(ResourceStrings.mTopLevel, null, (s,e)=> {
			                                 	mTopLevel.Checked = (TopMost = (ConfigModel.Instance.KeepOnTop = !ConfigModel.Instance.KeepOnTop));
			                                 	ConfigModel.Instance.Save();
			                                 }) as ToolStripMenuItem;
			mTopLevel.ToolTipText = ResourceStrings.mTopLevelTip;
			cm.Items.Add("Browse Flags", null, (s,e)=> flagOptions.ShowDialog(this));
			mRemovePath       = cm.Items.Add(ResourceStrings.mRemovePath, null, (s,e)=> RemoveTargetPath()) as ToolStripMenuItem;
			// Flags
			mAbortOnDuplicate = mOptions.AddCheckItem(F.AbortOnDuplicate,ResourceStrings.mAbortOnDuplicate, FlagsFromMenu);
			mNameFromURL      = mOptions.AddCheckItem(F.NameFromURL,     ResourceStrings.mNameFromURL,      ResourceStrings.mNameFromURL_Msg, FlagsFromMenu);
			mContinue         = mOptions.AddCheckItem(F.Continue,        ResourceStrings.mContinue,         ResourceStrings.mContinueTip, FlagsFromMenu);
			mIgnoreErrors     = mOptions.AddCheckItem(F.IgnoreErrors,    ResourceStrings.mIgnoreErrors,     ResourceStrings.mIgnoreErrors, FlagsFromMenu);
			mVerbose          = mOptions.AddCheckItem(F.Verbose,         ResourceStrings.mVerbose,          ResourceStrings.mVerbose, FlagsFromMenu);
			mSimulate         = mOptions.AddCheckItem(F.Simulate,        ResourceStrings.mSimulate,         ResourceStrings.mSimulate, FlagsFromMenu);
			mAddMetadata      = mOptions.AddCheckItem(F.AddMetadata,     ResourceStrings.mAddMetadata,      ResourceStrings.mAddMetaDataTip, FlagsFromMenu);
			mEmbedSubs        = mOptions.AddCheckItem(F.EmbedSubs,       ResourceStrings.mEmbedSubs,        ResourceStrings.mEmbedSubsTip,   FlagsFromMenu);
			mEmbedThumb       = mOptions.AddCheckItem(F.EmbedThumb,      ResourceStrings.mEmbedThumb,       ResourceStrings.mEmbedThumbTip,  FlagsFromMenu);
			mGetPlaylist      = mOptions.AddCheckItem(F.GetPlaylist,     ResourceStrings.mGetPlaylist,      ResourceStrings.mGetPlaylistTip, FlagsFromMenu);
			mFlatPlaylist     = mOptions.AddCheckItem(F.FlatPlaylist,    ResourceStrings.mFlatPlaylist,     ResourceStrings.FlatPlaylist,    FlagsFromMenu);
			mMaxDownloads     = mOptions.AddCheckItem(F.MaxDownloads,    ResourceStrings.mMaxDownloads,     ResourceStrings.mMaxDownloadsTip, FlagsFromMenu);
			mWriteAutoSub     = mOptions.AddCheckItem(F.WriteAutoSubs,   ResourceStrings.mWriteAutoSub,     ResourceStrings.mWriteAutoSubTip, FlagsFromMenu);
			mWriteAnnotations = mOptions.AddCheckItem(F.WriteAnnotations,ResourceStrings.mWriteAnnotations, ResourceStrings.mWriteAnnotationsTip, FlagsFromMenu);
			mWriteSubs        = mOptions.AddCheckItem(F.WriteSubs,       ResourceStrings.mWriteSubs,        ResourceStrings.mWriteSubsTip,    FlagsFromMenu);
			mExtractAudio     = mOptions.AddCheckItem(F.ExtractAudio,    ResourceStrings.mExtractAudio,     ResourceStrings.mExtractAudioTip, FlagsFromMenu);
			mPreferFFmpeg     = mOptions.AddCheckItem(F.PreferFFmpeg,    ResourceStrings.mPreferFFmpeg,     ResourceStrings.mPreferFFmpegTip, FlagsFromMenu);
			mOptions.DropDownItems.Insert(cm.Items.IndexOf(mExplore), new ToolStripSeparator());
			//mOptions.DropDownItems.Insert(mOptions.DropDownItems.IndexOf(mAbortOnDuplicate),new ToolStripSeparator());
			mOptions.DropDownItems.Insert(mOptions.DropDownItems.IndexOf(mContinue),new ToolStripSeparator());
			mOptions.DropDownItems.Insert(mOptions.DropDownItems.IndexOf(mAddMetadata),new ToolStripSeparator());
			mOptions.DropDownItems.Insert(mOptions.DropDownItems.IndexOf(mGetPlaylist),new ToolStripSeparator());
			mOptions.DropDownItems.Insert(mOptions.DropDownItems.IndexOf(mWriteAutoSub),new ToolStripSeparator());
			mOptions.DropDownItems.Insert(mOptions.DropDownItems.IndexOf(mExtractAudio),new ToolStripSeparator());
			mOptions.DropDownItems.Insert(mOptions.DropDownItems.IndexOf(mPreferFFmpeg),new ToolStripSeparator());
			// Targets

			mDownloadTargets = cm.Items.Add(ResourceStrings.mDownloadTargets) as ToolStripMenuItem;
			
			var shf = cm.Items.Add(ResourceStrings.mShellFolders) as ToolStripMenuItem;
			ShellFolderItem(shf, "%USERPROFILE%\\Desktop");
			ShellFolderItem(shf, "%USERPROFILE%\\Documents");
			ShellFolderItem(shf, "%USERPROFILE%\\Downloads");
			ShellFolderItem(shf, "%USERPROFILE%\\Music");
			ShellFolderItem(shf, "%USERPROFILE%\\Videos");
			
			lbLast.Text = $"[{ConfigModel.Instance.TargetType}]"; // initial target-type is m4a (itunes audio)
			
			cm.Items.Add("-");
			cm.Items.Add("Time to Milliseconds",null,(a,e)=>{
			             	timeCalculatorForm.ShowDialog(this);
			             });
			
			// load defaults

			UpdateDownloadTargets();
			lbMaxDownloads.Visible = (textMaxDownloads.Visible = mMaxDownloads.Checked);
		}
		TimeTest timeCalculatorForm = new TimeTest();
		
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

		void FlagsToMenu(ToolStripMenuItem item)
		{
			var flag = (F)item.Tag;
			item.Checked = ConfigModel.Instance.AppFlags.HasFlag(flag);
			lbMaxDownloads.Visible = (textMaxDownloads.Visible = mMaxDownloads.Checked);
		}
		void FlagsFromMenu(ToolStripMenuItem item)
		{
			ConfigModel.Instance.AppFlags ^= (F)item.Tag;
			lbMaxDownloads.Visible = (textMaxDownloads.Visible = mMaxDownloads.Checked);
			Event_ButtonShowContext(null, null);
			mOptions.ShowDropDown();
			ConfigModel.Instance.Save();
		}
		
	}
}
