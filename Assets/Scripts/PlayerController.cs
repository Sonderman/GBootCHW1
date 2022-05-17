using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed = 1.0f;
    public float jumpForce;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private GameManager _gameManager;
    private float _horizontalInput = 0f;
    private bool _isSpacePressed = false;
    private bool _isGrounded = false;

    private void Start()
    {
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        _animator = gameObject.GetComponent<Animator>();
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        CaptureInputs();
    }

    private void FixedUpdate()
    {
        MoveHorizontal();
    }

    private void CaptureInputs()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _isSpacePressed = Input.GetKeyDown(KeyCode.Space);
        if (_isSpacePressed) Jump();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            _rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            _gameManager.DecreaseLife();
        }

        if (collision.gameObject.CompareTag("Finish"))
        {
            _gameManager.LoadNextLevel();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("DeadZone"))
        {
            _gameManager.KillPlayer();
        }
    }


    private void MoveHorizontal()
    {
        if (_horizontalInput != 0f)
        {
            _rigidbody2D.AddForce(Vector2.right * (_horizontalInput * playerSpeed), ForceMode2D.Force);
        }
        
        if (_animator != null)
        {
            if (_horizontalInput == 0)
            {
                _animator.enabled = false;
            }
            else
            {
                _animator.enabled = true;
                RotatePlayer();
            }
        }else if(_horizontalInput != 0)RotatePlayer();
    }

    private void RotatePlayer()
    {
        if (_horizontalInput < 0f) transform.rotation = Quaternion.Euler(0, -180, 0);
        else transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void Jump()
    {
        if (_isSpacePressed && _isGrounded)
        {
            _isGrounded = false;
            _rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}