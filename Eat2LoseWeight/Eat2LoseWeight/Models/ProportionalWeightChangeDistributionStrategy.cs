using System;
using System.Collections.Generic;
using System.Linq;
using Eat2LoseWeight.DataAccess.Entities;

namespace Eat2LoseWeight.Models
{
    public class ProportionalWeightChangeDistributionStrategy : IWeightChangeDistributionStrategy
    {
        public WeightChangeDistribution Distribute(WeightSpan weightSpan, IList<ItemRecord> itemRecords)
        {
            var result = new WeightChangeDistribution();
            if (!CanDistribute(weightSpan, itemRecords)) return result;
            var itemGroups = itemRecords.GroupBy(ir => ir.ItemId).ToList();
            var groupWeightChange = weightSpan.Change / itemGroups.Count;
            itemGroups.ForEach(items =>
            {
                var itemWeightChange = groupWeightChange / items.Count();
                var weightChanges = Enumerable.Repeat(itemWeightChange, items.Count()).ToList();
                result.Add(items.Key, weightChanges);
            });
            return result;
        }

        private bool CanDistribute(WeightSpan weightSpan, IList<ItemRecord> itemRecords) =>
            weightSpan != null &&
            Math.Abs(weightSpan.Change) > double.Epsilon &&
            itemRecords != null &&
            itemRecords.Any();
    }
}
