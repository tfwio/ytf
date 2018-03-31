---
title: youtube-dl ui/utility
subtitle: Helper for running youtube-dl
...

![](logo.png)

FIRST STEPS
=========

As indispensable as youtube-dl may be, it still needs a bit of help dealing with the variety of AV file-formats it encounters.

i.   Get youtube-dl (from the link below) and extract it somewhere.
ii.  DragDrop youtube-dl.exe into this app to tell it where it is.
iii. Get AVConv and do the same.
iv.  Get FFmpeg and do the same.
v.   Go get AtomicParsley and do the same.

LINKS
=========

youtube-dl uses a small handful of tools to help when it comes to adding meta-data (title, album, cover-art, etc...) to the media it downloads while also many audio formats from the wild can use some fine-tuning every now and again in which case FFmpeg and/or (libAV) will become quite useful.

seven-zip
--------------

There are quite a few versions of 7z that you can install if you haven't already.

https://www.7-zip.org/download.html

the version (2018.03.26.1) above was current when this program was authored.

youtube-dl
--------------

https://github.com/rg3/youtube-dl/releases  
youtube-dl.exe

the version (2018.03.26.1) above was current when this program was authored.

libav / avconv
--------------

For whatever reason(s) libav, is the recommended youtube-dl helper that

http://builds.libav.org/windows/release-lgpl/

It may seem daunting when you see the many available builds for libav, though simple enough to just choose the most recent release version.

http://builds.libav.org/windows/nightly-lgpl/

Alternatively, you can download a nightly version, as above just find the most recent one on the page.

FFmpeg
--------------

https://ffmpeg.zeranoe.com/builds/

The version zeranoe.com selects automatically should be the best one for you.
e.g. the latest version, (auto-selected) 32 or 64 bit, static.
Just click the blue "Download Build" button on their site ;)

AtomicParsley
--------------

https://sourceforge.net/projects/atomicparsley/files/atomicparsley/AtomicParsley%20v0.9.0/

https://sourceforge.net/projects/atomicparsley/files/atomicparsley/AtomicParsley%20v0.9.0/AtomicParsley-win32-0.9.0.zip/download

AtomicParsley-win32-0.9.0.zip — is what we're interested in.
for itunes audio (m4a) and video (mp4).


Quick-List
==================

```
:: get the latest youtube-dl
https://yt-dl.org/latest/youtube-dl.exe

:: 7z release
https://www.7-zip.org/a/7z1801-extra.7z
:: 7z beta (if you prefer to get the newest 7z)
https://www.7-zip.org/a/7z1803-extra.7z

:: (we'll probably just use this guy)
:: 2010 zipped command-line version
https://www.7-zip.org/a/7za920.zip

:: libav / aconv
http://builds.libav.org/windows/nightly-lgpl/libav-x86_64-w64-mingw32-20180108.7z

https://ffmpeg.zeranoe.com/builds/win32/static/ffmpeg-latest-win32-static.zip
https://ffmpeg.zeranoe.com/builds/win64/static/ffmpeg-latest-win64-static.zip
```
<!-- 
      //http://downloads.sourceforge.net/gnuwin32/wget-1.11.4-1-bin.zip
      //https://sourceforge.net/projects/atomicparsley/files/atomicparsley/AtomicParsley%20v0.9.0/AtomicParsley-win32-0.9.0.zip
      //https://phoenixnap.dl.sourceforge.net/project/atomicparsley/atomicparsley/AtomicParsley%20v0.9.0/AtomicParsley-win32-0.9.0.zip

servers: cytranet, phoenixnap      

//  https://cytranet.dl.sourceforge.net/project/atomicparsley/atomicparsley/AtomicParsley%20v0.9.0/AtomicParsley-win32-0.9.0.zip
//https://phoenixnap.dl.sourceforge.net/project/gnuwin32/wget/1.11.4-1/wget-1.11.4-1-bin.zip

 -->