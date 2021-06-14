using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace OnlineBankingApp.Models
{
    [JsonObject]
    public class Id
    {
        [JsonProperty("$oid")]
        public string Oid { get; set; }
    }

    [JsonObject]
    public class Row
    {
        public Id _id { get; set; }
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("Address")]
        public string Address { get; set; }
        [JsonProperty("PhoneNo")]
        public string PhoneNo { get; set; }
        [JsonProperty("AccountNo")]
        public string AccountNo { get; set; }
        [JsonProperty("Description")]
        public string Description { get; set; }
    }

    [JsonObject]
    public class Query
    {
    }

    [JsonObject]
    public class Root
    {
        public int offset { get; set; }
        public List<Row> rows { get; set; }
        public int total_rows { get; set; }
        public Query query { get; set; }
        public int millis { get; set; }
    }

}