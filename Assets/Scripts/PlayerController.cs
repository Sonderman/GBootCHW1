using System;
using System.Collections;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    public float playerSpeed = 2.0f; 
    public float jumpForce;
    private bool isGrounded;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private GameManager _gameManager;
    void Start()
    {
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        _animator = gameObject.GetComponent<Animator>();
        isGrounded = false;
         _gameManager = FindObjectOfType<GameManager>();
    }
    
    void Update()
    {
        Move();
        Jump();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("DeadZone")||collision.gameObject.CompareTag("Enemy"))
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                _rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                _gameManager.DecreaseLife();
                return;
            }
            _gameManager.KillPlayer();
        }

        if (collision.gameObject.CompareTag("Finish"))
        {
            _gameManager.LoadNextLevel();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Gold"))
        {
            _gameManager.IncreaseGold();
            Destroy(col.gameObject);
        }
    }
    

    private void Move()
    {
        var transfrm = gameObject.transform;
        var position= transfrm.position;
        var input = Input.GetAxis("Horizontal");
        
        gameObject.transform.position = new Vector2(position.x+=input*Time.deltaTime*playerSpeed,position.y);
        if (input == 0)
        {
            _animator.enabled = false;
        }
        else
        {
            _animator.enabled = true;
            if (input<0f) transfrm.rotation = Quaternion.Euler(0, -180, 0);
            else transfrm.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded){
            isGrounded = false;
            _rigidbody2D.AddForce(Vector2.up*jumpForce,ForceMode2D.Impulse);
        }
    }
    
}
