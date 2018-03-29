using System;
namespace YouTubeDownloadUtil
{
	class CommandKeyHandler
	{
	  public string Name { get; set; }
		public System.Windows.Forms.Keys Keys { get; set; }
		public Action Action { get; set; }
	}
}




