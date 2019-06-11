using SQLite;
using System;

namespace Eat2LoseWeight
{
    public class WeightRecord
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public double Value { get; set; }

        public DateTime MeasuredAt { get; set; }
    }
}
