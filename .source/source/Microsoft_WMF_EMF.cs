using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

namespace YouTubeDownloadUtil
{
  // https://stackoverflow.com/questions/5270763/convert-an-image-into-wmf-with-net
  // https://social.msdn.microsoft.com/Forums/windowsdesktop/en-US/0fa8d9e6-d33d-4eca-8652-5ce16040a62c/emf-to-wmf-conversion?forum=windowssdk
  // https://stackoverflow.com/questions/623104/byte-to-hex-string#10048895
  static class Microsoft_WMF_EMF
  {
    [Flags]
    private enum EmfToWmfBitsFlags
    {
      EmfToWmfBitsFlagsDefault = 0x00000000,
      EmfToWmfBitsFlagsEmbedEmf = 0x00000001,
      EmfToWmfBitsFlagsIncludePlaceable = 0x00000002,
      EmfToWmfBitsFlagsNoXORClip = 0x00000004
    }

    //const int MM_ISOTROPIC = 7;
    const int MM_ANISOTROPIC = 8;

    [DllImport("gdiplus")] static extern uint GdipEmfToWmfBits(IntPtr _hEmf, uint _bufferSize, byte[] _buffer, int _mappingMode, EmfToWmfBitsFlags _flags);
    [DllImport("gdi32")] static extern IntPtr SetMetaFileBitsEx(uint _bufferSize, byte[] _buffer);
    [DllImport("gdi32")] static extern IntPtr CopyMetaFile(IntPtr hWmf, string filename);
    [DllImport("gdi32")] static extern bool DeleteMetaFile(IntPtr hWmf);
    [DllImport("gdi32")] static extern bool DeleteEnhMetaFile(IntPtr hEmf);
    
    /// Try not to use too big of images plz.
    /// Streams are usually incrementally handled or buffered.
    /// This is an entire stream dump.
    /// Then again, we do hold the entire image in memory, don't we.
    static public byte[] GetMetaFileBytes(this Bitmap image)
    {
      Metafile metafile = null;
      
      using (Graphics g = Graphics.FromImage(image))
      {
        IntPtr hDC = g.GetHdc();
        metafile = new Metafile(hDC, EmfType.EmfOnly);
        g.ReleaseHdc(hDC);
      }

      using (Graphics g = Graphics.FromImage(metafile)) g.DrawImage(image, 0, 0);

      IntPtr _hEmf = metafile.GetHenhmetafile();

      uint _bufferSize = GdipEmfToWmfBits(_hEmf, 0, null, MM_ANISOTROPIC, 0);

      byte[] _buffer = new byte[_bufferSize];
      GdipEmfToWmfBits(_hEmf, _bufferSize, _buffer, MM_ANISOTROPIC, 0);
      DeleteEnhMetaFile(_hEmf);
      return _buffer;
    }

    /// Try not to use too big of images plz.
    /// Streams are usually incrementally handled or buffered.
    /// This is an entire stream dump.
    /// Then again, we do hold the entire image in memory, don't we.
    static public MemoryStream MakeMetafileStream(this Bitmap image)
    {
      Metafile metafile = null;
      using (Graphics g = Graphics.FromImage(image))
      {
        IntPtr hDC = g.GetHdc();
        metafile = new Metafile(hDC, EmfType.EmfOnly);
        g.ReleaseHdc(hDC);
      }

      using (Graphics g = Graphics.FromImage(metafile)) g.DrawImage(image, 0, 0);

      IntPtr _hEmf = metafile.GetHenhmetafile();

      uint _bufferSize = GdipEmfToWmfBits(_hEmf, 0, null, MM_ANISOTROPIC, 0);

      byte[] _buffer = new byte[_bufferSize];
      GdipEmfToWmfBits(_hEmf, _bufferSize, _buffer, MM_ANISOTROPIC, 0);

      DeleteEnhMetaFile(_hEmf);

      var stream = new MemoryStream();
      stream.Write(_buffer, 0, (int)_bufferSize);
      stream.Seek(0, 0);

      return stream;
    }
#if ORIGINAL_POST // to file
    static MemoryStream MakeMetafileStream_OP(Bitmap image)
    {
      Metafile metafile = null;
      using (Graphics g = Graphics.FromImage(image))
      {
        IntPtr hDC = g.GetHdc();
        metafile = new Metafile(hDC, EmfType.EmfOnly);
        g.ReleaseHdc(hDC);
      }

      using (Graphics g = Graphics.FromImage(metafile)) g.DrawImage(image, 0, 0);

      IntPtr _hEmf = metafile.GetHenhmetafile();

      uint _bufferSize = GdipEmfToWmfBits(_hEmf, 0, null, MM_ANISOTROPIC, EmfToWmfBitsFlags.EmfToWmfBitsFlagsDefault);

      byte[] _buffer = new byte[_bufferSize];

      GdipEmfToWmfBits(_hEmf, _bufferSize, _buffer, MM_ANISOTROPIC, EmfToWmfBitsFlags.EmfToWmfBitsFlagsDefault);

      IntPtr hmf = SetMetaFileBitsEx(_bufferSize, _buffer);

      string tempfile = Path.GetTempFileName();
      CopyMetaFile(hmf, tempfile);
      DeleteMetaFile(hmf);
      DeleteEnhMetaFile(_hEmf);

      var stream = new MemoryStream();
      byte[] data = File.ReadAllBytes(tempfile);
      //File.Delete (tempfile);
      int count = data.Length;
      stream.Write(data, 0, count);
      return stream;
    }
#endif
    static string ToHex(this byte[] bytes)
    {
      char[] c = new char[bytes.Length * 2];

      byte b;

      for (int bx = 0, cx = 0; bx < bytes.Length; ++bx, ++cx)
      {
        b = ((byte)(bytes[bx] >> 4));
        c[cx] = (char)(b > 9 ? b - 10 + 'A' : b + '0');

        b = ((byte)(bytes[bx] & 0x0F));
        c[++cx] = (char)(b > 9 ? b - 10 + 'A' : b + '0');
      }

      return new string(c);
    }
  }
}




