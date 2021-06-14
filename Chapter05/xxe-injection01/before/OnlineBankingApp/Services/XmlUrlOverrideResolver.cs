using System;
using System.Xml;
using System.Collections.Generic;

namespace OnlineBankingApp.Services
{
    public class XmlUrlOverrideResolver : XmlUrlResolver
    {
        public Dictionary<string, string> DtdFileMap { 
            get; 
            private set;  
        }

        public XmlUrlOverrideResolver()
        {
            this.DtdFileMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        }

        public override Uri ResolveUri(Uri baseUri, string relativeUri)
        {
            string remappedLocation;
            if (DtdFileMap.TryGetValue(relativeUri, out remappedLocation))
                return new Uri(remappedLocation);
            var value = base.ResolveUri(baseUri, relativeUri);
            return value;
        }
    }
}