using System;

namespace FredCoreLib
{
    public class FredNonPlugIn 
    {
        public bool Run(string param)
        {
            Console.WriteLine($"[plugIn:{this.GetType().FullName}] param:{param}");
            return true;
        }
    }
}
