using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
namespace YouTubeDownloadUI
{
	class DownloadTarget
	{
		public string TargetUri { get; set; }

		public string TargetPath { get; set; } // directory
		public string TargetType { get; set; } // m4a, best, mp4, mp3, ogg
		public string SubLang { get; set; }
		
    public bool AbortOnDuplicate { get; set; }
		
		public bool AddMetaData { get; set; }
		public bool Continue { get; set; } // -c
		public bool EmbedSubs { get; set; } // --embed-subs
		public bool EmbedThumbnail { get; set; } 
		public bool GetPlaylist { get; set; } // --get-playlist
		public bool IgnoreErrors { get; set; } // -i
		public bool Verbose { get; set; } // --verbose
		public bool WriteAutoSub { get; set; }
		public bool WriteSub { get; set; }

		public string UriFiltered { get { return TargetUri.Replace("&", "^&"); } }
		public bool HasPlaylist { get { return this.TargetUri.Contains("&list"); } }
		public bool HasTargetType { get { return !string.IsNullOrEmpty(TargetType); } }
		
		static public readonly DownloadTarget Default = GenerateDefault;
		static public DownloadTarget GenerateDefault {
			get {
				return new DownloadTarget() {
          TargetType = "best",
					SubLang = "en",
          AbortOnDuplicate = true,
          AddMetaData = true,
          Continue = true,
          EmbedSubs = true,
          EmbedThumbnail = true,
          GetPlaylist = false,
					IgnoreErrors = true,
          Verbose = true,
          WriteAutoSub = true,
          WriteSub = true,
				};
			}
		}
	}
}




