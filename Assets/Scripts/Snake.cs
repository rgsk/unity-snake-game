using UnityEngine;
using System.Collections.Generic;

public class Snake : MonoBehaviour {
    Vector2 _direction = Vector2.right;
    public List<Transform> segments;
    public Transform segmentPrefab;
    void Start() {
        segments = new List<Transform>();
        segments.Add(this.transform);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
            _direction = Vector2.up;
        } else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
            _direction = Vector2.down;
        } else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
            _direction = Vector2.left;
        } else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
            _direction = Vector2.right;
        }
    }
    void FixedUpdate() {
        for (int i = segments.Count - 1; i > 0; i--) {
            segments[i].position = segments[i - 1].position;
        }
        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + _direction.x,
            Mathf.Round(this.transform.position.y) + _direction.y,
            0.0f
        );
    }

    void Grow() {
        var segment = Instantiate(segmentPrefab);
        segment.position = segments[segments.Count - 1].position;
        segments.Add(segment);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Food") {
            Grow();
        }
    }
}
