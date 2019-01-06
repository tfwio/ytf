using System;
using System.Collections.Generic;
using YouTubeDownloadUtil;
namespace System
{
	/// <summary>
	/// A string enclosure for translating the likes of environment variables
	/// or a dictionary of transforms.
	/// 
	/// 
	/// By default, we are using a simple "${RootString}" construct to translate
	/// the application's root (directory).
	/// 
	/// ### How It Works
	/// 
	/// The default `StringValue` value is stored as is, and when requested
	/// we can provide a rooted or unrooted version of the string via the
	/// Getter Property.
	/// 
	/// ### Implicit Operator to and from String
	/// 
	/// When translating to and from a string, we always provide the Rooted value
	/// to StringValue.
	/// </summary>
	public class RootString
	{
		static Dictionary<string,string> Root = new Dictionary<string,string>{
			{"${RootPath}", MainForm.AppRootPath},
			{"$(RootPath)", MainForm.AppRootPath},
		};

		#region String Functions

		/// <summary>Acts on Rooted.</summary>
		public string Replace(string needle, string replacement) { return Decoded.Replace(needle, replacement); }
		/// <summary>Acts on Rooted.</summary>
		public bool Contains(string needle) { return Decoded.Contains(needle); }
		#endregion

		// public bool TreatAsFullPath = true;
		string StringValue = null;

		public RootString() : this(null) {}
		public RootString(string input) { StringValue = input; }

		public RootString Normalize()
		{
			StringValue = this;
			return this;
		}

		/// <summary>
		/// Replaces ${RootPath} with actual path value.
		/// </summary>
		public string Decoded { get { return Decode(StringValue); } }
		
		/// <summary>
		/// replace Actual value with ${RootPath}.
		/// </summary>
		public string Encoded { get { return Decode(StringValue); } }

		static public implicit operator string(RootString input) { return input.Decoded; }
		static public implicit operator RootString(string input) { return new RootString(input); }
		
		static public string Encode(string input)
		{
			if (input==null) return null;
			if (string.IsNullOrEmpty(input)) return input;
			string result = input;
			foreach (var pair in Root) result = result.Replace(pair.Value, pair.Key);
			return result;
		}
		static public string Decode(string input)
		{
			if (input==null) return null;
			if (string.IsNullOrEmpty(input)) return input;
			string result = input;
			
			foreach (var pair in Root)
			{
				result = result.Replace(pair.Key.ToUpper(), pair.Value);
				result = result.Replace(pair.Key.ToLower(), pair.Value);
				result = result.Replace(pair.Key, pair.Value);
			}
			return result;
		}
		
	}
	static class Extensions
	{
		internal static bool DirectoryExistsAndNonempty(this RootString path) { return path.Decoded.DirectoryExistsAndNonempty(); }
		internal static string Encode(this string input) { return RootString.Encode(input); }
		internal static string Decode(this string input) { return RootString.Decode(input); }
	}
}