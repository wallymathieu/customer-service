#!/bin/bash
dotnet tool restore
exit_code=$?
if [ $exit_code -ne 0 ]; then
  exit $exit_code
fi
dotnet tool run paket restore
exit_code=$?
if [ $exit_code -ne 0 ]; then
  exit $exit_code
fi
dotnet restore
exit_code=$?
if [ $exit_code -ne 0 ]; then
  exit $exit_code
fi
dotnet build
exit_code=$?
if [ $exit_code -ne 0 ]; then
  exit $exit_code
fi 
dotnet test
exit_code=$?
if [ $exit_code -ne 0 ]; then
  exit $exit_code
fi
