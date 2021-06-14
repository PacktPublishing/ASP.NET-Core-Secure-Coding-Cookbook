using System.Xml;
using System.Xml.XPath;
using Microsoft.AspNetCore.Hosting;

using System.Collections.Generic;
using OnlineBankingApp.Models;

using System.Xml.Xsl;
using System.Xml.Schema;
using System.IO;

namespace OnlineBankingApp.Services
{
    public class KnowledgebaseService : IKnowledgebaseService
    {

        private IWebHostEnvironment  _env;

        public KnowledgebaseService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public List<Knowledge> Search(string input)
        {
            List<Knowledge> searchResult = new List<Knowledge>(); 
            var webRoot = _env.WebRootPath;

            var schemaSet = new XmlSchemaSet();
            var xsdFile = System.IO.Path.Combine(webRoot, "Knowledgebase.xsd");
            using (System.IO.FileStream stream = File.OpenRead(xsdFile))
            {
                schemaSet.Add(XmlSchema.Read(stream, (s, e) =>
                {
                    var x = e.Message;
                }));
            }

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ValidationType = ValidationType.Schema;
            settings.Schemas = schemaSet;
            settings.DtdProcessing = DtdProcessing.Parse;

            var file = System.IO.Path.Combine(webRoot, "Knowledgebase.xml");

    		XmlReader reader = XmlReader.Create(file, settings);
            XmlDocument xmlDoc = new XmlDocument()
            {
                XmlResolver = null
            };
            xmlDoc.Load(reader);

            XPathNavigator nav = xmlDoc.CreateNavigator();
            XPathExpression expr = nav.Compile(@"//knowledge[tags[contains(text(),$input)] and sensitivity/text()='Public']");

            XsltArgumentList varList = new XsltArgumentList();
            varList.AddParam("input", string.Empty, input);

            CustomContext context = new CustomContext(new NameTable(), varList);
            expr.SetContext(context);

            var matchedNodes = nav.Select(expr);

            foreach (XPathNavigator node in matchedNodes)
            {                
                searchResult.Add(new Knowledge() {Topic = node.SelectSingleNode(nav.Compile("topic")).Value,Description = node.SelectSingleNode(nav.Compile("description")).Value});                
            }

            return searchResult;
        }
    }

    public interface IKnowledgebaseService
    {
        List<Knowledge> Search(string input);
    }
}