using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour {
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    public Snake snake;

    int levelSize = 3;
    int score;
    int level;


    float startInterval = 0.1f;
    float updateInterval;
    private float timer = 0.0f;

    void Start() {
        Restart();
    }

    private void FixedUpdate() {
        timer += Time.fixedDeltaTime;
        if (timer >= updateInterval) {
            snake.UpdateMovement();
            timer = 0.0f;
        }
    }

    void UpdateScore(int newScore) {
        score = newScore;
        scoreText.text = score.ToString();
        level = Mathf.FloorToInt(score / levelSize) + 1;
        updateInterval = startInterval / level;
    }


    public void IncrementScore() {
        UpdateScore(score + 1);
    }

    public void Restart() {
        UpdateHighScore();
        UpdateScore(0);
        snake.ResetBody();
    }
    private void UpdateHighScore() {
        var highScore = PlayerPrefs.GetInt("highScore", 0);
        if (score > highScore) {
            highScore = score;
            PlayerPrefs.SetInt("highScore", highScore);
        }
        highScoreText.text = highScore.ToString();
    }
}
