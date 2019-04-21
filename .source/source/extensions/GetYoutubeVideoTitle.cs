using System;

namespace YouTubeDownloadUtil
{
  // fmt_list
  // enablecsi
  // tmi
  // watermark
  // relative_loudness
  // timestamp: when GET_VIDEO_INFO was called.
  // enabled_engage_types
  // hl
  // title
  // ssl
  // fexp
  // gapi_hint_params
  // itct
  // cr
  // external_play_video
  // author
  // adaptive_fmts
  // account_playback_token
  // status
  // cver
  // loudness
  // video_id
  // apiary_host
  // vss_host
  // csi_page_type
  // innertube_api_version
  // root_ve_type
  // idpj
  // player_response
  // ismb
  // thumbnail_url
  // length_seconds
  // fflags
  // player_error_log_fraction
  // csn
  // innertube_api_key
  // xhr_apiary_host
  // c
  // token
  // host_language
  // no_get_video_log
  // t
  // url_encoded_fmt_stream_map
  // ucid
  // innertube_context_client_version
  // ldpj
  // apiary_host_firstparty
  static class GetYoutubeVideoTitle
  {
    static public string GetInfoUrl(string url)
    {
      var mUrl = new UrlInfo(url);
      var targetVid = mUrl.Params["v"];
      if (targetVid == null) return null;
      return $"{mUrl.UriProtocol}://youtube.com/get_video_info?video_id={targetVid}";
    }

    static public string GetYoutubeDataString(this string url)
    {
      var mUri = GetInfoUrl(url);
      using (var cli = new System.Net.WebClient())
      {
        var data = cli.DownloadString(mUri);
        return data;
      }
    }
    static public System.Collections.Specialized.NameValueCollection GetYoutubeDataParams(this string url)
    {
      return url.GetYoutubeDataString().ParseParamString();
    }

  }
}




