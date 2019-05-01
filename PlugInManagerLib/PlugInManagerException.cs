namespace PlugInManagerLib
{
    public class PlugInManagerException : System.Exception
    {
        public PlugInManagerException(string message, System.Exception innerException) : base(message, innerException)
        {

        }
        public PlugInManagerException(string message) : base(message)
        {

        }
    }
}
