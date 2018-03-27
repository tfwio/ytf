using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
namespace YouTubeDownloadUI
{
	class MainSettings
	{
		public string TargetOutputDirectory {
			get;
			set;
		}

		public string PathFFmpeg {
			get;
			set;
		}

		public string PathYoutubeDL {
			get;
			set;
		}

		public List<string> DownloadTargets {
			get;
			set;
		}
	}
}


