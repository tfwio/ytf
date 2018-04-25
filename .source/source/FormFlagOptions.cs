using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using F = YouTubeDownloadUtil.YoutubeDlFlags;
using R = YouTubeDownloadUtil.ResourceStrings;
namespace YouTubeDownloadUtil
{
  public partial class FormFlagOptions : Form
  {
    public FormFlagOptions()
    {
      InitializeComponent();
      toolTipControl.MaximumSize = new Size(270, toolTipControl.MaximumSize.Height);
      foreach (F i in Enum.GetValues(typeof(F)))
      {
        var s = $"${i}";
        if (!ConfigModel.FlagUsage.ContainsKey(i)) continue;
        var dic = ConfigModel.FlagUsage[i];
        if (s.Contains("None")) continue;
        if (s.Contains("Reserved")) continue;
        var checker = new CheckBox()
        {
          Name = $"ck{i}",
          Text = dic.Name,
          Checked = ConfigModel.Instance.AppFlags.HasFlag(i),
          Tag = i,
          Width=120,
          Height = 24,
        };
        if (!string.IsNullOrEmpty(dic.Value)) toolTipControl.SetToolTip(
          checker, $"<b>{dic.Name}</b><br/>{dic.Value}"
          );
        checker.CheckedChanged += Lick;
        this.flowLayoutPanel1.Controls.Add(checker);
      }
    }
    struct NamedValue
    {
      public string Name;
      public string Value;
    }
    void Lick(object o, EventArgs l) => ConfigModel.Instance.AppFlags ^= (F)(o as CheckBox).Tag;
    protected override void OnFormClosing(FormClosingEventArgs e)
    {
      foreach (CheckBox checker in this.flowLayoutPanel1.Controls) checker.CheckedChanged -= Lick;
      base.OnFormClosing(e);
    }
  }
}
