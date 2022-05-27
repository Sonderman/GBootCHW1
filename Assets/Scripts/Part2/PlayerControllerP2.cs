using System.Collections.Generic;
using UnityEngine;

namespace Part2
{
    public class PlayerControllerP2 : MonoBehaviour
    {
        public float playerSpeed = 1.0f;
        public float jumpForce;
        private Rigidbody2D _rigidbody2D;
        private Animator _animator;
        private GameManagerP2 _gameManager;
        private float _horizontalInput;
        private bool _isSpacePressed;
        private bool _isGrounded;
        private ParticleSystemController _particleSystemController;
        public GameObject HitParticleSystemObj;
        private AudioManager _audioManager;

        private void Start()
        {
            _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
            _animator = gameObject.GetComponent<Animator>();
            _gameManager = FindObjectOfType<GameManagerP2>();
            _particleSystemController = GetComponent<ParticleSystemController>();
            _audioManager = FindObjectOfType<AudioManager>();
        }

        private void Update()
        {
            CaptureInputs();
        }

        private void FixedUpdate()
        {
            MoveHorizontal();
            if (_rigidbody2D.velocity.y < -0.5f)
            {
                _animator.SetBool("isFalling",true);
            }else _animator.SetBool("isFalling",false);
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
                _audioManager.PlayClip(AudioManager.AudioClips.EnemyHit,gameObject.transform.position);
                _isGrounded = false;
                _rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                _particleSystemController.StopPS();
                Instantiate(HitParticleSystemObj, gameObject.transform.position + new Vector3(0.25f, 0, -2), Quaternion.identity);
                _gameManager.DecreaseHealth();
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("DeadZone"))
            {
                _audioManager.PlayClip(AudioManager.AudioClips.Fall,gameObject.transform.position);
                _gameManager.KillPlayer();
            }

            if (col.gameObject.CompareTag("Finish"))
            {
                _audioManager.StopAudio();
                _audioManager.PlayClip(AudioManager.AudioClips.Finish,gameObject.transform.position);
                _gameManager.LoadNextLevel();
            }
        }


        private void MoveHorizontal()
        {
            if (_horizontalInput != 0f)
            {
                _rigidbody2D.AddForce(Vector2.right * (_horizontalInput * playerSpeed), ForceMode2D.Force);
                if (!_particleSystemController.IsPlaying() && _isGrounded)
                {
                    _particleSystemController.StartPS();
                    _animator.SetBool("isRunning",true);
                }
            }
            else
            {
                if (_particleSystemController.IsPlaying())
                {
                    _particleSystemController.StopPS();
                    _animator.SetBool("isRunning",false);
                }
            }
            RotatePlayer();
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
                _audioManager.PlayClip(AudioManager.AudioClips.Jump,gameObject.transform.position);
                _isGrounded = false;
                _particleSystemController.StopPS();
                _animator.SetTrigger("Jumping");
                _animator.SetBool("isRunning",false);
                _rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }
    }
}