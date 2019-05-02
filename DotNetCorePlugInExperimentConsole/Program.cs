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
            Console.WriteLine("DotNetCorePlugInExperimentConsole");

            // Load assembly from the file system
            var assemblyFileName = @"C:\DVT\.NET\DotNetCorePlugInExperiment\FredCoreLib\bin\Debug\netcoreapp2.1\FredCoreLib.dll";

            var myPlugIn = new PlugInManager(assemblyFileName, "FredCoreLib.FredPlugIn");
            if (!myPlugIn.Load()) throw new ApplicationException();

            var valid = myPlugIn.IsValidPlugIn;
            foreach (var typeFound in myPlugIn.GetTypes())
                Console.WriteLine($"Type Found :{typeFound}");

            Console.WriteLine($"Execute PlugIn Instance :{myPlugIn.Instance.Run("myParam1")}");

            // Load assembly as binary
            byte[] assemblyBinary = File.ReadAllBytes(assemblyFileName);
            var myPlugIn2 = new PlugInManager(assemblyBinary, "FredCoreLib.FredPlugIn");
            if (!myPlugIn2.Load()) throw new ApplicationException();
            Console.WriteLine($"Execute PlugIn Instance :{myPlugIn2.Instance.Run("myParam1")}");

            Console.ReadKey();
        }
    }
}
