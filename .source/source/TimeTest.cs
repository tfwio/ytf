/*
 * User: xo
 * Date: 4/8/2018
 * Time: 6:46 AM
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using TimeHelper;

namespace YouTubeDownloadUtil
{
  /// <summary>
  /// Description of TimeTest.
  /// </summary>
  public partial class TimeTest : Form
  {
    public string MyTimeStart { get; set; }
    
    public TimeTest()
    {
      InitializeComponent();
      
      tbCommandText.Text = "0:0.000";
      textBox1.ReadOnly = true;
      
      tbCommandText.TextChanged += TbCommandTextTextChanged;
      
    }
    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      tbCommandText.DataBindings.Clear();
      var bin=new Binding("Text",this,"MyTimeStart");
      bin.FormatInfo = new TimeSpanStringFormatter();
      bin.Format += (sender, v) => {
        var s = v.Value==null ? "0" : v.Value as string;
        if (!(s.Contains(":")))
          v.Value = TimeClass.FromMilliseconds(double.Parse(s));
        else
          v.Value = new TimeClass(s as string).ToString();
      };
      tbCommandText.DataBindings.Add(bin);
    }
    
    Timer valueTimeOut = new Timer{Interval=1000};
    
    void TbCommandTextTextChanged(object sender, EventArgs e)
    {
      if (valueTimeOut.Enabled)
      {
        valueTimeOut.Stop();
        valueTimeOut.Start();
        this.textBox1.Text = "…";
        return;
      }
      var tv = Convert.ToInt32(tbCommandText.Text.StringToTimeSpan().TotalSeconds * 1000);
      this.textBox1.Text = $"{tv}";
    }
    void Button1Click(object sender, EventArgs e)
    {
      Close();
    }
  }
}
