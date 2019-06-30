using System.Collections;
using System.Collections.Generic;

namespace Eat2LoseWeight.Models
{
    public class WeightChangeDistribution : IEnumerable<KeyValuePair<int, List<double>>>
    {
        private IDictionary<int, List<double>> Dictionary { get; }

        public WeightChangeDistribution()
        {
            Dictionary = new Dictionary<int, List<double>>();
        }

        public void Add(int itemId, List<double> weightChanges)
        {
            if (Dictionary.ContainsKey(itemId))
            {
                Dictionary[itemId].AddRange(weightChanges);
            }
            else
            {
                Dictionary.Add(itemId, new List<double>(weightChanges));
            }
        }

        public void Add(WeightChangeDistribution distribution)
        {
            foreach (var pair in distribution.Dictionary)
            {
                Add(pair.Key, pair.Value);
            }
        }

        public IEnumerator<KeyValuePair<int, List<double>>> GetEnumerator() => Dictionary.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)Dictionary).GetEnumerator();
    }
}
