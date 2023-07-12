using UnityEngine;
using System.Collections.Generic;
using System;

public class Food : MonoBehaviour {
    public BoxCollider2D gridArea;
    public Snake snake;
    public void RandomizePosition() {
        Bounds bounds = gridArea.bounds;
        var excludedPairs = new List<Tuple<float, float>>();
        foreach (var segment in snake.segments) {
            var pair = Tuple.Create(segment.position.x, segment.position.y);
            excludedPairs.Add(pair);
        }
        var selected = RandomNumberGenerator.RandomPairBetween(
            minX: bounds.min.x,
            maxX: bounds.max.x,
            minY: bounds.min.y,
            maxY: bounds.max.y,
            excludedPairs: excludedPairs
        );
        var x = selected.Item1;
        var y = selected.Item2;
        transform.position = new Vector3(x, y, 0.0f);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            RandomizePosition();
        }
    }
}
