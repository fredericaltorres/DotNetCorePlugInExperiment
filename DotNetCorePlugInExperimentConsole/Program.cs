using PlugInInterface;
using PlugInManagerLib;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;

namespace DotNetCoreConsoleTestAutoInstaller
{
   
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("START!");

            var assemblyFileName = @"C:\DVT\.NET\DotNetCorePlugInExperiment\FredCoreLib\bin\Debug\netcoreapp2.1\FredCoreLib.dll";
            var myPlugIn = new PlugInManager(assemblyFileName, "FredCoreLib.FredPlugIn").Load();
            Console.WriteLine($"Toto:{myPlugIn.Instance.Run("myParam1")}");

            byte[] assemblyBinary = File.ReadAllBytes(assemblyFileName);
            var myPlugIn2 = new PlugInManager(assemblyBinary, "FredCoreLib.FredPlugIn").Load();
            Console.WriteLine($"Toto:{myPlugIn2.Instance.Run("myParam1")}");

            Console.ReadKey();
        }
    }
}
