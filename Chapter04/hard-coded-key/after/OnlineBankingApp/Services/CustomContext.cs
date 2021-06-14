using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace OnlineBankingApp.Services
{
    public class CustomContext : XsltContext
    {
        private const string ExtensionsNamespaceUri = "http://xpathExtensions";
        // XsltArgumentList to store names and values of user-defined variables.
        private XsltArgumentList argList;

        public CustomContext()
        {
        }

        public CustomContext(NameTable nt, XsltArgumentList args)
            : base(nt)
        {
            argList = args;
        }

        // Function to resolve references to user-defined XPath extension
        // functions in XPath query expressions evaluated by using an
        // instance of this class as the XsltContext.
        public override System.Xml.Xsl.IXsltContextFunction ResolveFunction(
                                    string prefix, string name,
                                    System.Xml.XPath.XPathResultType[] argTypes)
        {
            // Verify namespace of function.
            if (this.LookupNamespace(prefix) == ExtensionsNamespaceUri)
            {
                string strCase = name;

                switch (strCase)
                {
                    case "CountChar":
                        return new XPathExtensionFunctions(2, 2, XPathResultType.Number,
                                                                    argTypes, "CountChar");

                    case "FindTaskBy": // This function is implemented but not called.
                        return new XPathExtensionFunctions(2, 2, XPathResultType.String,
                                                                    argTypes, "FindTaskBy");

                    case "Right": // This function is implemented but not called.
                        return new XPathExtensionFunctions(2, 2, XPathResultType.String,
                                                                        argTypes, "Right");

                    case "Left": // This function is implemented but not called.
                        return new XPathExtensionFunctions(2, 2, XPathResultType.String,
                                                                        argTypes, "Left");
                }
            }
            // Return null if none of the functions match name.
            return null;
        }

        // Function to resolve references to user-defined XPath
        // extension variables in XPath query.
        public override System.Xml.Xsl.IXsltContextVariable ResolveVariable(
                                                        string prefix, string name)
        {
            if (this.LookupNamespace(prefix) == ExtensionsNamespaceUri || !prefix.Equals(string.Empty))
            {
                throw new XPathException(string.Format("Variable '{0}:{1}' is not defined.", prefix, name));
            }

            // Verify name of function is defined.
            // if (name.Equals("text") || name.Equals("charToCount") ||
            //     name.Equals("right") || name.Equals("left"))
            // {
                // Create an instance of an XPathExtensionVariable
                // (custom IXsltContextVariable implementation) object
                //  by supplying the name of the user-defined variable to resolve.
                XPathExtensionVariable var;
                var = new XPathExtensionVariable(prefix, name);

                // The Evaluate method of the returned object will be used at run time
                // to resolve the user-defined variable that is referenced in the XPath
                // query expression.
                return var;
            //}
            //return null;
        }

        // Empty implementation, returns false.
        public override bool PreserveWhitespace(System.Xml.XPath.XPathNavigator node)
        {
            return false;
        }

        // empty implementation, returns 0.
        public override int CompareDocument(string baseUri, string nextbaseUri)
        {
            return 0;
        }

        public override bool Whitespace
        {
            get
            {
                return true;
            }
        }

        // The XsltArgumentList property is accessed by the Evaluate method of the
        // XPathExtensionVariable object that the ResolveVariable method returns. It is used
        // to resolve references to user-defined variables in XPath query expressions.
        public XsltArgumentList ArgList
        {
            get
            {
                return argList;
            }
        }
    }
}