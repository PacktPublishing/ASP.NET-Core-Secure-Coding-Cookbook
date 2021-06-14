using System;
using System.Xml.XPath;

namespace OnlineBankingApp.Services
{
    // The interface that resolves and executes a specified user-defined function.
    public class XPathExtensionFunctions : System.Xml.Xsl.IXsltContextFunction
    {
        // The data types of the arguments passed to XPath extension function.
        private System.Xml.XPath.XPathResultType[] argTypes;
        // The minimum number of arguments that can be passed to function.
        private int minArgs;
        // The maximum number of arguments that can be passed to function.
        private int maxArgs;
        // The data type returned by extension function.
        private System.Xml.XPath.XPathResultType returnType;
        // The name of the extension function.
        private string FunctionName;

        // Constructor used in the ResolveFunction method of the custom XsltContext
        // class to return an instance of IXsltContextFunction at run time.
        public XPathExtensionFunctions(int minArgs, int maxArgs,
            XPathResultType returnType, XPathResultType[] argTypes, string functionName)
        {
            this.minArgs = minArgs;
            this.maxArgs = maxArgs;
            this.returnType = returnType;
            this.argTypes = argTypes;
            this.FunctionName = functionName;
        }

        // Readonly property methods to access private fields.
        public System.Xml.XPath.XPathResultType[] ArgTypes
        {
            get
            {
                return argTypes;
            }
        }
        public int Maxargs
        {
            get
            {
                return maxArgs;
            }
        }

        public int Minargs
        {
            get
            {
                return maxArgs;
            }
        }

        public System.Xml.XPath.XPathResultType ReturnType
        {
            get
            {
                return returnType;
            }
        }

        // XPath extension functions.

        private int CountChar(XPathNodeIterator node, char charToCount)
        {
            int charCount = 0;
            for (int charIdx = 0; charIdx < node.Current.Value.Length; charIdx++)
            {
                if (node.Current.Value[charIdx] ==  charToCount)
                {
                    charCount++;
                }
            }
            return charCount;
        }

        // This overload will not force the user
        // to cast to string in the xpath expression
        private string FindTaskBy(XPathNodeIterator node, string text)
        {
            if (node.Current.Value.Contains(text))
                return node.Current.Value;
            else
                return "";
        }

        private string Left(string str, int length)
        {
            return str.Substring(0, length);
        }

        private string Right(string str, int length)
        {
            return str.Substring((str.Length - length), length);
        }

        // Function to execute a specified user-defined XPath extension
        // function at run time.
        public object Invoke(System.Xml.Xsl.XsltContext xsltContext,
                    object[] args, System.Xml.XPath.XPathNavigator docContext)
        {
            if (FunctionName == "CountChar")
                return (Object)CountChar((XPathNodeIterator)args[0],
                                                Convert.ToChar(args[1]));
            if (FunctionName == "FindTaskBy")
                return FindTaskBy((XPathNodeIterator)args[0],
                                            Convert.ToString(args[1]));

            if (FunctionName == "Left")
                return (Object)Left(Convert.ToString(args[0]),
                                                Convert.ToInt16(args[1]));

            if (FunctionName == "Right")
                return (Object)Right(Convert.ToString(args[0]),
                                                Convert.ToInt16(args[1]));

            return null;
        }
    }
}