using System.Collections.Generic;
using UnityEngine;

public class RandomNumberGenerator
{
    public List<int> GenrateNumber(int LowerBound, int UpperBound, int NumberOfNumber)
    {
        List<int> randomInt = new List<int>(NumberOfNumber);

        while (randomInt.Count < NumberOfNumber)
        {
            var number = Random.Range(LowerBound, UpperBound);
            if (randomInt.Contains(number)) continue;
            randomInt.Add(number);
        }

        return randomInt;
    }
}
