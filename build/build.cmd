@echo off
".\..\packages\fake_build\FAKE.Core\tools\FAKE.exe" "%1"
exit /b %errorlevel%