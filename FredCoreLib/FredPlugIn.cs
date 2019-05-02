using System;

namespace FredCoreLib
{
    /// <summary>
    /// A full implementation of a IPlugIn interface
    /// </summary>
    public class FredPlugIn : PlugInInterface.IPlugIn
    {
        public bool Run(string param)
        {
            Console.WriteLine($"[plugIn:{this.GetType().FullName}] param:{param}");
            return true;
        }
    }
}
