#!/bin/sh
cd "$(dirname "$0")" || exit 1
git submodule update --recursive
for i in ../lib/Cead ../lib/cs-restbl ../lib/cs-restbl/lib/Native.IO; do
    cmake --no-warn-unused-cli \
        -DCMAKE_EXPORT_COMPILE_COMMANDS:BOOL=TRUE \
        -DCMAKE_BUILD_TYPE:STRING=Release \
        -S "$i/native" \
        -B "$i/native/build/linux" \
        -G "Ninja" &&
        cmake --build "$i/native/build/linux" --config Release --target all -j
done

cp ../lib/cs-restbl/lib/Native.IO/native/build/linux/native_io.so ../lib/cs-restbl/native/build/linux/cs_restbl.so .
