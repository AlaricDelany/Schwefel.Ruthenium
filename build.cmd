@echo off
".\packages\fake_build\FAKE.Core\tools\FAKE.exe" "build\build.fsx" %*
exit /b %errorlevel%