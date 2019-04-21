namespace System.Windows.Forms
{
  class CommandKeyHandler<TParent> : CommandKeyHandler
  {
    public new Action<TParent> Action { get; set; }
  }
  class CommandKeyHandler
  {
    public string Name { get; set; }
    public Keys Keys { get; set; }
    public Action Action { get; set; }
  }
}

