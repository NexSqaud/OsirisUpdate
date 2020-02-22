# Osiris Updater [![C#](https://img.shields.io/badge/language-C%23-brightgreen.svg)](https://en.wikipedia.org/wiki/C_Sharp_(programming_language)) [![Windows](https://img.shields.io/badge/platform-Windows-0078d7.svg)](https://en.wikipedia.org/wiki/Microsoft_Windows) [![License](https://img.shields.io/github/license/NexSqaud/OsirisUpdate.svg)](LICENSE)

Program for auto update [Osiris](https://github.com/danielkrupinski/Osiris). Writed on C# WinForms.
Requie [Microsoft Visual Studio 2019](https://visualstudio.microsoft.com/) and [Inflame Injector](https://github.com/danielkrupinski/Inflame).

# How to use
* First run
    * Install Visual Studio 2019
    * Compile Inflame
    * Run program
    * Click **Init Downloader**
    * Select folder were osiris will be located
    * Wait for initilaizing
    * Click **Build**
    * Select MSBuild.exe (default located at: C:/Program Files(x86)/Microsoft Visual Studio/2019/Community/MSBuild/Current/Bin/MSBuild.exe)
    * Wait for building
    * Run game
    * Click **Inject**
    * Select Inflame.exe

The program saves the state and restores the next time you start it.

* On the next run, with update:
    * Click **Update**
    * Wait for repo updated
    * Click **Build**
    * Wait for building
    * Run game
    * Click **Inject**
* Without update:
    * Run game
    * Click **Inject**

For setting injector click **Inject Setting**

# How to build

* Download files from repository
* Open OsirisUpdater.sln in Visual Studio
* Select Release configuration
* Click Build -> Build Solution (or Ctrl+Shift+B)
* Wait for compiling
* Now OsirisUpdater.exe stored in OsirisUpdater/bin/Release/OsirisUpdater.exe 

**Make sure you installed .NET 4.7.2 SDK and .NET 4.6.1 SDK or Targeting Packs** 

# Program files struct:
* OsirisUpdater.exe - main executable file
* LibGit2Sharp.dll - git library
* Newtonsoft.Json.dll - json library

If you find grammatical errors, please report this issue.
