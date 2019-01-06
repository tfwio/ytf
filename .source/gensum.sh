#! /usr/bin/sh

# usage: ./gensum.sh /path/to/directory/with/zip-files
# see: https://stackoverflow.com/questions/3679296/only-get-hash-value-using-md5sum-without-filename#5773761

md5_32=($(md5sum       ${1}/ytdl_util-*-win32.zip))
sha1_32=($(sha1sum     ${1}/ytdl_util-*-win32.zip))
sha256_32=($(sha256sum ${1}/ytdl_util-*-win32.zip))

md5_64=($(md5sum       ${1}/ytdl_util-*-win64.zip))
sha1_64=($(sha1sum     ${1}/ytdl_util-*-win64.zip))
sha256_64=($(sha256sum ${1}/ytdl_util-*-win64.zip))

echo "
32 BIT

md5    : \`${md5_32}\`  
sha1   : \`${sha1_32}\`  
sha256 : \`${sha256_32}\`

64 BIT

md5    : \`${md5_64}\`  
sha1   : \`${sha1_64}\`  
sha256 : \`${sha256_64}\`
"

echo "
32 BIT

md5    : \`${md5_32}\`  
sha1   : \`${sha1_32}\`  
sha256 : \`${sha256_32}\`

64 BIT

md5    : \`${md5_64}\`  
sha1   : \`${sha1_64}\`  
sha256 : \`${sha256_64}\`
" > sha_sums.txt
