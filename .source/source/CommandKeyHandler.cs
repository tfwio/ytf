using System;
namespace YouTubeDownloadUtil
{
  class CommandKeyHandler<TParent>
  {
    public string Name { get; set; }
    public System.Windows.Forms.Keys Keys { get; set; }
    public Action<TParent> Action { get; set; }
  }
  class CommandKeyHandler
  {
    public string Name { get; set; }
    public System.Windows.Forms.Keys Keys { get; set; }
    public Action Action { get; set; }
  }
}

