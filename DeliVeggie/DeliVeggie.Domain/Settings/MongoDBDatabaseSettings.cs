using System;
using System.Collections.Generic;
using System.Text;

namespace DeliVeggie.Domain.Settings
{
    public class MongoDBDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
