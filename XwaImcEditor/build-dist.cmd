@echo off
setlocal

cd "%~dp0"

For %%a in (
"XwaImcEditor\bin\Release\*.dll"
"XwaImcEditor\bin\Release\*.exe"
) do (
xcopy /s /d "%%~a" dist\
)

For %%a in (
"XwaImcPlayer\bin\Release\*.dll"
"XwaImcPlayer\bin\Release\*.exe"
) do (
xcopy /s /d "%%~a" dist\
)
