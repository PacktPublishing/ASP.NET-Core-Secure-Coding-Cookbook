using OnlineBankingApp.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;

namespace OnlineBankingApp.Services
{
    public class PayeeService : IPayeeService
    {
        private readonly IMongoCollection<Payee> payees;
        private readonly JavaScriptEncoder _jsEncoder;

        public PayeeService(IOnlineBankDatabaseSettings settings,JavaScriptEncoder jsEncoder)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            payees = database.GetCollection<Payee>(settings.PayeesCollectionName);
            _jsEncoder = jsEncoder;
        }

        public List<Payee> Get() =>
            payees.Find(payee => true).ToList();

        public List<Payee> Get(string name) {
            
            var filter = "{$where: \"function() {return this.Name == '" + _jsEncoder.Encode(name) + "'}\"}";
            return payees.Find(filter).ToList();
        }

        public Payee Create(Payee payee)
        {
            payees.InsertOne(payee);
            return payee;
        }

        public void Update(string id, Payee payeeIn) =>
            payees.ReplaceOne(payee => payee.Id == id, payeeIn);

        public void Remove(Payee payeeIn) =>
            payees.DeleteOne(payee => payee.Id == payeeIn.Id);

        public void Remove(string id) => 
            payees.DeleteOne(payee => payee.Id == id);
    }

    public interface IPayeeService
    {
        List<Payee> Get();
        List<Payee> Get(string name);
        Payee Create(Payee payee);
        void Update(string id, Payee payeeIn);
        void Remove(Payee payeeIn);
        void Remove(string id);
    }
}