using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private RoadSpawner _roadSpawner;
    [SerializeField] private List<GameObject> _movePoints;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpSpeed; 
    [SerializeField] private float _jumpHeight;
    [SerializeField] private float _slidingTime;

    private List<bool> _whichPoint = new List<bool>();
    private bool _isMovingLeft = false;
    private bool _isMovingRight = false;
    [SerializeField] private bool _isJumping = false;
    [SerializeField] private bool _isGoingDown = false;
    [SerializeField] private bool _isGrounded = true;
    [SerializeField] private bool _isSliding = false;

    [SerializeField] private float _lastYPos;
    private float _currentSlidingTime = 0f;
    private float _GroundSlidingY;

    private void Start()
    {
        _whichPoint.Add(false);
        _whichPoint.Add(true);
        _whichPoint.Add(false);
        _currentSlidingTime = 0f;
        _lastYPos = 1f;
    }

    private void FixedUpdate()
    {
        if (_isSliding)
        {
            if (_currentSlidingTime <= _slidingTime && _isGrounded)
            {
                _currentSlidingTime += Time.fixedDeltaTime;
                if (transform.position.y < 0.2f)
                {
                    _lastYPos = 1f;
                    transform.position =  new Vector3(0f, _GroundSlidingY, 0f);
                }
            }
            else
            {
                _isSliding = false;
                transform.Rotate(new Vector3(-90f, transform.rotation.y));
                if (transform.position.y - _lastYPos > 0.5f)
                    _lastYPos = 1f;
                transform.position = new Vector3(_movePoints[_whichPoint.IndexOf(true)].transform.position.x, _lastYPos, _movePoints[_whichPoint.IndexOf(true)].transform.position.z);
                _currentSlidingTime = 0f;
                _animator.SetBool("isSliding", false);
                transform.GetChild(0).transform.localPosition = new Vector3(0f, -0.72f, 0f);
                transform.GetChild(0).Rotate(new Vector3(90f, 0f));
            }
        }

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

        if (_isJumping)
        {
            if (transform.position.y <= _lastYPos + _jumpHeight)
            {
                transform.Translate(Vector3.up * _jumpSpeed * Time.fixedDeltaTime);
            }
            else
            {
                _isJumping = false;
                _isGoingDown = true;
                _lastYPos = 1f;
                _animator.SetBool("isFallingDown", true);
                _animator.SetBool("isJumping", false);
            }
        }

        if (_isGoingDown)
        {
            if (transform.position.y >= _lastYPos)
                transform.Translate(Vector3.down * _jumpSpeed * Time.fixedDeltaTime);
            else
            {
                transform.position = new Vector3(transform.position.x, _lastYPos, transform.position.z);
                _isGoingDown = false;
                _isGrounded = true;
                _animator.SetBool("isFallingDown", false);
            }
        }
    }

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && _whichPoint[2] != true && !_isMovingLeft && !_isMovingRight && !_isSliding)
        {
            int i = _whichPoint.IndexOf(true);
            _whichPoint[i] = false;
            _whichPoint[i + 1] = true;
            _isMovingRight = true;
        }

        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && _whichPoint[0] != true && !_isMovingLeft && !_isMovingRight && !_isSliding)
        {
            int i = _whichPoint.IndexOf(true);
            _whichPoint[i] = false;
            _whichPoint[i - 1] = true;
            _isMovingLeft = true;
        }

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && _isGrounded && (!_isMovingLeft && !_isMovingRight) && !_isSliding)
        {
            _isJumping = true;
            _isGrounded = false;
            _lastYPos = transform.position.y;
            _animator.SetBool("isJumping", true);
        }

        if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && _isGrounded && (!_isMovingLeft && !_isMovingRight) && !_isSliding)
        {
            _isSliding = true;
            transform.Translate(new Vector3(0f, transform.position.y - GetComponent<CapsuleCollider>().height / 1.5f, 0f));
            if (_GroundSlidingY == 0f)
                _GroundSlidingY = transform.position.y;
            transform.Rotate(new Vector3(90f, 0f));
            _animator.SetBool("isSliding", true);
            if (transform.position.y > 1f)
                transform.GetChild(0).transform.localPosition = new Vector3(0f, 0f, 0f);
            else
                transform.GetChild(0).transform.localPosition = new Vector3(0f, 0f, 0.5f);
            transform.GetChild(0).Rotate(new Vector3(-90f, 0f));
        }

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Ground")
        {
            _isGrounded = true;
            _isGoingDown = false;
            if (!_isSliding)
                _lastYPos = transform.position.y;
            _animator.SetBool("isFallingDown", false);
        }

        if (other.tag == "Obstacle")
        {
            Health health = (Health)FindObjectOfType(typeof(Health));
            if (other.name == "FrontSide")
            {
                health.Death();
            }

            health.TakeDamage();
            if (_isMovingLeft)
            {
                int i =_whichPoint.IndexOf(true);
                _whichPoint[i] = false;
                _whichPoint[i + 1] = true;
                _isMovingLeft = false;
                _isMovingRight = true;
            }
            else if (_isMovingRight)
            {
                int i = _whichPoint.IndexOf(true);
                _whichPoint[i] = false;
                _whichPoint[i - 1] = true;
                _isMovingLeft = true;
                _isMovingRight = false;
            }
        }

        if (other.tag == "Untagged")
            _roadSpawner.Spawn();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ground")
        {
            if (_isSliding)
            {
                transform.Translate(new Vector3(0f, 0f, other.transform.position.y + GetComponent<CapsuleCollider>().height / 3f));
            }
            else
            {
                _isGrounded = false;
                if (!_isJumping)
                {
                    _isGoingDown = true;
                    _animator.SetBool("isFallingDown", true);
                }
                else
                    _animator.SetBool("isJumping", true);
                _lastYPos = 1f;
            }
        }
    }

    public void StopMoving()
    {
        _isMovingLeft = false;
        _isMovingRight = false;
    }
}
