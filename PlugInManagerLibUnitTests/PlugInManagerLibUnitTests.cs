using PlugInManagerLib;
using System;
using System.IO;
using Xunit;

namespace PlugInManagerLibUnitTests
{
    public class PlugInManagerLibUnitTests
    {
        private const string GoodPlugInFQN = "FredCoreLib.FredPlugIn";

        string assemblyFileName { 
            get {
                return @".\FredCoreLib.dll";
            }        
        }

        [Fact]
        public void CannotLoadNonExistentAssemblyName()
        {
            Assert.Throws<PlugInManagerException>(() => 
                new PlugInManager(@".\FredCoreLib.BAD.dll", null)
            );
        }

        [Fact]
        public void CannotLoadInvalidPlugInTypeName()
        {
            var plugIn = new PlugInManager(assemblyFileName, "FredCoreLib.FredBADPlugIn");
            Assert.False(plugIn.Load());
        }

        [Fact]
        public void Check_IsValidPlugIn_OnComponentThatDoesNotImplementIPlugIn()
        {
            var plugIn = new PlugInManager(assemblyFileName, "FredCoreLib.FredNonPlugIn");
            plugIn.Load();
            Assert.False(plugIn.IsValidPlugIn);
        }

        [Fact]
        public void CannotInvokeComponent_ThatDoesNotImplementIPlugIn()
        {
            var plugIn = new PlugInManager(assemblyFileName, "FredCoreLib.FredNonPlugIn");
            plugIn.Load();
            Assert.Throws<PlugInManagerException>(() =>
               Assert.True(plugIn.Instance.Run("param"))
           );
        }

        [Fact]
        public void LoadAssemblyAsFileName_And_InvokePlugIn()
        {
            var plugIn = new PlugInManager(assemblyFileName, GoodPlugInFQN);
            Assert.True(plugIn.Load());
            Assert.True(plugIn.IsValidPlugIn);
            Assert.True(plugIn.Instance.Run("param"));
        }

        [Fact]
        public void LoadAssemblyAsByteArray_And_InvokePlugIn()
        {
            var plugIn = new PlugInManager(File.ReadAllBytes(assemblyFileName), GoodPlugInFQN);
            Assert.True(plugIn.Load());
            Assert.True(plugIn.IsValidPlugIn);
            Assert.True(plugIn.Instance.Run("param"));
        }

        [Fact]
        public void LoadAssemblyAsFileName_InvokePlugIn_WithoutLoadingPlugInFirst_ShouldThrowException()
        {
            var plugIn = new PlugInManager(assemblyFileName, GoodPlugInFQN);
            Assert.Throws<PlugInManagerException>(() =>
                plugIn.Instance.Run("param")
            );
        }
    }
}
