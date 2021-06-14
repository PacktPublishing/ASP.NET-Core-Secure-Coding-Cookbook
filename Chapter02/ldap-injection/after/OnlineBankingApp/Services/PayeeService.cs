using OnlineBankingApp.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace OnlineBankingApp.Services
{
    public class PayeeService : IPayeeService
    {
        private readonly IMongoCollection<Payee> payees;

        public PayeeService(IOnlineBankDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            payees = database.GetCollection<Payee>(settings.PayeesCollectionName);
        }

        public List<Payee> Get() =>
            payees.Find(payee => true).ToList();

        public List<Payee> Get(string name) {            
            return payees.Find(payee => payee.Name == name).ToList();
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