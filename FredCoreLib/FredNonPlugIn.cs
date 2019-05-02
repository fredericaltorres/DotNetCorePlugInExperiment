using System;

namespace FredCoreLib
{
    /// <summary>
    /// Just a class that does NOT implementof the interface IPlugIn
    /// </summary>
    public class FredNonPlugIn 
    {
        public bool Run(string param)
        {
            Console.WriteLine($"[plugIn:{this.GetType().FullName}] param:{param}");
            return true;
        }
    }
}
