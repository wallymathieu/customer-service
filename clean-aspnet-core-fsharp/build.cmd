@echo off
dotnet restore 
if errorlevel 1 (
  exit /b %errorlevel%
)
dotnet build 
if errorlevel 1 (
  exit /b %errorlevel%
)
dotnet test Tests
if errorlevel 1 (
  exit /b %errorlevel%
)
