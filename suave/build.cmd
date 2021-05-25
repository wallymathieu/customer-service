@echo off
dotnet tool restore 
if errorlevel 1 (
  exit /b %errorlevel%
)
dotnet tool run paket restore 
if errorlevel 1 (
  exit /b %errorlevel%
)
dotnet restore 
if errorlevel 1 (
  exit /b %errorlevel%
)
dotnet build 
if errorlevel 1 (
  exit /b %errorlevel%
)
dotnet test
if errorlevel 1 (
  exit /b %errorlevel%
)
