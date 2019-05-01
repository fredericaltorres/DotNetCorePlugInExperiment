//using FredCoreLib;
//using System;
//using System.IO;
//using System.Reflection;
//using System.Runtime.Loader;

//namespace DotNetCoreConsoleTestAutoInstaller
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            AppDomain.CurrentDomain.AssemblyResolve += __AssemblyResolve;
//            AssemblyLoadContext.Default.Resolving += __DefaultResolving;
//            Console.WriteLine("START!");

//            var fred = new FredCoreClass();
//            Console.WriteLine($"Toto:{fred.GetToto()}");
//            Console.ReadKey();
//        }

//        private static Assembly __DefaultResolving(AssemblyLoadContext arg1, AssemblyName arg2)
//        {
//            Console.WriteLine($"Default_Resolving:{arg2.FullName}");
//            return null;
//        }

//        private static System.Reflection.Assembly __AssemblyResolve(object sender, ResolveEventArgs args)
//        { 
//            var assemblyName = args.Name.Split(',')[0];
//            var assemblyFileName = Path.Combine("bu", assemblyName + ".dll");
//            Console.WriteLine($"__AssemblyResolve:{assemblyName}");
//            var assembly = Assembly.LoadFrom(assemblyFileName);
//            return assembly;
//        }
//    }
//}
