using SQLite;
using System;

namespace Eat2LoseWeight
{
    public class ItemRecord
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int ItemId { get; set; }

        public DateTime At { get; set; }
    }
}
