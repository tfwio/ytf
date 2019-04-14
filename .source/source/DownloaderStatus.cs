namespace YouTubeDownloadUtil
{
  struct DownloaderStatus
  {
    const int Max = 3;
    const string msgNoTime = "00:00";
    const string msgNA = "n/a";

    public string Percent { get; set; }// {0:"[download]"}{ 1:"  1%"}
    bool MultipleDownloads { get { return PercentCount > 1; } }

    public string Bits { get; set; }
    public string Speed { get; set; }
    public string ETA { get; set; }
    public string TimeComplete { get; set; }

    // this complicates the idea of our percentage.
    int PercentCount, PercentIndex;

    public float FloatPercent { get { return float.Parse(Percent.Replace("%", string.Empty)); } }
    public int IntPercent { get { return (int)FloatPercent; } }

    public DownloaderStatus(string input, int percentCount = 1, int percentIndex = 0)
    {
      // default number of download items needs to be initialized.
      PercentCount = percentCount;
      PercentIndex = percentIndex;

      var text = input
        .Replace("[download] ", string.Empty)
        .Trim();
      var a=text.Split(' ');
      Percent = "0";
      Bits = msgNA;
      Speed = msgNA;
      ETA = msgNoTime;
      TimeComplete = msgNA;
      try
      {
        Percent = a[0];

        // ___ % of 4.91MiB
        // byte count status.
        if (text.Contains("of"))
        {
          Bits = msgNA;
          Speed = msgNA;
          ETA = msgNoTime;
          TimeComplete = a[2];
        }

        // ___% of ___MiB at ___KiB/s ETA __:__
        // update percent for current download
        else if (text.Contains("at"))
        {
          Bits = a[2];
          Speed = (a.Length > Max) ? a[4] : msgNA;
          ETA = (a.Length > Max) ? a[6] : msgNoTime;
          TimeComplete = msgNA;
        }
        // ___%   of ___MiB in __:__
        // handle multiple downloads
        else if (text.Contains("in"))
        {
          Bits = a[2];
          Speed = (a.Length > Max) ? a[4] : msgNA;
          ETA = (a.Length > Max) ? a[6] : msgNoTime;
          TimeComplete = msgNA;
        }
      } catch {
        System.Windows.Forms.MessageBox.Show($"{text}, len={a.Length}","Error");
        Percent = "0";
        Bits = msgNA;
        Speed = msgNA;
        ETA = msgNoTime;
        TimeComplete = msgNA;
      }
    }
  }
}
