namespace YouTubeDownloadUtil
{
  class DownloadTarget
	{
	  public YoutubeDlFlags Flags { get; set; }
	  
		public string TargetUri { get; set; }

		public string KnownTargetFile { get; set; }
		public string TargetPath { get; set; } // directory
		public string TargetType { get; set; } // m4a, best, mp4, mp3, ogg
		public string SubLang { get; set; }
		
    public string AbortMessage { get; set; }
		
		public bool HasPlaylist { get { return TargetUri.Contains("&list=") || TargetUri.Contains("?list="); } } // only youtube.
		public bool HasTargetType { get { return !string.IsNullOrEmpty(TargetType); } }
		
		static public readonly DownloadTarget Default = GenerateDefault;
		static public DownloadTarget GenerateDefault {
			get {
				return new DownloadTarget() {
		      Flags = ConfigModel.Instance.AppFlags,
          TargetType = "best",
					SubLang = "en",
				};
			}
		}
	}
}




