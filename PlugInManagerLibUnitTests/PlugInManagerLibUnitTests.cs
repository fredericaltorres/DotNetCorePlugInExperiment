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
        
        public void CannotLoadWrongAssemblyName()
        {
            Assert.Throws<PlugInManagerException>(() => new PlugInManager(@".\FredCoreLib.BAD.dll", null));
        }

        [Fact]
        public void CannotLoadBadPlugInTypeName()
        {
            var plugIn = new PlugInManager(assemblyFileName, "FredCoreLib.FredBADPlugIn");
            Assert.False(plugIn.Load());
        }

        [Fact]
        public void CannotInvokeComponentThatIsNotAnIPlugIn()
        {
            var plugIn = new PlugInManager(assemblyFileName, "FredCoreLib.FredNonPlugIn");
            // TODO: Test pass but plugin type is not  loaded yet
            Assert.False(plugIn.IsValidPlugIn);
        }

        [Fact]
        public void LoadAssemblyAsFileNameAndInvokePlugIn()
        {
            var plugIn = new PlugInManager(assemblyFileName, "FredCoreLib.FredPlugIn");
            Assert.True(plugIn.Load());
            Assert.True(plugIn.Instance.Run("param"));
        }

        [Fact]
        public void LoadAssemblyAsByteArrayAndInvokePlugIn()
        {
            var plugIn = new PlugInManager(File.ReadAllBytes(assemblyFileName), "FredCoreLib.FredPlugIn");
            Assert.True(plugIn.Load());
            Assert.True(plugIn.Instance.Run("param"));
        }
    }
}
