using System.Collections.Generic;
using Eat2LoseWeight.DataAccess.Entities;

namespace Eat2LoseWeight.Models
{
    public interface IWeightChangeDistributionStrategy
    {
        WeightChangeDistribution Distribute(WeightSpan weightSpan, IList<ItemRecord> itemRecords);
    }
}
