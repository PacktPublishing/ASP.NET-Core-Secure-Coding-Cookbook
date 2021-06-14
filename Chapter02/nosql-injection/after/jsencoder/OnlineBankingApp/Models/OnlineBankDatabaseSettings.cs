namespace OnlineBankingApp.Models
{
    public class OnlineBankDatabaseSettings : IOnlineBankDatabaseSettings
    {
        public string PayeesCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IOnlineBankDatabaseSettings
    {
        string PayeesCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}