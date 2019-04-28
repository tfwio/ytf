using System;
using System.IO;
using System.Windows.Forms;

namespace YouTubeDownloadUtil
{
  internal sealed class Program
  {
    [STAThread]
    private static void Main(string[] args)
    {
      // Console.WriteLine("=====================================================");
      // YoutubeInfoTester.Test();
      // var testParams = other_file.ParseParamString();
      // Console.WriteLine("=====================================================");
      // foreach (var k in testParams) Console.WriteLine($"  {k}");
      // Console.WriteLine("=====================================================");
      // return;

      if (!File.Exists("config.ini")){
        using (var stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("YouTubeDownloadUtil.config.ini"))
          using (var reader = new StreamReader(stream))
        {
          var content = reader.ReadToEnd();
          File.WriteAllText(Path.Combine(System.DirectoryHelper.ExecutableDirectory, ResourceStrings.ConfDefault), content);
        }
      }
      
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new MainForm());
    }
  }
}
