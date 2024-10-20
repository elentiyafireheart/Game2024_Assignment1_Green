using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;

    int _score = 0;

    // Start is called before the first frame update
    void Start()
    {
        ChangeScore(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeScore(int points)
    {
        _score += points;
        UpdateScore();
    }

    void UpdateScore()
    {
        _scoreText.text = "POINTS: " + _score;
    }
}
