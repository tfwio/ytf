using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using System.Windows.Forms;

// this looks illegal
using R = YouTubeDownloadUtil.ResourceStrings;

namespace YouTubeDownloadUtil
{
  // want: --flat-playlist
  [Flags] enum YoutubeDlFlags : ulong
  {
    None              = 0,
    AbortOnDuplicate  = 1,
    AddMetadata       = 2,
    Continue          = 4,
    EmbedSubs         = 8,
    EmbedThumb        = 1 * 0x10,
    AutoNumber        = 2 * 0x10,
    FlatPlaylist      = 4 * 0x10,
    GetPlaylist       = 8 * 0x10,
    IgnoreErrors      = 1 * 0x100,
    Verbose           = 2 * 0x100,
    WriteAnnotations  = 4 * 0x100,
    WriteAutoSubs     = 8 * 0x100,
    WriteSubs         = 1 * 0x1000,
    ExtractAudio      = 2 * 0x1000,
    PreferFFmpeg      = 4 * 0x1000,
    MaxDownloads      = 8 * 0x1000,
    Simulate          = 1 * 0x10000,
    NameFromURL       = 2 * 0x10000,
    ReservedC         = 4 * 0x10000,
    ReservedB         = 8 * 0x10000,
    ReservedA         = 1 * 0x100000,
    Reserved9         = 2 * 0x100000,
    Reserved8         = 4 * 0x100000,
    Reserved7         = 8 * 0x100000,
    Reserved6         = 1 * 0x1000000,
    Reserved5         = 2 * 0x1000000,
    Reserved4         = 4 * 0x1000000,
    Reserved3         = 8 * 0x1000000,
    Reserved2         = 1 * 0x10000000,
    Reserved1         = 2 * 0x10000000,
    Reserved0         = 4 * 0x10000000,

  }

  /// <summary>Specify audio format: "best", "aac", "flac", "mp3", "m4a", "opus", "vorbis", or "wav"; "best" by default; No effect without -x.</summary>
  [Flags] enum ExtractAudioTypes {
    none = 0,
    best = 1,
    aac = 2,
    flac = 4,
    mp3 = 8,
    m4a = 1 * 16,
    opus = 2 * 16,
    vorbis = 4 * 16,
    wav = 8 * 16,
  }

  /// <summary>see: --convert-subs flag</summary>
  [Flags] enum ConvertSubTypes {
    srt = 1,
    ass = 2,
    vtt = 4,
    lrc = 8,
  }

  class ConfigModel
  {
    internal struct NamedValue { public string Name; public string Value; }
    
    static internal Dictionary<YoutubeDlFlags, NamedValue> FlagUsage { get; set; } = new Dictionary<YoutubeDlFlags, NamedValue>{
      { YoutubeDlFlags.AbortOnDuplicate , new NamedValue{ Name=R.mAbortOnDuplicate, Value=null                   } },
      { YoutubeDlFlags.AddMetadata      , new NamedValue{ Name=R.mAddMetadata,      Value=R.mAddMetaDataTip      } },
      { YoutubeDlFlags.Continue         , new NamedValue{ Name=R.mContinue,         Value=R.mContinueTip         } },
      { YoutubeDlFlags.EmbedSubs        , new NamedValue{ Name=R.mEmbedSubs,        Value=R.mEmbedSubsTip        } },
      { YoutubeDlFlags.EmbedThumb       , new NamedValue{ Name=R.mEmbedThumb,       Value=R.mEmbedThumbTip       } },
      { YoutubeDlFlags.FlatPlaylist     , new NamedValue{ Name=R.mFlatPlaylist,     Value=null                   } },
      { YoutubeDlFlags.GetPlaylist      , new NamedValue{ Name=R.mGetPlaylist,      Value=R.mGetPlaylistTip      } },
      { YoutubeDlFlags.IgnoreErrors     , new NamedValue{ Name=R.mIgnoreErrors,     Value=R.mIgnoreErrorsTip     } },
      { YoutubeDlFlags.Verbose          , new NamedValue{ Name=R.mVerbose,          Value=R.mVerboseTip          } },
      { YoutubeDlFlags.WriteAnnotations , new NamedValue{ Name=R.mWriteAnnotations, Value=R.mWriteAnnotationsTip } },
      { YoutubeDlFlags.WriteAutoSubs    , new NamedValue{ Name=R.mWriteAutoSub,     Value=R.mWriteAutoSubTip     } },
      { YoutubeDlFlags.WriteSubs        , new NamedValue{ Name=R.mWriteSubs,        Value=R.mWriteSubsTip        } },
      { YoutubeDlFlags.ExtractAudio     , new NamedValue{ Name=R.mExtractAudio,     Value=R.mExtractAudioTip     } },
      { YoutubeDlFlags.PreferFFmpeg     , new NamedValue{ Name=R.mPreferFFmpeg,     Value=R.mPreferFFmpegTip     } },
      { YoutubeDlFlags.MaxDownloads     , new NamedValue{ Name=R.mMaxDownloads,     Value=R.mMaxDownloadsTip     } },
      { YoutubeDlFlags.Simulate         , new NamedValue{ Name=R.mSimulate,         Value=R.mSimulateTip         } },
      { YoutubeDlFlags.NameFromURL      , new NamedValue{ Name=R.mNameFromURL,      Value=R.mNameFromURL_Msg     } },
    };

    static readonly FileInfo confDotIni = new FileInfo(Path.Combine(System.DirectoryHelper.ExecutableDirectory, ResourceStrings.ConfDefault));

    static internal ConfigModel Instance = Load();

    static public readonly string OriginalPath = System.Environment.GetEnvironmentVariable("PATH");

    /// <summary>Whats shown in the text box will go here for access from the worker.</summary>
    internal static string TargetURI { get; set; }

    /// <summary>the active download-target directory</summary>
    [IniKey(Group = "global", Alias = "target")] public string TargetOutputDirectory { get; set; }

    /// <summary>the active download-target directory</summary>
    [IniKey(Group = "global", Alias = "target-type")] public string TargetType { get; set; }

    /// <summary>This is a collection of strings separated by semi-colon.</summary>
    [IniKey(Group = "global", Alias = "target-list")] public string DownloadTargets { get; set; }

    /// <summary>path containing aconv.exe</summary>
    [IniKey(Group = "global", Alias = "AVConv_bin")] public string PathAVConv { get; set; }

    /// <summary>path containing FFmpeg.exe</summary>
    [IniKey(Group = "global", Alias = "FFmpeg_bin")] public string PathFFmpeg { get; set; }

    /// <summary>Path containing youtube-dl and atomicparsley.</summary>
    [IniKey(Group = "global", Alias = "youtube-dl_bin")] public string PathYoutubeDL { get; set; }

    /// <summary>Path containing youtube-dl and atomicparsley.</summary>
    [IniKey(Group = "global", Alias = "flags")] public string YoutubeDlFlagsStr { get; set; }

    /// <summary>arbitrary maximum number of downloads allowed</summary>
    [IniKey(Group = "global", Alias = "max-downloads")] public string MaxDownloads { get; set; }
    [Ignore] public int MaxDownloadsInt { get { return int.Parse(MaxDownloads); } set { MaxDownloads = value.ToString(); } }

    /// <summary>Primary application flags (youtube-dl specific)</summary>
    [Ignore] public YoutubeDlFlags AppFlags {
      get { YoutubeDlFlags l = 0; foreach (var n in YoutubeDlFlagsStr.StringToArray()) { YoutubeDlFlags kk; l |= n.TryParse<YoutubeDlFlags>(out kk) ? kk : 0; } return l; }
      set { YoutubeDlFlagsStr = value.ToString(); OnFlagsChanged(); }
    }

    /// <summary>Extract audio types.</summary>
    [IniKey(Group = "global", Alias = "audio-types")] public string XAudioTypes { get; set; }

    [Ignore] public ExtractAudioTypes AudioTypes {
      get { ExtractAudioTypes l = 0; foreach (var n in XAudioTypes.StringToArray()) { ExtractAudioTypes kk; l |= n.TryParse<ExtractAudioTypes>(out kk) ? kk : 0; } return l; }
      set { XAudioTypes = value.ToString(); }
    }

    /// <summary>Convert subtitle types.</summary>
    [IniKey(Group = "global", Alias = "subtitle-types")] public string XSubtitleTypes { get; set; }

    [Ignore] public ConvertSubTypes SubtitleTypes {
      get { ConvertSubTypes l = 0; foreach (var n in XSubtitleTypes.StringToArray()) { ConvertSubTypes kk; l |= n.TryParse<ConvertSubTypes>(out kk) ? kk : 0; } return l; }
      set { XSubtitleTypes = value.ToString(); }
    }

    [IniKey(Group = "global", Alias = "window-bounds")] public string RestoreBounds { get; set; }

    public event EventHandler Saved;
    protected virtual void OnSaved() => Saved?.Invoke(this, EventArgs.Empty);

    public event EventHandler BeforeSaved;
    protected virtual void OnBeforeSaved() => BeforeSaved?.Invoke(this, EventArgs.Empty);

    internal event EventHandler FlagsChanged;
    protected void OnFlagsChanged() => FlagsChanged?.Invoke(this, EventArgs.Empty);

    ConfigModel()
    {
      SaveTimer = new Timer() { Enabled = false, Interval = 1200 };
      SaveTimer.Tick += (o,a) => SaveAction();
    }

    void SaveAction()
    {
      SaveTimer.Stop();
      OnBeforeSaved(); // get the window state.
      var coll = new IniCollection(this);
      coll.Write(confDotIni);
      OnSaved();
    }
    public void Save(bool ignoreTimer=false)
    {
      if (ignoreTimer) { SaveAction(); return; }
      if (SaveTimer.Enabled) SaveTimer.Stop();
      SaveTimer.Start();
    }
    Timer SaveTimer;

    static public ConfigModel Load() { return Load(DirectoryHelper.ExecutableDirectory); }
    static public ConfigModel Load(string confDir)
    {
      var ini = new ConfigModel() {
        TargetOutputDirectory = ResourceStrings.UserDownloads,
        DownloadTargets = ResourceStrings.UserDownloads,
        YoutubeDlFlagsStr = string.Empty,
        PathFFmpeg = string.Empty,
        PathAVConv = string.Empty,
        MaxDownloads = "1",
        XAudioTypes = string.Empty,
        XSubtitleTypes = string.Empty,
        RestoreBounds = string.Empty,
      };
      if (!confDotIni.Exists) ini.Save();
      var coll = new IniCollection(confDotIni);
      coll.ToInstance(ini);
      return ini;
    }

    [Ignore] public List<string> DownloadTargetsList {
      get {
        var l = new List<string>(DownloadTargets.Split(';'));
        for (int i = 0; i < l.Count; i++) l[i] = l[i];
        l.Sort();
        return l;
      }
      set { DownloadTargets = value.ToArray().JoinSemiColon(); }
    }

    public bool RemoveDirectory(string path)
    {
      var p = Path.GetFullPath(path);
      var tg = new List<string>(DownloadTargetsList);
      if (tg.Contains(p))
      {
        tg.Remove(path);
        DownloadTargetsList = tg;
        Save();
        return true;
      }
      return false;
    }
    public void AddDirectory(string path)
    {
      var tg = new List<string>(DownloadTargetsList);
      if (tg.Contains(Path.GetFullPath(path))) return;

      if (string.IsNullOrEmpty(DownloadTargets)) { TargetOutputDirectory = path; DownloadTargets = path; }
      else tg.Add(path);

      tg.Sort();
      DownloadTargetsList = tg;
      Save();
    }
  }

}

