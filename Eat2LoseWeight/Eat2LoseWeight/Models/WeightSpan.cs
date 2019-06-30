using System;
using Eat2LoseWeight.DataAccess.Entities;

namespace Eat2LoseWeight.Models
{
    public class WeightSpan
    {
        public WeightSpan(WeightRecord record1, WeightRecord record2)
        {
            if (record1.MeasuredAt < record2.MeasuredAt)
            {
                From = record1.MeasuredAt;
                To = record2.MeasuredAt;
                Change = record2.Value - record1.Value;
            }
            else
            {
                From = record2.MeasuredAt;
                To = record1.MeasuredAt;
                Change = record1.Value - record2.Value;
            }
        }

        public DateTime From { get; }

        public DateTime To { get; }

        public double Change { get; }
    }
}
