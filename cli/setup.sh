#!/bin/sh
test ! -d ../../Cead && git clone https://github.com/TotkMods/Cead ../../Cead --depth 1 --recursive
test ! -d ../../cs-restbl && git clone https://github.com/EPD-Libraries/cs-restbl ../../cs-restbl --depth 1 --recursive

for i in ../../Cead ../../cs-restbl ../../cs-restbl/lib/Native.IO; do
    cmake --no-warn-unused-cli \
        -DCMAKE_EXPORT_COMPILE_COMMANDS:BOOL=TRUE \
        -DCMAKE_BUILD_TYPE:STRING=Release \
        -S "$i/native" \
        -B "$i/native/build/linux" \
        -G "Ninja" &&
        cmake --build "$i/native/build/linux" --config Release --target all -j
done

cp ../../cs-restbl/lib/Native.IO/native/build/linux/native_io.so ../../cs-restbl/native/build/linux/cs_restbl.so .
