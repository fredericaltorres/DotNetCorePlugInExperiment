# DotNetCorePlugInExperiment

## Overview

Experimentation 
- Loading code on the fly from miscellaneous sources with DotNetCore 2.x
- Execute plug in 

## Plug In Interface

- Implement plugin interface 

```cs
interface IPlugIn
{
    bool Run(string param);
}
```

## Loading

Allow to load the assembly from the file system or from a byte array

```cs
// Load assembly from the file system
var assemblyFileName = @"C:\DVT\.NET\DotNetCorePlugInExperiment\FredCoreLib\bin\Debug\netcoreapp2.1\FredCoreLib.dll";
var myPlugIn = new PlugInManager(assemblyFileName, "FredCoreLib.FredPlugIn").Load();
myPlugIn.Load();
Console.WriteLine($"Execute PlugIn Instance :{myPlugIn.Instance.Run("myParam1")}");

// Load assembly as binary
byte[] assemblyBinary = File.ReadAllBytes(assemblyFileName);
var myPlugIn2 = new PlugInManager(assemblyBinary, "FredCoreLib.FredPlugIn").Load();
myPlugIn.Load();
Console.WriteLine($"Execute PlugIn Instance :{myPlugIn2.Instance.Run("myParam1")}");
```