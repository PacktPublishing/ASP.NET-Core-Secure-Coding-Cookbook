using System.Xml;
using System.Xml.XPath;
using Microsoft.AspNetCore.Hosting;

using System.Collections.Generic;
using OnlineBankingApp.Models;

using System.Linq;
using System.Xml.Linq;

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

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.DtdProcessing = DtdProcessing.Prohibit;
            settings.MaxCharactersFromEntities = 1024;
            settings.MaxCharactersInDocument = 2048;

            using(XmlReader reader = XmlReader.Create(file, settings)){
                try {
                    XDocument xmlDoc = XDocument.Load(reader);        

                    var query = from i in xmlDoc.Element("knowledgebase")
                            .Elements("knowledge")
                            where
                            (i.Element("topic").ToString().Contains(input) == true ||
                            i.Element("description").ToString().Contains(input) == true) &&
                            i.Element("sensitivity").ToString().Contains("Public") == true
                            select new
                            {
                                Topic = (string)i.Element("topic"),
                                Description = (string)i.Element("description")
                            };

                    foreach (var knowledge in query)
                    {                
                        searchResult.Add(new Knowledge() {Topic = knowledge.Topic ,Description = knowledge.Description });                
                    }

                    return searchResult;
                }
                catch (XmlException ex) {
                    throw new XmlException(ex.Message);
                }
            }
        }
    }

    public interface IKnowledgebaseService
    {
        List<Knowledge> Search(string input);
    }
}