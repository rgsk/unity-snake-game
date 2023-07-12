using System;
using System.Collections.Generic;

public class RandomNumberGenerator {
    public static Tuple<float, float> RandomPairBetween(float minX, float maxX, float minY, float maxY, List<Tuple<float, float>> excludedPairs = null) {
        var availablePairs = new List<Tuple<float, float>>();

        if (excludedPairs != null) {
            for (float x = minX; x <= maxX; x++) {
                for (float y = minY; y <= maxY; y++) {
                    var pair = Tuple.Create(x, y);
                    if (!excludedPairs.Contains(pair)) {
                        availablePairs.Add(pair);
                    }
                }
            }
        } else {
            for (float x = minX; x <= maxX; x++) {
                for (float y = minY; y <= maxY; y++) {
                    availablePairs.Add(Tuple.Create(x, y));
                }
            }
        }

        if (availablePairs.Count > 0) {
            Random random = new Random();
            var randomIndex = random.Next(0, availablePairs.Count);
            var result = availablePairs[randomIndex];
            return result;
        } else {
            var result = Tuple.Create(0.0f, 0.0f);
            return result;
        }
    }
}