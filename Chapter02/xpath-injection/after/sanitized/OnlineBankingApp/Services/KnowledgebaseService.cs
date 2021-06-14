using System.Xml;
using System.Xml.XPath;
using Microsoft.AspNetCore.Hosting;

using System.Collections.Generic;
using OnlineBankingApp.Models;

using System;
using System.Linq;

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
            string sanitizedInput = Sanitize(input);

            List<Knowledge> searchResult = new List<Knowledge>(); 
            var webRoot = _env.WebRootPath;
            var file = System.IO.Path.Combine(webRoot, "Knowledgebase.xml");

            XmlDocument XmlDoc = new XmlDocument();
            XmlDoc.Load(file);
            
            XPathNavigator nav = XmlDoc.CreateNavigator();
            XPathExpression expr = nav.Compile(@"//knowledge[tags[contains(text(),'" + sanitizedInput + "')] and sensitivity/text()='Public']");

            var matchedNodes = nav.Select(expr);

            foreach (XPathNavigator node in matchedNodes)
            {                
                searchResult.Add(new Knowledge() {Topic = node.SelectSingleNode(nav.Compile("topic")).Value,Description = node.SelectSingleNode(nav.Compile("description")).Value});                
            }

            return searchResult;
        }

        private string Sanitize(string input)
        {
            if (string.IsNullOrEmpty(input)) {
                throw new ArgumentNullException("input", "input cannot be null");
            }
            HashSet<char> whitelist = new HashSet<char>(@"1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz ");
            return string.Concat(input.Where(i => whitelist.Contains(i))); ;
        }
    }

    public interface IKnowledgebaseService
    {
        List<Knowledge> Search(string input);
    }
}