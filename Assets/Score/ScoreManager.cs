using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _scoreText;
    private int _score;
    private int _maxScore;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _score = 0;
        // _maxScore = 0;
        UpdateUI();
    }

    public void SetMaxScore(int value)
    {
        _maxScore = value;
        UpdateUI();
    }

    public void AddScore(int value)
    {
        _score += 1;
        UpdateUI();
    }

    public void UpdateUI()
    {
        _scoreText.SetText($"Score: {_score}/{_maxScore}");
    }
}
