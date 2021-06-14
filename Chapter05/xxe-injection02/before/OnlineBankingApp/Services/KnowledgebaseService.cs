using System.Xml;
using System.Xml.XPath;
using Microsoft.AspNetCore.Hosting;

using System.Collections.Generic;
using OnlineBankingApp.Models;

using System.Xml.Xsl;

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
            var file = System.IO.Path.Combine(webRoot, "Knowledgebase.xml");

            XmlTextReader xmlReader = new XmlTextReader(file);
            XPathDocument xmlDoc = new XPathDocument(xmlReader);
            
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