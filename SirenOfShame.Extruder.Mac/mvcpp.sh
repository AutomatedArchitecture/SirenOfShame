cp ../MyCppLibrary/MyCppLib/Build/Products/Debug/libMyCppLib.dylib ./libMyCppLib.dylib
install_name_tool -change /usr/local/lib/libhidapi.0.dylib @loader_path/libhidapi.0.dylib ./libMyCppLib.dylib
