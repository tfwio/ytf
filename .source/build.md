[paket]:            https://fsprojects.github.io/Paket/getting-started.html
[patch]:            .patch/20180904-e0a2d1702aae5a4f267cc60d847c2c64c1a5b0bc-html-renderer-changes-used.diff

A simple (prototypical) youtube-dl wrapper in winforms.

Build (Windows)
================

get the sources

```bash
git clone https://github.com/tfwio/youtube-dl-winforms
```

*Target framework is net35-cli by default but you can re-target if you need to.*

- Load it up in some IDE and build it
- Or find the version of msbuild.exe (PathTowardMsBuild) on your system so you can...
  ```cmd
  set PATH=%PATH%;[msbuild-path]
  msbuild .solution/youtube-dl-winforms.sln /t:YouTubeDownloadUtil "/p:Configuration=Release;Platform=Any CPU"
  ```

### Develop

get [paket] by going into the `.paket` directory and launching the boot-strapper

Once `./.paket/paket.exe` exists, from a command-shell...

```bash
cd .source
../.paket/paket install
```

Once you run paket (install), we have to patch RColor.cs from HTML-Renderer.  
see: [.patch/20180904-e0a2d1702aae5a4f267cc60d847c2c64c1a5b0bc-html-renderer-changes-used.diff][patch]  
*or you can just do a git-revert on the file conveniently using tortoise-git*.

