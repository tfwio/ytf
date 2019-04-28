#! /usr/bin/sh

# usage: ./gensum.sh /path/to/directory/with/zip-files

md5_32_z=($(md5sum       ${1}/ytdl_util-*-win32.zip))
sha1_32_z=($(sha1sum     ${1}/ytdl_util-*-win32.zip))
sha256_32_z=($(sha256sum ${1}/ytdl_util-*-win32.zip))

md5_64_z=($(md5sum       ${1}/ytdl_util-*-win64.zip))
sha1_64_z=($(sha1sum     ${1}/ytdl_util-*-win64.zip))
sha256_64_z=($(sha256sum ${1}/ytdl_util-*-win64.zip))

md5_32_7z=($(md5sum       ${1}/ytdl_util-*-win32.7z))
sha1_32_7z=($(sha1sum     ${1}/ytdl_util-*-win32.7z))
sha256_32_7z=($(sha256sum ${1}/ytdl_util-*-win32.7z))

md5_64_7z=($(md5sum       ${1}/ytdl_util-*-win64.7z))
sha1_64_7z=($(sha1sum     ${1}/ytdl_util-*-win64.7z))
sha256_64_7z=($(sha256sum ${1}/ytdl_util-*-win64.7z))

echo "
**$(echo ytdl_util-*-win32.zip)**

md5    : \`${md5_32_z}\`  
sha1   : \`${sha1_32_z}\`  
sha256 : \`${sha256_32_z}\`

**$(echo ytdl_util-*-win64.zip)**

md5    : \`${md5_64_z}\`  
sha1   : \`${sha1_64_z}\`  
sha256 : \`${sha256_64_z}\`

**$(echo ytdl_util-*-win32.7z)**

md5    : \`${md5_32_7z}\`  
sha1   : \`${sha1_32_7z}\`  
sha256 : \`${sha256_32_7z}\`

**$(echo ytdl_util-*-win64.7z)**

md5    : \`${md5_64_7z}\`  
sha1   : \`${sha1_64_7z}\`  
sha256 : \`${sha256_64_7z}\`
" > sha_sums.txt


echo "
**$(echo ytdl_util-*-win32.zip)**

md5    : \`${md5_32_z}\`  
sha1   : \`${sha1_32_z}\`  
sha256 : \`${sha256_32_z}\`

**$(echo ytdl_util-*-win64.zip)**

md5    : \`${md5_64_z}\`  
sha1   : \`${sha1_64_z}\`  
sha256 : \`${sha256_64_z}\`

**$(echo ytdl_util-*-win32.7z)**

md5    : \`${md5_32_7z}\`  
sha1   : \`${sha1_32_7z}\`  
sha256 : \`${sha256_32_7z}\`

**$(echo ytdl_util-*-win64.7z)**

md5    : \`${md5_64_7z}\`  
sha1   : \`${sha1_64_7z}\`  
sha256 : \`${sha256_64_7z}\`
"