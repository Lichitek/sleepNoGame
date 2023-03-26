using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBooster : Pickup
{
    [SerializeField] private int _points;
    protected void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        FindObjectOfType<Score>().AddPoints(_points);
    }
}
