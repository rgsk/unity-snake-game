using UnityEngine;
using System.Collections.Generic;




public class Snake : MonoBehaviour {

    int levelSize = 3;
    int score;
    int level;

    Vector2 _direction = Vector2.right;
    public List<Transform> segments = new List<Transform>();
    public Transform segmentPrefab;

    int initialSize = 4;

    float startInterval = 0.1f;
    float updateInterval;
    private float timer = 0.0f;

    void Start() {
        ResetState();
    }

    void UpdateLevel() {
        level = Mathf.FloorToInt(score / levelSize) + 1;
        updateInterval = startInterval / level;
    }



    private void FixedUpdate() {
        timer += Time.fixedDeltaTime;
        if (timer >= updateInterval) {
            UpdateMovement();
            timer = 0.0f;
        }
    }
    private void UpdateMovement() {
        for (int i = segments.Count - 1; i > 0; i--) {
            segments[i].position = segments[i - 1].position;
        }

        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + _direction.x,
            Mathf.Round(this.transform.position.y) + _direction.y,
            0.0f
        );
    }


    void Update() {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
            // if single body then we can move in opposite direction 
            if (segments.Count == 1 || _direction != Vector2.down) {
                _direction = Vector2.up;
            }
        } else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
            if (segments.Count == 1 || _direction != Vector2.up) {
                _direction = Vector2.down;
            }
        } else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
            if (segments.Count == 1 || _direction != Vector2.right) {
                _direction = Vector2.left;
            }
        } else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
            if (segments.Count == 1 || _direction != Vector2.left) {
                _direction = Vector2.right;
            }
        }
    }


    void Grow() {
        var segment = Instantiate(segmentPrefab);
        segment.position = segments[segments.Count - 1].position;
        segments.Add(segment);

        // add to score
        score++;
        UpdateLevel();
    }

    void ResetState() {
        for (int i = 1; i < segments.Count; i++) {
            Destroy(segments[i].gameObject);
        }
        segments.Clear();
        segments.Add(this.transform);

        for (int i = 1; i < this.initialSize; i++) {
            segments.Add(Instantiate(segmentPrefab));
        }

        this.transform.position = Vector3.zero;
        score = 0;
        UpdateLevel();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Food") {
            Grow();
        }
        if (other.tag == "Obstacle") {
            ResetState();
        }
    }
}
