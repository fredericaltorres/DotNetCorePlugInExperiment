using PlugInInterface;
using System;
using System.Linq;
using System.Collections.Generic;
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
            // Is it possible to unload an Assembly loaded dynamically in dotnet core?
            // https://stackoverflow.com/questions/43106917/is-it-possible-to-unload-an-assembly-loaded-dynamically-in-dotnet-core

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

        public bool Load()
        {
            try
            {
                this.LoadType();                
                return true;
            }
            catch
            {

            }
            return false;
        }

        public List<string> GetTypes()
        {
            var l = new List<string>();
            foreach (TypeInfo type in this._assembly.DefinedTypes)
            {
                l.Add(type.FullName);
            }
            return l;
        }

        public bool IsValidPlugIn
        {
            get
            {
                // https://stackoverflow.com/questions/4963160/how-to-determine-if-a-type-implements-an-interface-with-c-sharp-reflection
    
                return typeof(IPlugIn).IsAssignableFrom(this._plugInType);
            }
        }

        private void CreateInstance()
        {
            if(!this.IsValidPlugIn)
                throw new PlugInManagerException($"Class {this._plugInFQN} does not implement the IPlugIn interface");

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
            if(this._plugInType == null)
            {
                this._plugInType = this._assembly.GetType(this._plugInFQN);
                if (this._plugInType == null)
                    throw new PlugInManagerException($"Type {this._plugInFQN} not found in assembly {this._assembly.FullName} ");
            }
        }

        public IPlugIn Instance
        {
            get
            {
                this.CreateInstance();
                if (this._plugIn == null)
                    throw new PlugInManagerException($"PlugIn not loaded");
                return this._plugIn;
            }
        }        
    }
}
