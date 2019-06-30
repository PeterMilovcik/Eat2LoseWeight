using System;
using SQLite;

namespace Eat2LoseWeight.DataAccess.Entities
{
    public class ItemRecord
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int ItemId { get; set; }

        public DateTime At { get; set; }
    }
}
