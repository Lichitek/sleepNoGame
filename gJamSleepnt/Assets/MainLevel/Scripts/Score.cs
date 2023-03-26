using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Score : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreBoard;
    [SerializeField] private TextMeshProUGUI _scoreBoardEnd;
    private int _score;
    private bool _isDead;
    

    void Start()
    {
        _isDead= false;
        _score = 0;
        _scoreBoard.text = _score.ToString();
    }

    
    void FixedUpdate()
    {
        if (!_isDead)
        {
            _score += 1;
            //_scoreBoard.text = _score.ToString();
        }
        else
        {
            _score = 0;
            _isDead = false;
        }
            
        _scoreBoard.text=_score.ToString();
    }

    public void SetScoreToZero()
    {
        _score = 0;
    }

    public void MakeDead()
    {
        _scoreBoardEnd.text = "Your score : " + _score.ToString();        
        _isDead = true;
    }

    public void AddPoints(int points)
    {
        _score += points;
    }
}
