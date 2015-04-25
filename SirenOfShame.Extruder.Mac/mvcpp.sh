cp ~/Library/Developer/Xcode/DerivedData/MyCppLib-flhmxkpxnclvragizxzwypqrqwij/Build/Products/Debug/libMyCppLib.dylib ~/SirenOfShame/SirenOfShame.Extruder.Mac/libMyCppLib.dylib
install_name_tool -change /usr/local/lib/libhidapi.0.dylib @loader_path/libhidapi.0.dylib ~/SirenOfShame/SirenOfShame.Extruder.Mac/libMyCppLib.dylib
