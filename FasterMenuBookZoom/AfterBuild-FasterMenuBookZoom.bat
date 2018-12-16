del *.zip
rmdir /S /Q FasterMenuBookZoom

mkdir FasterMenuBookZoom || goto :error
xcopy FasterMenuBookZoom.dll FasterMenuBookZoom || goto :error
xcopy ..\..\..\Info.json FasterMenuBookZoom || goto :error
"C:\Program Files\7-Zip\7z.exe" a FasterMenuBookZoom.zip FasterMenuBookZoom || goto :error

"C:\Program Files\7-Zip\7z.exe" a FasterMenuBookZoom-Source.zip ..\..\*.cs || goto :error

xcopy /Y FasterMenuBookZoom.dll "C:\Program Files (x86)\Steam\steamapps\common\Pathfinder Kingmaker\Mods\FasterMenuBookZoom" || goto :error

goto :EOF

:error
echo Failed with error #%errorlevel%.
exit /b %errorlevel%