using System.Xml.XPath;
using System.Xml.Xsl;

namespace OnlineBankingApp.Services
{
    // The interface used to resolve references to user-defined variables
    // in XPath query expressions at run time. An instance of this class
    // is returned by the overridden ResolveVariable function of the
    // custom XsltContext class.
    public class XPathExtensionVariable : IXsltContextVariable
    {
        // Namespace of user-defined variable.
        private string prefix;
        // The name of the user-defined variable.
        private string varName;

        // Constructor used in the overridden ResolveVariable function of custom XsltContext.
        public XPathExtensionVariable(string prefix, string varName)
        {
            this.prefix = prefix;
            this.varName = varName;
        }

        // Function to return the value of the specified user-defined variable.
        // The GetParam method of the XsltArgumentList property of the active
        // XsltContext object returns value assigned to the specified variable.
        public object Evaluate(System.Xml.Xsl.XsltContext xsltContext)
        {
            XsltArgumentList vars = ((CustomContext)xsltContext).ArgList;
            return vars.GetParam(varName, prefix);
        }

        // Determines whether this variable is a local XSLT variable.
        // Needed only when using a style sheet.
        public bool IsLocal
        {
            get
            {
                return false;
            }
        }

        // Determines whether this parameter is an XSLT parameter.
        // Needed only when using a style sheet.
        public bool IsParam
        {
            get
            {
                return false;
            }
        }

        public System.Xml.XPath.XPathResultType VariableType
        {
            get
            {
                return XPathResultType.Any;
            }
        }
    }
}