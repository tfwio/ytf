
A simple (prototypical) youtube-dl wrapper in winforms.

paket: https://fsprojects.github.io/Paket/getting-started.html

### Build (Windows)

get the sources

```bash
git clone https://github.com/tfwio/youtube-dl-winforms
```

*Target framework is net35-cli but you can re-target
if you need to.**

- Load it up in some and build it
- Find msbuild on your system
  ```cmd
  set PATH=%PATH%;PathToMsBuild/Bin
  msbuild .solution/youtube-dl-winforms.sln /t:YouTubeDownloadUtil "/p:Configuration=Release;Platform=Any CPU"
  ```

### Delveop

get [paket] by going into the `.paket` directory and launching the boot-strapper

Once `./.paket/paket.exe` exists, from a command-shell...

```bash
cd .source
../.paket/paket install
```

