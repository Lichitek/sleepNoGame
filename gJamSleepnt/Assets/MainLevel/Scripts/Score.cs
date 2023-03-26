using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Score : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreBoard;
    private int _score;
    private bool _isDead;
    

    void Start()
    {
        _score = 0;
        _scoreBoard.text = _score.ToString();
    }

    
    void FixedUpdate()
    {
        if (!_isDead)
        {
            _score += 1;
            _scoreBoard.text = _score.ToString();
        }
    }

    public void MakeDead()
    {
        _isDead = true;
    }

    public void AddPoints(int points)
    {
        _score += points;
    }
}
