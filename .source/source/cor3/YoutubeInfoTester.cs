using System;

namespace YouTubeDownloadUtil
{
  class YoutubeInfoTester
  {
    static void PrintUriInfo(string uriTarget)
    {
      var uriInput = new UrlInfo(uriTarget);
      Console.WriteLine($"----------------------------------------------");
      Console.WriteLine($"INPUT: {uriInput.BaseUri}");
      Console.WriteLine($"  rewrite: {uriInput.Rewrite()}");
      Console.WriteLine($"  remains: {uriInput.UriLeftovers}");
      Console.WriteLine($"  params:  {uriInput.UriParamString}");
      Console.WriteLine($"  path:    {uriInput.UriPath}");
      Console.WriteLine("  Params List:");
      if (uriInput != null)
      {
        //foreach (string p in uriInput.Params)
        //  Console.WriteLine($"    param:   {p}={uriInput.Params[p]}");

        for (int i = 0; i < uriInput.Params.Count; i++)
        {
          var k = uriInput.Params.Keys[i];
          Console.WriteLine($"    param{i}: {k}={uriInput.Params[k]}");

        }
      }
    }

    static public void Test()
    {
      PrintUriInfo("https://www.youtube.com/watch?v=dKuvCu0KG5g");
      PrintUriInfo("https://youtu.be/dKuvCu0KG5g");

      var data = new UrlInfo("https://www.youtube.com/watch?v=FO7spLjad3k")
        .Rewrite()
        .GetYoutubeDataParams();
      foreach (var k in "t,title,author,length_seconds,timestamp,thumbnail_url,cver,fexp,player_response".Split(','))
      {
        string value = Uri.UnescapeDataString(data[k]).Replace('+', ' ');
        switch (k)
        {
          case "length_seconds":
            int s = int.Parse(Uri.UnescapeDataString(data[k]).Replace('+', ' '));
            var ts = TimeSpan.FromSeconds(s);
            value = $"{ts.Minutes:0#}:{ts.Seconds:0#} (translated)";
            if (ts.Hours > 0) value = $"{ts.Hours:0#}:{value}";
            break;
          case "player_response":
            // this gets some JSON data.
            value = Uri.UnescapeDataString(data[k]).Replace('+', ' ');
            break;
          default:
            value = Uri.UnescapeDataString(data[k]).Replace('+', ' ');
            break;
            //case "timestamp":
            //  int t = int.Parse(value);
            //  value = new DateTime(t).ToString();
            //  break;
        }
        Console.WriteLine($"  -{k,-9}: {value ?? "(null)"}");
      }
    }

    //Console.WriteLine($"  -{k,-9}: {value ?? "(null)"}");

    // Console.WriteLine("keys:");
    // for (int i = 0; i < data.Count; i++)
    // {
    //   string key = data.Keys[i];
    //   Console.WriteLine($"  {key}");
    // }
  }
}
