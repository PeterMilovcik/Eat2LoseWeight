using System;
using SQLite;

namespace Eat2LoseWeight.DataAccess.Entities
{
    public class WeightRecord
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public double Value { get; set; }

        public DateTime MeasuredAt { get; set; }
    }
}
