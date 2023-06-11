@echo off

@REM Build the project for x86_64 Windows
cargo build --release --target=x86_64-pc-windows-msvc

@REM Check if the Plugins folder exists and create it if it doesn't
if not exist "..\\..\\Assets\\Plugins\\x86_64" mkdir "..\\..\\Assets\\Plugins\\x86_64"

@REM Copy files to the Plugins folder
copy "target\\x86_64-pc-windows-msvc\\release\\file_browser.dll" "..\\..\\Assets\\Plugins\\x86_64\\file_browser.dll"