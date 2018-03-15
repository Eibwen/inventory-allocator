@echo off

REM the dotnet commandline tool is fairly new, so using this to keep track of the commands I use

set slnName=InventoryAllocator

REM install NUnit3 template
dotnet new -i NUnit3.DotNetNew.Template

dotnet new sln -n %slnName%
dotnet new classlib -n %slnName% -o %slnName%
dotnet new nunit -n %slnName%Tests -o %slnName%Tests
