using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
namespace YouTubeDownloadUI
{
	static class DirectoryHelper
	{
		/// <summary>
		/// Create directory if not exist relative to the calling application.
		/// </summary>
		/// <param name="dirName">directory-name</param>
		/// <returns>A full path to the target Directory.</returns>
		static public string EnsureLocalDirectory(string dirName)
		{
			var dirTarget = Path.Combine(Program.ExecutableDirectory, dirName);
			if (!Directory.Exists(dirTarget))
				Directory.CreateDirectory(dirTarget);
			return dirTarget;
		}
	}
}




