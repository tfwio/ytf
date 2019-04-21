using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace System
{

  static class UriConverterExtensions
  {
    internal static string ConvertYoutubeUri(this string mUri)
    {
      // https://www.youtube.com/watch?v=[hash]
      return mUri.Contains(UrlInfo.cYtShort)
        ? mUri.Replace($"{UrlInfo.cYtShort}/", $"{UrlInfo.cYoutube}/watch/?v=")
        : mUri;
    }
    // https://stackoverflow.com/questions/27442985/alternative-to-httputility-parsequerystring-without-system-web-dependency
    /// <summary>NameValueCollection to a query-tring.</summary>
    internal static string ToQueryString(this NameValueCollection nvc)
    {
      IEnumerable<string> segments =
        from key in nvc.AllKeys
        from value in nvc.GetValues(key)
        select string.Format("{0}={1}", Uri.EscapeUriString(key), Uri.EscapeUriString(value));

      return "?" + string.Join("&", segments.ToArray());
    }

    /// <summary>
    /// Convert input to NameValueCollection.
    /// 
    /// Note that if you attempt to load a key that isn't present yields a null value.
    /// </summary>
    internal static NameValueCollection ParseParamString(this string paramString)
    {
      var nvc = string.IsNullOrEmpty(paramString) ? null : new NameValueCollection();
      if (nvc == null) return nvc;

      var feed = paramString.Split('&');
      for (int i = 0; i < feed.Length; i++)
      {
        var node = feed[i].Trim(' ');
        var eIndex = node.IndexOf('=');
        if (eIndex > -1)
        {
          var split = node.Split('=');
          var key = node.Substring(0, eIndex);
          var val = eIndex != node.Length;
          nvc.Set(key, SubKeyValue(node));
        }
      }
      return nvc;
    }
    static string SubKeyValue(string input, char symbol = '=', string nullValue = "")
    {
      var idx = input.IndexOf(symbol);
      if (idx >= input.Length) return nullValue; // no value found.
      return input.Substring(idx + 1);
    }
  }
}




