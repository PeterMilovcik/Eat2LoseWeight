using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eat2LoseWeight
{
    public class Database
    {
        private SQLiteAsyncConnection Connection { get; }

        public Database(string dbPath)
        {
            Connection = new SQLiteAsyncConnection(dbPath);
            Connection.CreateTableAsync<WeightRecord>().Wait();
            Connection.CreateTableAsync<Item>().Wait();
        }

        public Task<List<WeightRecord>> GetWeightRecordsAsync() =>
            Connection.Table<WeightRecord>().ToListAsync();

        public Task<int> SaveWeightRecordAsync(WeightRecord weightRecord) =>
            Connection.InsertAsync(weightRecord);

        public Task<List<Item>> GetItemsAsync() =>
            Connection.Table<Item>().ToListAsync();

        public Task<int> SaveItemAsync(Item item) =>
            Connection.InsertAsync(item);
    }
}
