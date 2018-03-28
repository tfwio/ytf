﻿using System;
using System.Collections.Generic;
using System.IO;
namespace YouTubeDownloadUtil
{
  class ConfigModel
  {
    static readonly FileInfo confDotIni = new FileInfo(Path.Combine(System.DirectoryHelper.ExecutableDirectory,"config.ini"));
    
    static internal ConfigModel Instance = Load();
    
    static public readonly string OriginalPath = System.Environment.GetEnvironmentVariable("PATH");
    
    /// <summary>the active download-target directory</summary>
    [IniKey(Group="global", Alias="target")] public string TargetOutputDirectory { get; set; }
    
    /// <summary>This is a collection of strings separated by semi-colon.</summary>
    [IniKey(Group="global", Alias="target-list")] public string DownloadTargets { get; set; }
    
    /// <summary>path containing FFmpeg.exe</summary>
    [IniKey(Group="global", Alias="FFmpeg_bin")] public string PathFFmpeg { get; set; }
    
    /// <summary>Path containing youtube-dl and atomicparsley.</summary>
    [IniKey(Group="global", Alias="youtube-dl_bin")] public string PathYoutubeDL { get; set; }
    
    public event EventHandler Saved;
    protected virtual void OnSaved() { var handler = Saved; if (handler != null) handler(this, EventArgs.Empty); }
    
    public void Save() { var coll = new IniCollection(this); coll.Write(confDotIni); OnSaved(); }
    
    static public ConfigModel Load() { return Load(DirectoryHelper.ExecutableDirectory); }
    static public ConfigModel Load(string confDir)
    {
      var ini = new ConfigModel(){
        TargetOutputDirectory=Path.Combine(confDir,"%USERPROFILE%\\Downloads"),
        DownloadTargets=Path.Combine(confDir,"%USERPROFILE%\\Downloads"),
        // not likely.
        PathFFmpeg=Path.Combine(confDir,"bin"), PathYoutubeDL=Path.Combine(confDir,"bin"),
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


