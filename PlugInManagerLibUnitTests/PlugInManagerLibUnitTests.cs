using PlugInManagerLib;
using System;
using System.IO;
using Xunit;

namespace PlugInManagerLibUnitTests
{
    public class PlugInManagerLibUnitTests
    {
        string assemblyFileName { 
            get {
                return @".\FredCoreLib.dll";
            }        
        }

        [Fact]
        public void CannotLoadNonExistentAssemblyName()
        {
            Assert.Throws<PlugInManagerException>(() => new PlugInManager(@".\FredCoreLib.BAD.dll", null));
        }

        [Fact]
        public void CannotLoadInvalidPlugInTypeName()
        {
            var plugIn = new PlugInManager(assemblyFileName, "FredCoreLib.FredBADPlugIn");
            Assert.False(plugIn.Load());
        }

        [Fact]
        public void CannotInvokeComponentThatDoesNotImplementIPlugIn()
        {
            var plugIn = new PlugInManager(assemblyFileName, "FredCoreLib.FredNonPlugIn");
            plugIn.Load();
            Assert.False(plugIn.IsValidPlugIn);
        }

        [Fact]
        public void LoadAssemblyAsFileNameAndInvokePlugIn()
        {
            var plugIn = new PlugInManager(assemblyFileName, "FredCoreLib.FredPlugIn");
            Assert.True(plugIn.Load());
            Assert.True(plugIn.IsValidPlugIn);
            Assert.True(plugIn.Instance.Run("param"));
        }

        [Fact]
        public void LoadAssemblyAsByteArrayAndInvokePlugIn()
        {
            var plugIn = new PlugInManager(File.ReadAllBytes(assemblyFileName), "FredCoreLib.FredPlugIn");
            Assert.True(plugIn.Load());
            Assert.True(plugIn.IsValidPlugIn);
            Assert.True(plugIn.Instance.Run("param"));
        }
    }
}
