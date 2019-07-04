using Eat2LoseWeight.DataAccess.Entities;
using System.Collections.Generic;

namespace Eat2LoseWeight.Models
{
    public interface IWeightChangeDistributionStrategy
    {
        WeightChangeDistribution Distribute(WeightSpan weightSpan, IList<ItemRecord> itemRecords);
    }
}
