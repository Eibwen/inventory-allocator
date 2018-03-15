@echo off

REM the dotnet commandline tool is fairly new, so using this to keep track of the commands I use

set slnName=InventoryAllocator

REM install NUnit3 template
dotnet new -i NUnit3.DotNetNew.Template

dotnet new sln -n %slnName%
dotnet new classlib -n %slnName% -o %slnName%
dotnet new nunit -n %slnName%Tests -o %slnName%Tests

dotnet sln add %slnName%/%slnName%.csproj
dotnet sln add %slnName%Tests/%slnName%Tests.csproj

REM does not work:  dotnet add .\%slnName%\%slnName%.csproj reference .\%slnName%Tests\%slnName%Tests.csproj
cd %slnName%Tests
dotnet add reference ..\%slnName%/%slnName%.csproj
