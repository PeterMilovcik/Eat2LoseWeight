using Eat2LoseWeight.DataAccess.Entities;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eat2LoseWeight.DataAccess
{
    public class Database
    {
        private SQLiteAsyncConnection Connection { get; }

        public Database(string dbPath)
        {
            Connection = new SQLiteAsyncConnection(dbPath);
            Connection.CreateTableAsync<WeightRecord>().Wait();
            Connection.CreateTableAsync<Item>().Wait();
            Connection.CreateTableAsync<ItemRecord>().Wait();
        }

        public Task<List<WeightRecord>> GetWeightRecordsAsync() =>
            Connection.Table<WeightRecord>().ToListAsync();

        public Task<int> InsertWeightRecordAsync(WeightRecord weightRecord) =>
            Connection.InsertAsync(weightRecord);

        public Task<int> UpdateWeightRecordAsync(WeightRecord weightRecord) =>
            Connection.UpdateAsync(weightRecord);

        public Task<List<Item>> GetItemsAsync() =>
            Connection.Table<Item>().ToListAsync();

        public Task<int> SaveItemAsync(Item item) =>
            Connection.InsertAsync(item);

        public Task<List<ItemRecord>> GetItemRecordsAsync() =>
            Connection.Table<ItemRecord>().ToListAsync();

        public Task<int> SaveItemRecordAsync(ItemRecord itemRecord) =>
            Connection.InsertAsync(itemRecord);

        public Task<int> RemoveWeightAsync(WeightRecord weightRecord) =>
            Connection.DeleteAsync(weightRecord);
    }
}
