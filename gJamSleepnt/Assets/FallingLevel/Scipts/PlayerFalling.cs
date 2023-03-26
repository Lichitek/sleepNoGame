using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFalling : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private RoadSpawner _roadSpawner;
    [SerializeField] private List<GameObject> _movePoints;
    [SerializeField] private float _speed;

    private List<bool> _whichPoint = new List<bool>();
    private bool _isMovingLeft = false;
    private bool _isMovingRight = false;

    void Start()
    {
        _whichPoint.Add(false);
        _whichPoint.Add(true);
        _whichPoint.Add(false);
    }


    private void FixedUpdate()
    {
        if (_isMovingLeft || _isMovingRight)
        {
            if (_isMovingLeft)
            {
                transform.Translate(Vector3.left * Time.fixedDeltaTime * _speed);
                if (transform.position.x <= _movePoints[_whichPoint.IndexOf(true)].transform.position.x)
                {
                    transform.position = new Vector3(_movePoints[_whichPoint.IndexOf(true)].transform.position.x, transform.position.y, 0);
                    _isMovingLeft = false;
                }
            }
            else
            {
                transform.Translate(Vector3.right * Time.fixedDeltaTime * _speed);
                if (transform.position.x >= _movePoints[_whichPoint.IndexOf(true)].transform.position.x)
                {
                    transform.position = new Vector3(_movePoints[_whichPoint.IndexOf(true)].transform.position.x, transform.position.y, 0);
                    _isMovingRight = false;
                }
            }
        }
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && _whichPoint[2] != true && !_isMovingLeft && !_isMovingRight)
        {
            int i = _whichPoint.IndexOf(true);
            _whichPoint[i] = false;
            _whichPoint[i + 1] = true;
            _isMovingRight = true;
        }

        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && _whichPoint[0] != true && !_isMovingLeft && !_isMovingRight)
        {
            int i = _whichPoint.IndexOf(true);
            _whichPoint[i] = false;
            _whichPoint[i - 1] = true;
            _isMovingLeft = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle")
        {
            Health health = (Health)FindObjectOfType(typeof(Health));
            health.TakeDamage();
        }

        if (other.tag == "Untagged")
            _roadSpawner.Spawn();
    }

    public void StopMoving()
    {
        _isMovingLeft = false;
        _isMovingRight = false;
    }
}
