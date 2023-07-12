using UnityEngine;
using System.Collections.Generic;




public class Snake : MonoBehaviour {


    Vector2 _direction = Vector2.right;
    public List<Transform> segments = new List<Transform>();
    public Transform segmentPrefab;

    public GameManager gameManager;

    int initialSize = 4;
    bool movedPostDirectionChange = true;


    public void UpdateMovement() {
        for (int i = segments.Count - 1; i > 0; i--) {
            segments[i].position = segments[i - 1].position;
        }

        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + _direction.x,
            Mathf.Round(this.transform.position.y) + _direction.y,
            0.0f
        );
        movedPostDirectionChange = true;
    }


    void Update() {
        // reason for - movedPostDirectionChange 
        // let's say when user is moving to right
        // presses up and right in a very close interval, 
        // in other words, before moving 1 grid upwards user presses the right
        // in that case body collision was occurring
        // movedPostDirectionChange prevents that situation

        if (movedPostDirectionChange) {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
                // if single body then we can move in opposite direction 
                if (segments.Count == 1 || _direction != Vector2.down) {
                    _direction = Vector2.up;
                    movedPostDirectionChange = false;
                }
            } else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
                if (segments.Count == 1 || _direction != Vector2.up) {
                    _direction = Vector2.down;
                    movedPostDirectionChange = false;
                }
            } else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
                if (segments.Count == 1 || _direction != Vector2.right) {
                    _direction = Vector2.left;
                    movedPostDirectionChange = false;
                }
            } else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
                if (segments.Count == 1 || _direction != Vector2.left) {
                    _direction = Vector2.right;
                    movedPostDirectionChange = false;
                }
            }
        }
    }


    void Grow() {
        var segment = Instantiate(segmentPrefab);
        segment.position = segments[segments.Count - 1].position;
        segments.Add(segment);
    }

    public void ResetBody() {
        for (int i = 1; i < segments.Count; i++) {
            Destroy(segments[i].gameObject);
        }
        segments.Clear();
        this.transform.position = Vector3.zero;
        segments.Add(this.transform);
        for (int i = 1; i < this.initialSize; i++) {
            var segment = Instantiate(segmentPrefab);
            segment.position = new Vector3(transform.position.x - i, transform.position.y, transform.position.z);
            segments.Add(segment);
        }
        _direction = Vector2.right;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Food") {
            Grow();
            gameManager.IncrementScore();
        }
        if (other.tag == "Obstacle") {
            gameManager.Restart();
        }
    }
}
