@echo off
setlocal

cd "%~dp0"

For %%a in (
"XwaImcEditor\bin\Release\net48\*.dll"
"XwaImcEditor\bin\Release\net48\*.exe"
"XwaImcEditor\bin\Release\net48\*.config"
) do (
xcopy /s /d "%%~a" dist\
)

For %%a in (
"XwaImcPlayer\bin\Release\net48\*.dll"
"XwaImcPlayer\bin\Release\net48\*.exe"
"XwaImcPlayer\bin\Release\net48\*.config"
) do (
xcopy /s /d "%%~a" dist\
)
