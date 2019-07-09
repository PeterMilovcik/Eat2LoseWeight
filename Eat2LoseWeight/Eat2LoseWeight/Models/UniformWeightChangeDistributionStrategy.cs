using Eat2LoseWeight.DataAccess.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Eat2LoseWeight.Models
{
    public class UniformWeightChangeDistributionStrategy : IWeightChangeDistributionStrategy
    {
        public WeightChangeDistribution Distribute(WeightSpan weightSpan, IList<ItemRecord> itemRecords)
        {
            var result = new WeightChangeDistribution();
            if (!CanDistribute(weightSpan, itemRecords)) return result;
            var itemWeightChange = weightSpan.Change / itemRecords.Count;
            foreach (var itemRecord in itemRecords)
            {
                result.Add(itemRecord.ItemId, new List<double> { itemWeightChange });
            }
            return result;
        }

        private bool CanDistribute(WeightSpan weightSpan, IList<ItemRecord> itemRecords) =>
            weightSpan != null &&
            itemRecords != null &&
            itemRecords.Any();
    }
}