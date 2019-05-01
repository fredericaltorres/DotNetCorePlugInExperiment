using PlugInInterface;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;

namespace PlugInManagerLib
{
    public class PlugInManager
    {
        private Assembly _assembly;
        private string _plugInFQN;
        private Type _plugInType;
        private IPlugIn _plugIn = null;

        public PlugInManager(string assemblyFileName, string plugInFQN)
        {
            LoadAssembly(assemblyFileName);
            this._plugInFQN = plugInFQN;
        }
        public PlugInManager(byte [] assemblyBinary, string plugInFQN)
        {
            LoadAssembly(assemblyBinary);
            this._plugInFQN = plugInFQN;
        }

        private void LoadAssembly(byte [] buffer)
        {
            MemoryStream stream = new MemoryStream(buffer);
            if(this._assembly == null)
                this._assembly = AssemblyLoadContext.Default.LoadFromStream(stream);
        }

        private void LoadAssembly(string assemblyFileName)
        {
            try
            {
                var assemblyBinary = File.ReadAllBytes(assemblyFileName);
                this.LoadAssembly(assemblyBinary);
            }
            catch (System.Exception ex)
            {
                throw new PlugInManagerException($"Cannot load assembly {assemblyFileName}", ex);
            }
        }

        public PlugInManager Load()
        {
            this.LoadType();
            this.CreateInstance();
            return this;
        }

        private void CreateInstance()
        {
            try
            {
                if(this._plugIn == null)
                    this._plugIn = Activator.CreateInstance(this._plugInType) as IPlugIn;
            }
            catch (System.Exception ex)
            {
                throw new PlugInManagerException($"Cannot create instance of class {this._plugInType}", ex);
            }
        }

        private void LoadType()
        {

            try
            {
                if(this._plugInType == null)
                    this._plugInType = this._assembly.GetType(this._plugInFQN);
            }
            catch (System.Exception ex)
            {
                throw new PlugInManagerException($"Type {this._plugInFQN} not found in assembly {this._assembly.FullName} ", ex);
            }
        }

        public IPlugIn Instance
        {
            get
            {
                if (this._plugIn == null)
                    throw new PlugInManagerException($"PlugIn not loaded");
                return this._plugIn;
            }
        }        
    }
}
