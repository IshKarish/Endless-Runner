using TMPro;
using UnityEngine;

public class ScoreTracker : MonoBehaviour
{
    private int _score;
    private int _highScore;
    
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    private void Awake()
    {
        _highScore = PlayerPrefs.GetInt("HighScore");
        highScoreText.text = "High Score: " + _highScore;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectable"))
        {
            AddScore(other.GetComponent<CoinSetup>().worth);
            Destroy(other.transform.parent.gameObject);
        }
    }

    private void AddScore(int score)
    {
        _score += score;
        scoreText.text = "Score: " + _score;

        if (_score <= _highScore) return;
        
        _highScore = _score;
        highScoreText.text = "High Score: " + _highScore;

        PlayerPrefs.SetInt("HighScore", _score);
        PlayerPrefs.Save();
    }
}
