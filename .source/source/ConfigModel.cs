using System;
using System.Collections.Generic;
using System.IO;
using DH=System.DirectoryHelper;
namespace YouTubeDownloadUtil
{
  class ConfigModel
  {
    static readonly FileInfo confDotIni = new FileInfo(Path.Combine(DH.ExecutableDirectory,"config.ini"));
    
    public void Save()
    {
      var coll = new IniCollection(this);
      coll.Write(confDotIni);
    }
    
    static public ConfigModel Load()
    {
      var ini = new ConfigModel(){
        TargetOutputDirectory=Path.Combine(DH.ExecutableDirectory,"downloads"),
        DownloadTargets=Path.Combine(DH.ExecutableDirectory,"downloads"),
        PathFFmpeg=Path.Combine(DH.ExecutableDirectory,"bin"),
        PathYoutubeDL=Path.Combine(DH.ExecutableDirectory,"bin"),
      };
      if (!confDotIni.Exists) ini.Save();
      var coll = new IniCollection(confDotIni);
      coll.ToInstance(ini);
      return ini;
    }
    /// <summary>
    /// the active download-target directory
    /// </summary>
    [IniKey(Group="global", Default="downloads")]
    public string TargetOutputDirectory { get; set; }
    
    /// <summary>
    /// path containing FFmpeg.exe
    /// </summary>
    [IniKey(Group="global", Default="bin")]
    public string PathFFmpeg { get; set; }
    
    /// <summary>
    /// Path containing youtube-dl and atomicparsley.
    /// </summary>
    [IniKey(Group="global", Default="bin")]
    public string PathYoutubeDL { get; set; }
    
    /// <summary>
    /// This is a collection of strings separated by semi-colon.
    /// </summary>
    [IniKey(Group="global")]
    public string DownloadTargets { get; set; }
    
    [Ignore]
    public List<string> DownloadTargetsList {
      get {
        var l = new List<string>(DownloadTargets.Split(';'));
        for (int i = 0; i < l.Count; i++) l[i] = l[i].Trim();
        l.Sort();
        return l;
      }
      set { DownloadTargets = value.ToArray().JoinSemiColon(); }
    }
    
    public void AddDirectory(string path)
    {
      var tg = new List<string>(DownloadTargetsList);
      if (tg.Contains(Path.GetFullPath(path))) return;
      if (string.IsNullOrEmpty(DownloadTargets)) {
        TargetOutputDirectory = path;
        DownloadTargets = path;
      }
      else
      {
        tg.Add(path);
      }
      tg.Sort();
      DownloadTargetsList = tg;
      Save();
    }
  }
}


