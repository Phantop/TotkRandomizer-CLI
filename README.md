# TOTK Randomizer Mod
A port of [MelonSpeedruns's](https://github.com/MelonSpeedruns/)
[Tears of the Kingdom Randomizer](https://github.com/MelonSpeedruns/TotkRandomizer)
to a commandline utility that can run on Linux with no changes to the original
source.

To run:
```sh
cd src
./setup.sh
dotnet run -c Release <path/to/romfs>
```
* romfs path must not end in a `/`
