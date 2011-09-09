using System.Collections.Generic;
using Nancy.ViewEngines.Razor;

namespace Mixer.BackOffice.Infrastructure.Nancy
{
    public class RazorConfiguration : IRazorConfiguration
    {
        #region Methods
        public IEnumerable<string> GetAssemblyNames()
        {
            return new string[] { 
                "System.Xml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089",
                "Nancy.Hosting.Aspnet"
            };
        }
        public IEnumerable<string> GetDefaultNamespaces()
        {
            return new string[] { 
                "System.Xml"
            };
        }
        #endregion

        #region Properties
        public bool AutoIncludeModelNamespace
        {
            get { return false; }
        }
        #endregion
    }
}