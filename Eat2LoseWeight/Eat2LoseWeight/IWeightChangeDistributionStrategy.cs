using System.Collections.Generic;

namespace Eat2LoseWeight
{
    public interface IWeightChangeDistributionStrategy
    {
        WeightChangeDistribution Distribute(WeightSpan weightSpan, IList<ItemRecord> itemRecords);
    }
}
