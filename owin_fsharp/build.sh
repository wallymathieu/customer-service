#!/bin/bash
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
dotnet test Tests
exit_code=$?
if [ $exit_code -ne 0 ]; then
  exit $exit_code
fi
