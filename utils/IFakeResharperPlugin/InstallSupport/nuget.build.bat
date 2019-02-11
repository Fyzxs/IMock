REM Called from AfterBuild Step within csproj
@ECHO === === === === === === === ===
SET EnableNuGetPackageRestore=true
SET NUGET=..\..\..\InstallSupport\NuGet.exe
SET VER=%1

REM Extract Wave Version from Package. JetBrains.Platform.Sdk.*.*.*.*
SET VV=DIR /B ..\..\..\Packages\JetBrains.Platform.Sdk.*
for /f "usebackq tokens=4 delims=." %%a in (`%VV%`) do SET WAVEID=%%a
@ECHO Resharper Platform WAVE Id=%WAVEID%

@ECHO ===NUGET Publishing Version %VER%
%NUGET% pack -Symbols -Version %VER% IFakeResharperPlugin.nuspec -properties WAVEID=%WAVEID%
@ECHO === === === === === === === ===