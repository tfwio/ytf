using System;
using System.Collections.Specialized;
using System.Linq;

namespace System
{
  public abstract class StringFilter
  {
    public abstract string Apply(string input);
  }
  /// <summary>
  /// Parses simple URI strings with parameters, suitable
  /// for parsing some simple youtube URLs.
  /// </summary>
  public class UrlInfo
  {
    internal const string cHttps = "https://";
    internal const string cHttp = "http://";
    internal const string cYoutube = "www.youtube.com";
    internal const string cYtShort = "youtu.be";

    /// <summary>same as input URI.</summary>
    public string BaseUri;

    public string UriLeftovers;
    /// <summary>e.g. "http://"</summary>
    public string UriProtocol { get; set; }
    /// <summary>e.g. [domain.com]/[path0]/[path1]</summary>
    public string UriPath { get; set; }
    /// <summary>e.g. "www.youtube.com"</summary>
    public string UriDomain { get; set; }
    /// <summary>
    /// (theoretically) everything in the string other than yielded by `Uri.GetLeftPart(url)`.
    /// </summary>
    public string UriParamString { get; set; }

    public NameValueCollection Params { get; set; }

    // init with null values.
    public UrlInfo() {
      BaseUri = null;
      UriLeftovers = null;
      UriProtocol = null;
      UriDomain = null;
      UriPath = null;
      UriParamString = null;
      Params = null;
    }

    public UrlInfo(string uriInput, params StringFilter[] filters) : base() { ParseUriString(uriInput, filters); }

    int ParseUriString(string uriInput, params StringFilter[] filters)
    {
      BaseUri = uriInput;
      // in case of incorrectly provided path separators, convert to '/'.
      // plus conversion of youtu.be URLs (of course)
      UriLeftovers = uriInput.Replace("\\", "/");
      
      for (int i = 0; i < filters.Length; i++)
        UriLeftovers = filters[i].Apply(UriLeftovers);

      UriProtocol = null;
      UriDomain = null;
      UriPath = null;
      UriParamString = null;

      // Protocol (strip protocol from mUri)
      // ==================================================

      if (UriLeftovers.Contains(cHttps))
      {
        UriProtocol = "https";
        UriLeftovers = UriLeftovers.Replace(cHttps, string.Empty);
      }
      else if (UriLeftovers.Contains(cHttp))
      {
        UriProtocol = "http";
        UriLeftovers = UriLeftovers.Replace(cHttp, string.Empty);
      }
      else UriProtocol = string.Empty;

      // Parameter String (strip params)
      // ==================================================

      int start = UriLeftovers.IndexOf("?");
      if (start != -1 && start <= UriLeftovers.Length)
      {
        var s1 = start + 1;
        //UriParams = mURI.Substring(start + 1);
        //UriParams = Uri.EscapeDataString(mURI.Substring(start + 1));
        UriParamString = Uri.EscapeUriString(UriLeftovers.Substring(start + 1));

        if (!string.IsNullOrEmpty(UriParamString))
        {
          UriParamString = UriParamString.TrimStart('?').Trim('/',' ');
          UriLeftovers = UriLeftovers.Replace($"?{UriParamString}", string.Empty);
        }
        Params = UriParamString.ParseParamString();
      }

      UriLeftovers = UriLeftovers.TrimStart('/');

      var mPath = UriLeftovers.Split('/').ToList();
      UriDomain = mPath[0];
      mPath.RemoveAt(0);

      UriPath = string.Join("/", mPath.ToArray()).Trim('/');

      UriLeftovers = UriLeftovers.Replace($"{UriDomain}/", string.Empty);
      UriLeftovers = UriLeftovers.Replace($"{UriPath}", string.Empty);
      UriLeftovers = UriLeftovers.Trim('/');

      return 0;
    }


    public string Rewrite(bool PathSeparateParams=false)
    {
      var Separator = PathSeparateParams ? "/" : "";
      return $"{UriProtocol}://{UriDomain}/{UriPath}{Separator}{Params.ToQueryString()}";
    }
  }
}




