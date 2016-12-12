using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeReviewJob
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> Sample<T>(this IEnumerable<T> population, int sampleSize)
        {
            if (population == null)
            {
                throw new ArgumentNullException(nameof(population));
            }   

            var localCopy = population.ToList();
            if (localCopy.Count < sampleSize)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(sampleSize),
                    "sample size should larger than population.");
            }
            
            var random = new Random();
            for (var populated = 0; populated < sampleSize; populated++)
            {
                var i = random.Next(0, localCopy.Count);
                yield return localCopy[i];
                localCopy.RemoveAt(i);
            }
        }
    }
}
